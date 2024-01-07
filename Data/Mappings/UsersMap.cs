using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Picpay_01.Models;
using Picpay_01.Models.Enums;

namespace Picpay_01.Mappings;

public class UsersMap : IEntityTypeConfiguration<Users>
{
    public void Configure(EntityTypeBuilder<Users> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Email, "IX_USER_EMAIL")
            .IsUnique();

        builder.HasIndex(x => x.Document, "IX_USER_DOCUMET")
            .IsUnique();

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasColumnName("FirstName")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasColumnName("LastName")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);

        builder.Property(x => x.Document)
            .IsRequired()
            .HasColumnName("Document")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(16);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnName("Email")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);

        builder.Property(x => x.Password)
            .IsRequired()
            .HasColumnName("Password")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(36);

        builder.Property(x => x.Balance)
            .IsRequired()
            .HasColumnName("Balance")
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.UserType)
            .IsRequired()
            .HasConversion(y => y.ToString(),
                y => (UserType)Enum.Parse<UserType>(y));



    }
}