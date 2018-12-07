using Microsoft.EntityFrameworkCore;
using System;
namespace CoreBackend.Api.Entities
{

    /// <summary>
    /// 商城访问配置
    /// </summary>
    public class ProductContext : DbContext
    {

        public DbSet<Buyer> Buyers { get; set; }

        public DbSet<Seed> Seeds { get; set; }

        public DbSet<Seller> Sellers { get; set; }

        public DbSet<Indent> Indents { get; set; }

        public DbSet<Inventory> Inventorys { get; set; }

        public DbSet<SellInformation> SellInformations { get; set; }

        public DbSet<UserStatus> userStatuses { get; set; }

        public DbSet<FileUpDownload> fileUpDownloads { get; set; }

        protected override void OnModelCreating(ModelBuilder _modelBuilder)
        {

            //设置数据库字段标识（自动增长）
            //  _modelBuilder.Entity<Buyer>().Property(t => t.BuyerID);
            _modelBuilder.Entity<Buyer>().Property(x => x.Account).HasMaxLength(30);
           _modelBuilder.ApplyConfiguration(new BuyerConfiguration());

            _modelBuilder.ApplyConfiguration(new SeedConfiguration());

            _modelBuilder.ApplyConfiguration(new SellerConfiguration());

            _modelBuilder.ApplyConfiguration(new IndentConfiguration());

            _modelBuilder.ApplyConfiguration(new InventoryConfiguration());

            _modelBuilder.ApplyConfiguration(new SellInformationConfiguration());

            _modelBuilder.ApplyConfiguration(new UserStatusConfiguration());

            _modelBuilder.ApplyConfiguration(new FileUpDownloadConfiguration());
        }

        public ProductContext(DbContextOptions<ProductContext> dbContextOptions) : base(dbContextOptions)
        {
           

#if DEBUG
            try
            {
                //  Database.Migrate();

                Database.EnsureCreated();

            }
            catch (Exception e)
            {

                string es = e.ToString();
                 throw;
            }
#else 
            Database.Migrate();
#endif

        }

        public ProductContext() : base()
        {


#if DEBUG
            try
            {
                 Database.Migrate();

                //Database.EnsureCreated();

            }
            catch (Exception e)
            {

                string es = e.ToString();
                throw;
            }
#else 
            Database.Migrate();
#endif

        }

    }
}
