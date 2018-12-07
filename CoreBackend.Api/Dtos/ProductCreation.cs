using System.ComponentModel.DataAnnotations;

namespace CoreBackend.Api.Dtos
{
    public class ProductCreation
    {
        [Display(Name="产品名称")]
        [Required(ErrorMessage ="{0}是必填项")]
        [StringLength(10,MinimumLength =2,ErrorMessage ="{0}'s length between 2 and 10")]
        public string Name { get; set; }

        [Display(Name="价格")]
        [Range(0,99.99,ErrorMessage ="价格必须在0到99.99之间")]
        public float Price { get; set; }

        [Display(Name="描述")]
        [MaxLength(100,ErrorMessage ="{0}的长度不可以超过100")]
        public string Description { get; set; } = "value";
    }
}
