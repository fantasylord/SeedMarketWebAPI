using CoreBackend.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreBackend.Api.Entities
{
    public static class ProductContextExtensions
    {
        public static void EnsureSeedDataForContext(this ProductContext context)
        {
            if (context.Buyers.Any() || context.Sellers.Any() || context.Seeds.Any())
            {
                return;
            }

            List<Buyer> buyers = new List<Buyer>
            {
                new Buyer
                {
                    Name="张三",
                    Account="Testone",
                    PW="Testone",
                    Tell="8888",
                    Mail="123@qq.com",
                   BusinessKey="蔬菜,水果",
                   LastTime=Convert.ToDateTime("2018 - 05 - 26T16: 16:12.761Z")

                },
                new Buyer
                {
                    Name="张三",
                    Account="Testone1",
                    PW="Testone",
                    Tell="8888",
                    Mail="123@qq.com",
                   BusinessKey="蔬菜,水果",
                   LastTime=Convert.ToDateTime("2018 - 05 - 26T16: 16:12.761Z")

                },
                new Buyer
                {
                    Name="张三",
                    Account="Testone2",
                    PW="Testone",
                    Tell="8888",
                    Mail="123@qq.com",
                   BusinessKey="蔬菜,水果",
                   LastTime=Convert.ToDateTime("2018 - 05 - 26T16: 16:12.761Z")

                },

            };
            context.Buyers.AddRange(buyers);
            context.SaveChanges();

            List<Seller> sellers = new List<Seller>
            {
                new Seller
                {
                    Detail="卖什么",
                    IsStatus=BusinessStatus.营业中,
                    MarkerID="10020001",
                    MarkerName="天字第一号",
                    Name="小王",

        },

                                new Seller
                {
                    Detail="卖什么",
                   IsStatus=BusinessStatus.营业中,
                    MarkerID="10020002",
                    MarkerName="地字第一号",
                    Name="小李",

        },
                new Seller
                {
                    Detail="卖什么",
                    IsStatus=BusinessStatus.营业中,
                    MarkerID="10020003",
                    MarkerName="玄字第一号",
                    Name="小张",

        },
                new Seller
                {
                    Detail="卖什么",
                    IsStatus=BusinessStatus.营业中,
                    MarkerID="10020004",
                    MarkerName="黄字第一号",
                    Name="小二",

        },

            };
            context.Sellers.AddRange(sellers);
            context.SaveChanges();


            List<Seed> seeds = new List<Seed>
            {
               new Seed
               {
                Brand="大地",
                SellerID=sellers[0].SellerID,
                SeedClass=SeedClass.大田作物类,
                Details="好吃的水果",
                Exhibitions="展示信息",
                Name="种子名称",
                Price=8.8f,
                MakertID=sellers[0].MarkerID,
                Species=SpeciesClass.叶菜类
               },

               new Seed
               {
                Brand="大地",
                SellerID=sellers[0].SellerID,
                SeedClass=SeedClass.大田作物类,
                Details="好吃的水果",
                Exhibitions="展示信息",
                Name="种子名称",
                MakertID=sellers[0].MarkerID,
                Price=8.8f,
                Species=SpeciesClass.叶菜类
               },

               new Seed
               {
                Brand="大地",
                SellerID=sellers[1].SellerID,
                SeedClass=SeedClass.大田作物类,
                Details="好吃的水果",
                Exhibitions="展示信息",
                Name="种子名称",
                MakertID=sellers[1].MarkerID,
                Price=8.8f,
                Species=SpeciesClass.叶菜类
               },

               new Seed
               {
                Brand="大地",
                SellerID=sellers[1].SellerID,
                SeedClass=SeedClass.大田作物类,
                Details="好吃的水果",
                Exhibitions="展示信息",
                Name="种子名称",
                MakertID=sellers[1].MarkerID,
                Price=8.8f,
                Species=SpeciesClass.叶菜类,

               },

            };
            context.Seeds.AddRange(seeds);
            context.SaveChanges();


            List<Indent> indents = new List<Indent>
            {
                new Indent
                {
                    Account=buyers[0].Account,

                    Count=10,
                    Price=seeds[0].Price,
                    Amount=10 * seeds[0].Price,
                    CreatTIme=System.DateTime.Now,
                    FinishedTime=System.DateTime.Now,
                    Status=IndentStatus.创建,
                    SeedID=seeds[0].SeedID,
                     SellerID=seeds[0].SellerID
                },
                new Indent
                {
                    Account=buyers[1].Account,
                      Count=10,
                      Price=seeds[0].Price,
                    Amount=10 * seeds[0].Price,
                    CreatTIme=System.DateTime.Now,
                    FinishedTime=System.DateTime.Now,
                    SeedID=seeds[0].SeedID,
                    Status=IndentStatus.创建,
                     SellerID=seeds[0].SellerID
                },
                new Indent
                {
                    Account=buyers[1].Account,
                        Count=10,
                      Price=seeds[0].Price,
                    Amount=10 * seeds[0].Price,
                    CreatTIme=System.DateTime.Now,
                    FinishedTime=System.DateTime.Now,
                    SeedID=seeds[0].SeedID,
                    Status=IndentStatus.创建,
                       SellerID=seeds[0].SellerID
                },
                new Indent
                {
                    Account=buyers[0].Account,
                          Count=10,
                      Price=seeds[2].Price,
                    Amount=10 * seeds[2].Price,
                    CreatTIme=System.DateTime.Now,
                    FinishedTime=System.DateTime.Now,
                    SeedID=seeds[2].SeedID,
                    Status=IndentStatus.创建,
                    SellerID=seeds[2].SellerID
                },

            };
            context.Indents.AddRange(indents);
            context.SaveChanges();



            List<Inventory> inventorys = new List<Inventory>
            {
                new Inventory
                {
                    Count=100,
                    SeedID=seeds[0].SeedID,
                    SellerID=sellers[0].SellerID,

                    SumCount=100,

                } ,               new Inventory
                {
                    Count=100,
                    SeedID=seeds[1].SeedID,
                    SellerID=sellers[1].SellerID,
                    SumCount=100
                }  ,              new Inventory
                {
                    Count=100,
                    SeedID=seeds[2].SeedID,
                    SellerID=sellers[2].SellerID,
                    SumCount=100
                }  ,              new Inventory
                {
                    Count=100,
                    SeedID=seeds[3].SeedID,
                    SellerID=sellers[3].SellerID,
                    SumCount=100
                }
            };
            context.Inventorys.AddRange(inventorys);
            context.SaveChanges();


            //var buyers=new List<Buyer>
            //{
            //    new Buyer
            //    {
            //        Account="T001",

            //    }
            //}



        }
    }
}
