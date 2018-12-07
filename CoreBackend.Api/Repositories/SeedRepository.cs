using CoreBackend.Api.Dtos;
using CoreBackend.Api.Entities;
using CoreBackend.Api.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreBackend.Api.Repositories
{
    public class SeedRepository : ISeedRepository
    {
        private readonly ProductContext _myContext;
        public SeedRepository(ProductContext myContext)
        {
            _myContext = myContext;
        }
        //BuyerAPI
        public void AddBuyer(Buyer buyer)
        {

            try
            {
                _myContext.Buyers.Add(buyer);
            }
            catch (Exception e)
            {
                string s = e.ToString();
                throw;
            }
        }

        public void DeletBuyer(Buyer buyer)
        {
            _myContext.Buyers.Remove(buyer);
        }

        public Buyer GetBuyer(string account)
        {

            return _myContext.Buyers.Find(account);
        }

        public IEnumerable<Buyer> GetBuyers()
        {
            //在分页前先要是用OrderBy或者OrderByDescending对数据进行正序或者倒序然后在skip（）跳过多少条，take（）查询多少
            //db.User.OrderBy(u => u.ID).Skip(0).Take(5).ToList().ForEach(c=>Console.WriteLine(c.ID));
            // return _myContext.Buyers.OrderBy(x => x.Account).Skip(0).Take(100).ToList<Buyer>();
            return _myContext.Buyers.OrderBy(x => x.Account).ToList<Buyer>();
        }


        //Seller
        public void AddSeller(Seller seller)
        {
            _myContext.Sellers.Add(seller);
        }

        public Seller GetSeller(int sellerID)
        {
            return _myContext.Sellers.Find(sellerID);
        }

        public void DeleteSeller(Seller seller)
        {
            _myContext.Sellers.Remove(seller);
        }

        public IEnumerable<Seller> GetSellers(int status = 0)
        {
            return _myContext.Sellers.OrderBy(x => x.SellerID).ToList<Seller>();
        }


        //IndentAPI
        public void AddIndent(Indent indent)
        {

            if (_myContext.Buyers.Find(indent.Account) != null || _myContext.Seeds.Find(indent.SeedID) != null)
                _myContext.Indents.Add(indent);

        }
        public void AddIndent(List<Indent> indents)
        {
            _myContext.Indents.AddRange(indents);
        }
        public void DeleteIndent(Indent indent)
        {
            _myContext.Indents.Remove(indent);
        }
        public void DeleteIndent(int indentID)
        {
            ;
        }

        public Indent GetIndent(int indentID, int seedID,int sellerid, string account)
        {
            return _myContext.Indents.Find(indentID, account, seedID,sellerid);
        }
        public Indent GetIndentForId(int indentID)
        {
            return _myContext.Indents.SingleOrDefault(x => x.IndentID == indentID);
        }

        public IEnumerable<Indent> GetIndents(string account,IndentStatus status=IndentStatus.所有状态)
        {
            if (status ==IndentStatus.所有状态)
                return _myContext.Indents.Where(x => x.Account == account).OrderBy(x => x.Account).ToList<Indent>();
            else
                return _myContext.Indents.Where(x => x.Account.Equals(account)).OrderBy(x => x.Account).ToList<Indent>();
        }

        //InventoryAPI
        public void AddInventory(Inventory inventory)
        {
            _myContext.Inventorys.Add(inventory);
        }
        public void AddInventory(List<Inventory> inventorys)
        {
            _myContext.AddRange(inventorys);
        }
        public void DeleteInventory(Inventory inventory)
        {
            _myContext.Inventorys.Remove(inventory);
        }
        public void DeleteInventory(List<Inventory> inventorys)
        {
            _myContext.Inventorys.RemoveRange(inventorys);
        }
        public IEnumerable<Inventory> GetInventories(int sellerID = -1, int seedID = -1)
        {
            if (seedID == -1 && sellerID != -1)
                return _myContext.Inventorys.Where(x => x.SellerID == sellerID).OrderBy(x => x.SellerID).ToList<Inventory>();
            if (sellerID == -1 && seedID != -1)
                return _myContext.Inventorys.Where(x => x.SeedID == seedID).OrderBy(x => x.SeedID).ToList<Inventory>();
            if (sellerID != -1 && seedID != -1)
                return _myContext.Inventorys.Where(x => x.SellerID == sellerID).Where(x => x.SeedID == seedID).ToList<Inventory>();
            return _myContext.Inventorys.OrderBy(x => x.SeedID).ToList<Inventory>();
        }

        public Inventory GetInventory(int sellerID = -1, int seedID = -1)
        {
            if (sellerID > 0 && seedID > 0)
                return _myContext.Inventorys.Where(x=>x.SeedID==seedID&&x.SellerID==sellerID).FirstOrDefault();
            else
                return null;
        }
        //SeedAPI
        public void AddSeed(Seed seed)
        {
            _myContext.Seeds.Add(seed);
        }
        public void DeleteSeed(Seed seed)
        {
            _myContext.Seeds.Remove(seed);
        }
        public Seed GetSeed(int seedID)
        {
            var result = _myContext.Seeds.Where(x => x.SeedID==seedID).FirstOrDefault();
            if (result == null)
                return null;
            return result;
        }

        public IEnumerable<Seed> GetSeeds(int sellerID = -1, string brand = "")
        {

            if (sellerID < 0 && brand != "")
                //item => item.CategoryName.Contains("t")).ToList();
                return _myContext.Seeds.Where(x => x.Brand.Contains(brand)).OrderBy(x => x.SeedID).ToList<Seed>();

            else if (sellerID > 0 && brand != "")
                return _myContext.Seeds.Where(x => x.SellerID == sellerID).Where(x => x.Brand.Contains(brand)).OrderBy(x => x.SellerID).ToList<Seed>();

            else if (sellerID < 0 && brand == "")
                return _myContext.Seeds.OrderBy(x => x.SeedID).ToList<Seed>();
            else        // (sellerID > 0 && brand == "")
                return _myContext.Seeds.Where(x => x.SellerID == sellerID).OrderBy(x => x.SeedID).ToList<Seed>();

        }

        //SellInformatrion
        public SellInformation GetSellInformation(int SelledID)
        {
            
            return _myContext.SellInformations.Where(x => x.SelledID == SelledID).FirstOrDefault();
        }

        public IEnumerable<SellInformation> GetSellInformations(string account, int indentid)
        {
            if(indentid>0)
            return _myContext.SellInformations.Where(x =>x.Account == account && x.IndentID == indentid).OrderBy(x=>x.SeedID).ToList<SellInformation>();
            return _myContext.SellInformations.Where(x => x.Account == account).OrderBy(x => x.IndentID).ToList();
        }

        public void AddSellInformation(SellInformation sellInformation)
        {
            _myContext.Add(sellInformation);
        }

        public void DeleteSellInformation(SellInformation sellInformation)
        {
            _myContext.Remove(sellInformation);
        }

        //------

      




        public void AddUserStatus(UserStatus userStatus)
        {
            _myContext.userStatuses.Add(userStatus);
        }

        public IEnumerable<UserStatus> GetUserStatus(string  Account)
        {
          return  _myContext.userStatuses.Where(x => x.Account == Account).OrderBy(x => x.Account).ToList<UserStatus>();
        }
        public void DeleteUserStatus(UserStatus userStatu)
        {
            _myContext.userStatuses.Remove(userStatu);
        }
        public void DeleteUserStatus(List<UserStatus> userStatus)
        {
            _myContext.userStatuses.RemoveRange(userStatus);
        }
        public UserStatus GetUserStatus(string account, int sellerid, int seedid)
        {

         return    _myContext.userStatuses.Find(account, seedid, seedid);
        }

        //------
        public void AddFile(FileUpDownload fileUpDownload)
        {
            _myContext.fileUpDownloads.Add(fileUpDownload);
        }
        public void DeleteFile(FileUpDownload fileUpDownload)
        {
            _myContext.fileUpDownloads.Remove(fileUpDownload);
        }
       public FileUpDownload GetFile(int seedID)
        {
          return  _myContext.fileUpDownloads.Where(x => x.seed.SeedID == seedID).FirstOrDefault();
        }

        public IEnumerable<FileUpDownload> GetFiles(Dictionary<int ,int> seedIDandseller)
        {
            //u=>list.Contains(u.id)
            return _myContext.fileUpDownloads.Where(x => seedIDandseller.Keys.Contains(x.seed.SeedID) && seedIDandseller.Values.Contains(x.FID)).ToList();
        }


        public bool Save()
        {
            return _myContext.SaveChanges() >= 0;
        }


    }
}
