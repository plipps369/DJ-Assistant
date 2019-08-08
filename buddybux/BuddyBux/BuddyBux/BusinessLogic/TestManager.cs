using BuddyBux.DAO;
using BuddyBux.Models.Database;
using BuddyBux.Models;
using System.IO;
using System.Transactions;

namespace BuddyBux.BusinessLogic
{
    public static class TestManager
    {
        public static void PopulateDatabaseWithTradeStatus(IBuddyBuxDAO db)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                db.AddTradeStatusItem(new TradeStatusItem() { Status = "Pending" });
                db.AddTradeStatusItem(new TradeStatusItem() { Status = "Accepted" });
                db.AddTradeStatusItem(new TradeStatusItem() { Status = "Rejected" });

                scope.Complete();
            }
        }

        public static void PopulateDatabaseWithUserAccounts(IBuddyBuxDAO db)
        {
            PasswordManager passHelper = new PasswordManager("a");
            FileManager fileMgr = new FileManager(db);

            using (TransactionScope scope = new TransactionScope())
            {
                // Add roles to database
                int adminId = db.AddRoleItem(new RoleItem() { Name = RoleManager.ADMINISTRATOR });
                db.AddRoleItem(new RoleItem() { Name = RoleManager.USER });

                // Add user image file to database
                byte[] userImage = Properties.Resources.christopher;
                FileItem userFileItem = fileMgr.StoreFile(userImage, "Christopher.png");

                // Add currency image file to database
                byte[] currencyImage = Properties.Resources.Rupptastics;
                FileItem currencyFileItem = fileMgr.StoreFile(currencyImage, "Rupptastics.png");

                // Create user item
                UserItem userItem = new UserItem()
                {
                    FirstName = "Chris",
                    LastName = "Rupp",
                    Email = "chris@techelevator.com",
                    RoleId = adminId,
                    ImageFileId = userFileItem.Id
                };
                userItem.Hash = passHelper.Hash;
                userItem.Salt = passHelper.Salt;

                CurrencyItem currencyItem = new CurrencyItem();
                currencyItem.Name = "Rupptastics";
                currencyItem.ImageFileId = currencyFileItem.Id;

                UserAccountModel userAccount = new UserAccountModel();
                userAccount.User = userItem;
                userAccount.Currency = currencyItem;
                userAccount.Store = new StoreItem();

                db.AddUserAccount(userAccount);
                scope.Complete();
            }
            using (TransactionScope scope = new TransactionScope())
            {
                // Add roles to database
                int adminId = db.AddRoleItem(new RoleItem() { Name = RoleManager.ADMINISTRATOR });
                db.AddRoleItem(new RoleItem() { Name = RoleManager.USER });

                // Add user image file to database
                byte[] userImage = Properties.Resources.christopher;
                FileItem userFileItem = fileMgr.StoreFile(userImage, "ram.png");

                // Add currency image file to database
                byte[] currencyImage = Properties.Resources.Rupptastics;
                FileItem currencyFileItem = fileMgr.StoreFile(currencyImage, "fakemoney.png");

                // Create user item
                UserItem userItem = new UserItem()
                {
                    FirstName = "Chris",
                    LastName = "Ramstetter",
                    Email = "ramstetter@techelevator.com",
                    RoleId = adminId,
                    ImageFileId = userFileItem.Id,
                    Rating = 0
                };
                userItem.Hash = passHelper.Hash;
                userItem.Salt = passHelper.Salt;

                CurrencyItem currencyItem = new CurrencyItem();
                currencyItem.Name = "fake money";
                currencyItem.ImageFileId = currencyFileItem.Id;

                UserAccountModel userAccount = new UserAccountModel();
                userAccount.User = userItem;
                userAccount.Currency = currencyItem;
                userAccount.Store = new StoreItem();

                db.AddUserAccount(userAccount);
                scope.Complete();
            }
        }
    }

}

