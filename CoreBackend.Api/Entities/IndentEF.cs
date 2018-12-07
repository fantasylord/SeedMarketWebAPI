using CoreBackend.Api.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreBackend.Api.Entities
{
    /// <summary>
    /// 订单记录实体
    /// </summary>
    public class Indent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IndentID { set; get; }
  
        public string Account { set; get; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SeedID { set; get; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SellerID { set; get; }

        public float Price { set; get; }

        public int Count { set; get; }

        public float Amount { set; get; }


        public IndentStatus Status { set; get; }

        public DateTime CreatTIme { set; get; }

        public DateTime FinishedTime { set; get; }

    }
    public class IndentConfiguration : IEntityTypeConfiguration<Indent>
    {
        public void Configure(EntityTypeBuilder<Indent> builder)
        {
            builder.HasKey(post => new { post.IndentID, post.Account, post.SeedID,post.SellerID });
            builder.Property(x => x.Account).HasMaxLength(30);
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Count).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.CreatTIme).IsRequired();
            builder.Property(x => x.FinishedTime).IsRequired();
            //HasOne选择外键所在的表，withMany为设置表为1对多的关系，HasForeignKey是表里面的外键，OnDelete是外键删掉之后的处理
            

        }
    }
}
