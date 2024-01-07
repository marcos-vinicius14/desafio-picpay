using Microsoft.EntityFrameworkCore;
using Picpay_01.Mappings;
using Picpay_01.Models;

namespace Picpay_01.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> contextOptions)
        : base(contextOptions)
    {}

    public DbSet<Users> Users { get; set; }
    public DbSet<Transactions> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsersMap());
        modelBuilder.ApplyConfiguration(new TransactionsMap());
    }
}