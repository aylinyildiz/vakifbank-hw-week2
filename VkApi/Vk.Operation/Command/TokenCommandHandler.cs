using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Vk.Base;
using Vk.Base.Response;
using Vk.Base.Token;
using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Schema;

namespace Vk.Operation.Command;

public class TokenCommandHandler :
    IRequestHandler<CreateTokenCommand, ApiResponse<TokenResponse>>

{
    private readonly VkDbContext dbContext;
    private readonly JwtConfig jwtConfig;

    public TokenCommandHandler(VkDbContext dbContext,IOptionsMonitor<JwtConfig> jwtConfig)
    {
        this.dbContext = dbContext;
        this.jwtConfig = jwtConfig.CurrentValue;
    }
    

    public async Task<ApiResponse<TokenResponse>> Handle(CreateTokenCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Customer>().FirstOrDefaultAsync(x => x.CustomerNumber == request.Model.CustomerNumber, cancellationToken);
        if (entity == null)
        {
            return new ApiResponse<TokenResponse>("Invalid user informations");
        }

        var md5 = Md5.Create(request.Model.Password.ToUpper());
        //if (entity.Password != md5)
        if(entity.Password != request.Model.Password)
        {
            entity.LastActivityDate = DateTime.UtcNow;
            entity.PasswordRetryCount++;
            await dbContext.SaveChangesAsync(cancellationToken);

            return new ApiResponse<TokenResponse>("Invalid user informations");
        }

        if (!entity.IsActive)
        {
            return new ApiResponse<TokenResponse>("Invalid user!");
        }

        string token = Token(entity);
        TokenResponse tokenResponse = new()
        {
            Token = token,
            ExpireDate = DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
            CustomerNumber = entity.CustomerNumber,
            Email = entity.Email
        };
        
        return new ApiResponse<TokenResponse>(tokenResponse);
    }
    
    private string Token(Customer user)
    {
        Claim[] claims = GetClaims(user);
        var secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);

        var jwtToken = new JwtSecurityToken(
            jwtConfig.Issuer,
            jwtConfig.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
        );

        string accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return accessToken;
    }


    private Claim[] GetClaims(Customer customer)
    {
        var claims = new[]
        {
            new Claim("Id", customer.Id.ToString()),
            new Claim("CustomerNumber", customer.CustomerNumber.ToString()),
            new Claim("Role", customer.Role),
            new Claim("Email", customer.Email),
            new Claim(ClaimTypes.Role, customer.Role),
            new Claim("FullName", $"{customer.FirstName} {customer.LastName}")
        };

        return claims;
    }
}