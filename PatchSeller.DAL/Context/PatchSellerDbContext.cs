using Microsoft.EntityFrameworkCore;
using PatchSeller.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatchSeller.DAL.Context
{
    public class PatchSellerDbContext:DbContext
    {
        public PatchSellerDbContext()
        {
            
        }

        public PatchSellerDbContext(DbContextOptions<PatchSellerDbContext> options) : base(options)
        {

        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Patch> Patches { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<UserPurchase> UserPurchases { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameCategory> GameCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<GameImage> GameImages { get; set; }
        public DbSet<GamePlatform> GamePlatforms { get; set; }
        public DbSet<PatchVersion> PatchVersions { get; set; }
        public DbSet<PatchImage> PatchImages { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<DownloadLog> DownloadLogs { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<ActionLog> ActionLogs { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<InstallLog> InstallLogs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<PagePermission> PagePermissions { get; set; }
        public DbSet<StaffPagePermission> StaffPagePermissions { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            PatchSellerDbSeeder.Seed(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=PatchSeller;TrustServerCertificate=True;User Id=sa; Password=123456");
        }
    }
}
