using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreBackend.Api.Entities
{
    /// <summary>
    /// 用户状态存储
    /// </summary>
    public class UserStatus
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int BuyerID { set; get; }
        public string Account { set; get; }


        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int  SeedID{ get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SellerID { set; get; }
        public int Count { get; set; }
        public float Price { get; set; }
        
    }
    public class UserStatusConfiguration : IEntityTypeConfiguration<UserStatus>
    {
        public void Configure(EntityTypeBuilder<UserStatus> builder)
        {
            //  builder.HasKey(x => new { x.BuyerID });
            builder.Property(x => x.Account).HasMaxLength(30);
            builder.HasKey(x => new { x.Account, x.SeedID, x.SellerID });
     
            builder.Property(x => x.Count).IsRequired();
            builder.Property(x => x.Price).IsRequired();
        
            //builder.Property(x => x.LastTime).IsRequired().HasMaxLength(30);


        }
    }
}
