using CoreBackend.Api.Entity;
using Microsoft.EntityFrameworkCore;
using System;

namespace CoreBackend.Api.Entities
{
    /// <summary>
    /// MySqlConnection product
    /// </summary>
    public class MyContext : DbContext
    {

        //针对DbSet的Linq查询语句将会被解释成针对数据库的查询语句
        /// <summary>
        /// DB访问产品数据操作
        /// </summary>
        public DbSet<Product> Products { get; set; }

        //同理注册对Material的使用
        public DbSet<Material> Materials { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySQL(Startup.Configuration["mysqlSettings:ConnectionString"]);
        //    base.OnConfiguring(optionsBuilder);
        //}
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(Startup.Configuration["sqlSettings:ConnectionString"]);
        //    base.OnConfiguring(optionsBuilder);
        //}

        //---采用IEntityTypeConfiguration
        protected override void OnModelCreating(ModelBuilder _modelBuilder)
        {
            _modelBuilder.ApplyConfiguration(new ProductConfiguration());
            _modelBuilder.ApplyConfiguration(new MaterialConfiguration());
        }

        public MyContext(DbContextOptions<MyContext> dbContextOptions) : base(dbContextOptions)
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


    }
}
