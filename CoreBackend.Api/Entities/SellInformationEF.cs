using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreBackend.Api.Entities
{
    /// <summary>
    /// 销售信息实体模型
    /// </summary>
    public class SellInformation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SelledID { set; get; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IndentID { set; get; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SeedID { set; get; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SellerID { set; get; }

        public int SellCount { set; get; }

        public DateTime SellTime { set; get; }

 
        
        public string  Account { set; get; }



    }
    public class SellInformationConfiguration : IEntityTypeConfiguration<SellInformation>
    {
        public void Configure(EntityTypeBuilder<SellInformation> builder)
        {
            builder.HasKey(x => new { x.SelledID, x.IndentID, x.SeedID,x.Account});

            builder.Property(x => x.Account).IsRequired();

            builder.Property(x => x.SellCount).IsRequired();

            builder.Property(x => x.SelledID).IsRequired();

            builder.Property(x => x.SellerID).IsRequired();

            builder.Property(x => x.SellTime).IsRequired();

            builder.Property(x => x.Account).IsRequired().HasMaxLength(50);
        }
    }
}
