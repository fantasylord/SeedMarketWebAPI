using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreBackend.Api.Entities
{
    /// <summary>
    /// 用户实体模型
    /// </summary>
    public class Buyer
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int BuyerID { set; get; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Account { set; get; }
        public string Name { set; get; }
        public int Integral { set; get; }
        public string PW { set; get; }
        public string Tell { set; get; }
        public string Mail { set; get; }
        public string BusinessKey { set; get; }
        public DateTime LastTime { get; set ; }
       

    }
    public class BuyerConfiguration : IEntityTypeConfiguration<Buyer>
    {
        public void Configure(EntityTypeBuilder<Buyer> builder)
        {
          //  builder.HasKey(x => new { x.BuyerID });

            builder.HasKey(x => x.Account);
            
            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);

            builder.Property(x => x.Integral).IsRequired();
            builder.Property(x => x.PW).IsRequired().HasMaxLength(30);

            builder.Property(x => x.Tell).IsRequired().HasMaxLength(30);

            builder.Property(x => x.Mail).IsRequired().HasMaxLength(30);

            builder.Property(x => x.BusinessKey).IsRequired().HasMaxLength(30);

            builder.Property(x =>  x.LastTime).IsRequired().HasMaxLength(30);
            //builder.Property(x => x.LastTime).IsRequired().HasMaxLength(30);
   
   
        }
    }

}
