using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Vk.Base;

namespace Vk.Data.Domain;

[Table("AccountTransaction", Schema = "dbo")]
public class AccountTransaction : BaseModel
{
    public int AccountId { get; set; }
    public virtual Account Account { get; set; }

    public string ReferenceNumber { get; set; }   
    public decimal DebitAmount { get; set; }
    public decimal CreditAmount { get; set; }
    public string Description { get; set; }
    public DateTime TransactionDate { get; set; }
    public string TransactionCode { get; set; }
}

public class AccountTransactionConfigruration : IEntityTypeConfiguration<AccountTransaction>
{
    public void Configure(EntityTypeBuilder<AccountTransaction> builder)
    {
        builder.Property(x => x.InsertUserId).IsRequired();
        builder.Property(x => x.UpdateUserId).IsRequired().HasDefaultValue(0);
        builder.Property(x => x.InsertDate).IsRequired();
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(x => x.AccountId).IsRequired(true);
        builder.Property(x => x.ReferenceNumber).IsRequired().HasMaxLength(50);
        builder.Property(x => x.DebitAmount).IsRequired().HasPrecision(18,2).HasDefaultValue(0);
        builder.Property(x => x.CreditAmount).IsRequired().HasPrecision(18,2).HasDefaultValue(0);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(50);
        builder.Property(x => x.TransactionDate).IsRequired();
        builder.Property(x => x.TransactionCode).IsRequired().HasMaxLength(10);

        builder.HasIndex(x => x.AccountId);
    }
}