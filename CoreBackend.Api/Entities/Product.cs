using CoreBackend.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
/// <summary>
/// Entity模型 即数据库存储模型 也是原模型 所有模型的初态，即含有所有属性方法以及字段
/// </summary>
namespace CoreBackend.Api.Entity
{

    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public ICollection<Material> Materials { get; set; }
    }

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.ID);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Price).HasColumnType("float(8,2)");
            builder.Property(x => x.Description).HasMaxLength(200);
        }

    
    }
}
