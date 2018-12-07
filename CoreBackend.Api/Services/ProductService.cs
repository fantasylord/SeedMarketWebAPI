using CoreBackend.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBackend.Api.Services
{
    public class ProductService
    {
        public static ProductService Current { get; } = new ProductService();
        public List<ProductDot> Products { get; }

        private ProductService()
        {
            Products = new List<ProductDot>
            {
                new ProductDot
                {
                    ID = 1,
                    Name = "牛奶",
                    Price = 2.5f,
                    Materials = new List<MaterialDto>
                    {
                        new MaterialDto
                        {
                            ID = 1,
                            Name = "水"
                        },
                        new MaterialDto
                        {
                            ID = 2,
                            Name = "奶粉"
                        }
                    }
                },
                new ProductDot
                {
                    ID = 2,
                    Name = "面包",
                    Price = 4.5f,
                    Materials = new List<MaterialDto>
                    {
                        new MaterialDto
                        {
                            ID = 3,
                            Name = "面粉"
                        },
                        new MaterialDto
                        {
                            ID = 4,
                            Name = "糖"
                        }
                    }
                },
                new ProductDot
                {
                    ID = 3,
                    Name = "啤酒",
                    Price = 7.5f,
                    Materials = new List<MaterialDto>
                    {
                        new MaterialDto
                        {
                            ID = 5,
                            Name = "麦芽"
                        },
                        new MaterialDto
                        {
                            ID = 6,
                            Name = "地下水"
                        }
                    }
                }
            };
        }
    }
}
