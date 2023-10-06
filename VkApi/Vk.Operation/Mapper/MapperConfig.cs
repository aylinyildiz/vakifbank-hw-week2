using AutoMapper;
using Vk.Data.Domain;
using Vk.Schema;

namespace Vk.Operation.Mapper;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<CustomerRequest, Customer>();
        CreateMap<Customer, CustomerResponse>();

        CreateMap<AccountRequest, Account>();
        CreateMap<Account, AccountResponse>()
            .ForMember(dest => dest.CustomerName,
                opt => opt.MapFrom(src => src.Customer.FirstName + " " + src.Customer.LastName));

        CreateMap<CardRequest, Card>();
        CreateMap<Card, CardResponse>()
            .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.Account.Name))
            .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.Account.AccountNumber));

        CreateMap<AddressRequest, Address>();
        CreateMap<Address, AddressResponse>()
            .ForMember(dest => dest.CustomerName,
                opt => opt.MapFrom(src => src.Customer.FirstName + " " + src.Customer.LastName));

        CreateMap<EftTransactionRequest, EftTransaction>();
        CreateMap<EftTransaction, EftTransactionResponse>()
            .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.Account.Name))
            .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.Account.AccountNumber))
            .ForMember(dest => dest.CustomerName,
                opt => opt.MapFrom(src => src.Account.Customer.FirstName + " " + src.Account.Customer.LastName))
            .ForMember(dest => dest.CustomerNumber, opt => opt.MapFrom(src => src.Account.Customer.CustomerNumber));

        CreateMap<AccountTransactionRequest, AccountTransaction>();
        CreateMap<AccountTransaction, AccountTransactionResponse>()
            .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.Account.Name))
            .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.Account.AccountNumber))
            .ForMember(dest => dest.CustomerName,
                opt => opt.MapFrom(src => src.Account.Customer.FirstName + " " + src.Account.Customer.LastName))
            .ForMember(dest => dest.CustomerNumber, opt => opt.MapFrom(src => src.Account.Customer.CustomerNumber));


        CreateMap<MoneyTransferRequest, MoneyTransferResponse>();
        CreateMap<EftTransferRequest, EftTransferResponse>();
    }
}