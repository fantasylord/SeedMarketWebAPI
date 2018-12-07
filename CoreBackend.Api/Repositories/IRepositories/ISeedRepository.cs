using CoreBackend.Api.Dtos;
using CoreBackend.Api.Entities;
using System.Collections.Generic;

namespace CoreBackend.Api.Repositories.IRepositories
{
    public interface ISeedRepository
    {

        //Buyer API
        Buyer GetBuyer(string account);

        IEnumerable<Buyer> GetBuyers();

        void AddBuyer(Buyer buyer);

        void DeletBuyer(Buyer buyer);

        //SeedAPI

        Seed GetSeed(int  seedID);

        IEnumerable<Seed> GetSeeds( int  sellerID=-1 , string brand = null);

        void AddSeed(Seed seed);

        void DeleteSeed(Seed seed);

        //SellerAPI

        Seller GetSeller(int  sellerID);

        IEnumerable<Seller> GetSellers(int status = 0);

        void AddSeller(Seller seller);

        void DeleteSeller(Seller seller);

        //GetIndentAPI

        Indent GetIndent(int indentID,  int seedID,int sellerID, string account);
        Indent GetIndentForId(int indentID);

        IEnumerable<Indent> GetIndents(string account , IndentStatus status = IndentStatus.异常);

        void AddIndent(Indent indent);
        void AddIndent(List<Indent> indents);
    

        
        void DeleteIndent(Indent indent);
        void DeleteIndent(int indentID);

        //InventoryEF

        Inventory GetInventory(int sellerID = -1, int seedID = -1);

        IEnumerable<Inventory> GetInventories(int sellerID = -1, int seedID = -1);

        void AddInventory(Inventory inventory);
        void AddInventory(List<Inventory> inventorys);


        void DeleteInventory(Inventory inventory);
        void DeleteInventory(List<Inventory> inventories);

        //SellInformationAPI

        SellInformation GetSellInformation(int SelledID);

        IEnumerable<SellInformation> GetSellInformations(string account,int indentid );

        void AddSellInformation(SellInformation sellInformation);

        void DeleteSellInformation(SellInformation sellInformation);
        //----

        void AddUserStatus(UserStatus userStatus);

        UserStatus GetUserStatus(string account, int sellerid, int seedid);
   
        IEnumerable<UserStatus> GetUserStatus(string account);

        void DeleteUserStatus(UserStatus userStatu);

        void DeleteUserStatus(List<UserStatus> userStatus);
        //----
        void AddFile(FileUpDownload fileUpDownload);
        void DeleteFile(FileUpDownload fileUpDownload);
        FileUpDownload GetFile(int seedID);
        IEnumerable<FileUpDownload> GetFiles(Dictionary<int , int > seedIDandseller);

        bool Save();
    }
}
