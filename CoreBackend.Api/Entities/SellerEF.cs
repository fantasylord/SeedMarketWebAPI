using CoreBackend.Api.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreBackend.Api.Entities
{
    /// <summary>
    /// 卖家信息表
    /// </summary>
    public class Seller
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SellerID { set; get; }

        public string Name { set; get; }

        public string Detail { set; get; }
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string MarkerID { set; get; }
        public string MarkerName { set; get; }

        public ICollection<Seed> Seeds { set; get; }
        public BusinessStatus IsStatus { set; get; }


    }

    
    public class SellerConfiguration : IEntityTypeConfiguration<Seller>
    {
        public void Configure(EntityTypeBuilder<Seller> builder)
        {
            builder.HasKey(p=>p.SellerID);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Detail).IsRequired().HasMaxLength(100);
            builder.Property(x => x.MarkerID).IsRequired();
            builder.Property(x => x.IsStatus).IsRequired().HasMaxLength(1);
            builder.HasMany(x => x.Seeds).WithOne(x => x.Seller).HasForeignKey(x => x.SellerID).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
