using CoreBackend.Api.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreBackend.Api.Entities
{
    /// <summary>
    /// 产品实体
    /// </summary>
    public class Seed
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SeedID { set; get; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SellerID { set; get; }

        public string MakertID { set; get; }

        public string Brand { get; set; }

        public string Name { set; get; }
        public int FID { set; get; }
        public string Details { set; get; }
        public SeedClass SeedClass { set; get; }

        public float Price { set; get; }
        public string Exhibitions { set; get; }
        public SpeciesClass Species { set; get; }
        public Seller Seller { set; get; }
      
        public ICollection<FileUpDownload> fileUpDownloads { set; get; }
    }

    public class SeedConfiguration : IEntityTypeConfiguration<Seed>
    {
        public void Configure(EntityTypeBuilder<Seed> builder)
        {

            builder.HasKey(x => new { x.SeedID, x.SellerID, x.Brand });
            builder.Property(x => x.Brand).HasMaxLength(30);
            builder.Property(x => x.MakertID).HasMaxLength(30);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Details).IsRequired().HasMaxLength(30);
            builder.Property(x => x.SeedClass).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Exhibitions).IsRequired().HasMaxLength(1024);
            builder.Property(x => x.Species).IsRequired().HasMaxLength(30);
            builder.Property(x => x.FID).HasMaxLength(10);
            builder.HasMany(x => x.fileUpDownloads).WithOne(x => x.seed).OnDelete(DeleteBehavior.Cascade);
           
        }
    }
}

