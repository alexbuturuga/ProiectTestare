using Microsoft.EntityFrameworkCore;
using Proiect_DAW.Models;

namespace Proiect_DAW
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Product
            modelBuilder.Entity<Product>(builder =>
            {
                builder.HasKey(p => p.ProductId);
                builder.Property(p => p.ProductName).IsRequired().HasMaxLength(50);
                builder.Property(p => p.ProductDescription).IsRequired().HasMaxLength(500);
                builder.Property(p => p.Price).IsRequired();
            });

            //Promotion
            modelBuilder.Entity<Promotion>(builder =>
            {
                builder.HasKey(p => p.PromotionId);
                builder.Property(p => p.PromotionDescription).IsRequired().HasMaxLength(500);
                builder.Property(p => p.Discount).IsRequired();
            });

            //ReceiptProduct
            modelBuilder.Entity<ReceiptProduct>(builder =>
            {
                builder.Property(p => p.ProductId);
                builder.Property(p => p.ReceiptId);
                builder.Property(p => p.Amount).IsRequired();

                builder.HasOne(p => p.Receipt).WithMany()
                       .HasForeignKey(p => p.ReceiptId)
                       .OnDelete(DeleteBehavior.Cascade);

                builder.HasOne(p => p.Product).WithMany()
                       .HasForeignKey(p => p.ProductId)
                       .OnDelete(DeleteBehavior.Cascade);
            });

            //AccountPromotion
            modelBuilder.Entity<AccountPromotion>(builder =>
            {
                builder.Property(p => p.AccountId);
                builder.Property(p => p.PromotionId);

                builder.HasOne(p => p.Account).WithMany()
                       .HasForeignKey(p => p.AccountId)
                       .OnDelete(DeleteBehavior.Cascade);

                builder.HasOne(p => p.Promotion).WithMany()
                       .HasForeignKey(p => p.PromotionId)
                       .OnDelete(DeleteBehavior.Cascade);
            });

            //Receipt
            modelBuilder.Entity<Receipt>(builder =>
            {
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Sum).IsRequired();
                builder.Property(p => p.Date).IsRequired();

                builder.HasOne(p => p.Account).WithMany()
                       .HasForeignKey(p => p.AccountId)
                       .OnDelete(DeleteBehavior.Cascade);
            });

            //Account
            modelBuilder.Entity<Account>(builder =>
            {
                builder.HasKey(p => p.Id);
                builder.Property(p => p.UserName).IsRequired();
                builder.Property(p => p.Password).IsRequired();
                builder.Property(p => p.Admin).IsRequired();
                builder.Property(p => p.LastName).IsRequired();
                builder.Property(p => p.FirstName).IsRequired();
                builder.Property(p => p.PhoneNumber).IsRequired();
                builder.Property(p => p.RegisterDate).IsRequired();
            });
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountPromotion> AccountPromotions { get; set; }
        public DbSet<ReceiptProduct> ReceiptProducts { get; set; }
    }
}