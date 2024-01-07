using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Picpay_01.Models;

namespace Picpay_01.Mappings;

public class TransactionsMap : IEntityTypeConfiguration<Transactions>
{
    public void Configure(EntityTypeBuilder<Transactions> builder)
    {
        builder.ToTable("Transactions");

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(x => x.Amount)
            .HasColumnName("Amount")
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.LocalDateTime)
            .HasColumnName("LocalDateTime")
            .HasColumnType("SMALLDATETIME")
            .HasMaxLength(60)
            .HasDefaultValueSql("GETDATE()");

        /*builder.HasOne(x => x.Sender)
            .WithMany(x => x.TransactionsSender)
            .HasConstraintName("FK_SENDER_TRANSACTIONS")
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Receiver)
            .WithMany(x => x.TransactionsReceiver)
            .HasConstraintName("FK_RECEIVER_TRANSACTIONS")
            .OnDelete(DeleteBehavior.NoAction);
        */

    }
}