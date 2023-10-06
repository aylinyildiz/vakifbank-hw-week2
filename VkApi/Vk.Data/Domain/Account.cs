using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Vk.Base;

namespace Vk.Data.Domain;

[Table("Account", Schema = "dbo")]
public class Account : BaseModel
{
    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; }

    public string Name { get; set; }
    public int AccountNumber { get; set; }
    public string IBAN { get; set; }
    public decimal Balance { get; set; }
    public string CurrencyCode { get; set; }
    public DateTime OpenDate { get; set; }
    public DateTime? CloseDate { get; set; }

    public int? CardId { get; set; }
    public virtual Card Card { get; set; }

    public virtual List<EftTransaction> EftTransactions { get; set; }
    public virtual List<AccountTransaction> AccountTransactions { get; set; }
}



public class AccountConfigruration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.Property(x => x.InsertUserId).IsRequired();
        builder.Property(x => x.UpdateUserId).IsRequired().HasDefaultValue(0);
        builder.Property(x => x.InsertDate).IsRequired();
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(x => x.CustomerId).IsRequired(true);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.AccountNumber).IsRequired(true);
        builder.Property(x => x.IBAN).IsRequired().HasMaxLength(34);
        builder.Property(x => x.Balance).IsRequired().HasPrecision(18,2).HasDefaultValue(0);
        builder.Property(x => x.CurrencyCode).IsRequired().HasMaxLength(3);
        builder.Property(x => x.OpenDate).IsRequired();
        builder.Property(x => x.CloseDate).IsRequired(false);
        builder.Property(x => x.CardId).IsRequired(false);
        
        builder.HasIndex(x => x.CustomerId);
        builder.HasIndex(x => x.AccountNumber).IsUnique(true);

        builder.HasMany(x => x.EftTransactions)
            .WithOne(x => x.Account)
            .HasForeignKey(x => x.AccountId)
            .IsRequired(true);

        builder.HasMany(x => x.AccountTransactions)
            .WithOne(x => x.Account)
            .HasForeignKey(x => x.AccountId)
            .IsRequired(true);

        builder.HasOne(x=> x.Card)
            .WithOne(x=> x.Account)
            .HasForeignKey<Card>().IsRequired(true);    
    }
}