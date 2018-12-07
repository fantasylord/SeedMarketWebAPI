using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreBackend.Api.Entities
{
    /// <summary>
    /// 库存表实体
    /// </summary>
    public class Inventory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SeedID { set; get; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SellerID { set; get; }

        public int Count { set; get; }
        /// <summary>
        /// 库存总量
        /// </summary>
        public int SumCount { set; get; }
       

    }
    public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.HasKey(post => new { post.SeedID, post.SellerID });
            builder.Property(p => p.Count).IsRequired();
            builder.Property(p => p.SumCount).IsRequired();
       
            //HasOne选择外键所在的表，withMany为设置表为1对多的关系，HasForeignKey是表里面的外键，OnDelete是外键删掉之后的处理
    
        
        }
    }
}
