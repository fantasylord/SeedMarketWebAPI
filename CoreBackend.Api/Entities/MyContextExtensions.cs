using CoreBackend.Api.Entity;
using System.Collections.Generic;
using System.Linq;

namespace CoreBackend.Api.Entities
{
    public static class MyContextExtensions
    {
        public static void EnsureSeedDataForContext(this MyContext context)
        {
            if (context.Products.Any())
            {
                return;
            }
            var products = new List<Product>
            {
                new Product
                {
                    Name = "牛奶",
                    Price = 2.5f,
                    Description = "这是牛奶啊",
                    Materials = new List<Material>
                    {
                        new Material
                        {
                            Name = "水"
                        },
                        new Material
                        {
                            Name = "奶粉"
                        }
                    }
                },
                new Product
                {
                    Name = "面包",
                    Price = 4.5f,
                    Description = "这是面包啊",
                    Materials = new List<Material>
                    {
                        new Material
                        {
                            Name = "面粉"
                        },
                        new Material
                        {
                            Name = "糖"
                        }
                    }
                },
                new Product
                {
                    Name = "啤酒",
                    Price = 7.5f,
                    Description = "这是啤酒啊",
                    Materials = new List<Material>
                    {
                        new Material
                        {
                            Name = "麦芽"
                        },
                        new Material
                        {
                            Name = "地下水"
                        }
                    }
                }
            };
            //var buyers=new List<Buyer>
            //{
            //    new Buyer
            //    {
            //        Account="T001",
                    
            //    }
            //}
            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
