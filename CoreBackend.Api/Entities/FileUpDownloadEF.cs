using CoreBackend.Api.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreBackend.Api.Entities
{
    public class FileUpDownload
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int  FID { set; get; }
        //x.SeedID, x.SellerID, x.Brand
   

        public string FileUrl { get; set; }
        public FileClass FileClass { get; set; }
        public string FileName { set; get; }        
        public Seed seed { get; set; }
    }
    public class FileUpDownloadConfiguration : IEntityTypeConfiguration<FileUpDownload>
    {
        public void Configure(EntityTypeBuilder<FileUpDownload> builder)
        {
            //  builder.HasKey(x => new { x.BuyerID });

            builder.HasKey(x =>x.FID);
          //  builder.Property(x => x.SeedID).IsRequired();
            builder.Property(x => x.FileClass).IsRequired().HasMaxLength(30);
            builder.Property(x => x.FileUrl).IsRequired().HasMaxLength(500);
            builder.Property(x => x.FileName).IsRequired().HasMaxLength(30);
           // builder.HasOne(x => x.Product).WithMany(x => x.Materials).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);
      
        }
    }
}
