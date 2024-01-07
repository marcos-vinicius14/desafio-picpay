using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Picpay_01.Models;

namespace Picpay_01.Mappings;

public class CompanyAccountMap : IEntityTypeConfiguration<CompanyAccount>
{
    public void Configure(EntityTypeBuilder<CompanyAccount> builder)
    {
        builder.ToTable("CompanyAccount");

        builder.Property(x => x.CNPJ)
            .IsRequired()
            .UseIdentityColumn()
            .HasColumnName("CNPJ")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(14);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnName("E-mail")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Nome")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);

    }
}