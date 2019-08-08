using BuddyBux.BusinessLogic;
using BuddyBux.DAO;
using BuddyBux.Models.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using System.Text;
using System.IO;
using System;
using BuddyBux.Models;
using System.Collections.Generic;

namespace BuddyBuxTest
{
    [TestClass]
    public class IntegrationTests
    {
        //Used to begin a transaction during initialize and rollback during cleanup
        private TransactionScope _tran;

        private IBuddyBuxDAO _db = new BuddyBuxDAO(@"Data Source=.\SQLEXPRESS;Initial Catalog=BuddyBux;Integrated Security=true");
        private PasswordManager _passHelper = new PasswordManager("Abcd!234");
        private RoleManager _rollManager;
        UserItem _user = null;
        CurrencyItem _currency = null;

        public IntegrationTests()
        {
            _rollManager = new RoleManager(_db);
        }

        /// <summary>
        /// Set up the database before each test  
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            // Initialize a new transaction scope. This automatically begins the transaction.
            _tran = new TransactionScope();

            // Test add user
            _user = new UserItem();
            _user.FirstName = "Chris";
            _user.LastName = "Rupp";
            _user.Hash = _passHelper.Hash;
            _user.Salt = _passHelper.Salt;
            _user.Email = "chris@tech.com";
            _user.RoleId = _rollManager.AdministratorRoleId;
            _user.Rating = 3;
            _user.Id = _db.AddUserItem(_user);

            _currency = new CurrencyItem();
            _currency.Name = "Ball";
            _currency.OwnerId = _user.Id;
            _currency.Id = _db.AddCurrencyItem(_currency);
        }

        /// <summary>
        /// Cleanup runs after every single test
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            _tran.Dispose();
        }

        /// <summary>
        /// Tests the user POCO methods
        /// </summary>
        [TestMethod()]
        public void TestUser()
        {
            Assert.AreNotEqual(0, _user.Id);

            UserItem itemGet = _db.GetUserItemById(_user.Id);
            Assert.AreEqual(_user.Id, itemGet.Id);
            Assert.AreEqual(_user.FirstName, itemGet.FirstName);
            Assert.AreEqual(_user.LastName, itemGet.LastName);
            Assert.AreEqual(_user.Hash, itemGet.Hash);
            Assert.AreEqual(_user.Salt, itemGet.Salt);
            Assert.AreEqual(_user.Email, itemGet.Email);
            Assert.AreEqual(_user.Rating, itemGet.Rating);
            Assert.AreEqual(_user.RoleId, itemGet.RoleId);
            Assert.AreEqual(_user.ImageFileId, itemGet.ImageFileId);

            // Test update user
            UserItem item = new UserItem();
            item.Id = _user.Id;
            item.FirstName = "What";
            item.LastName = "What";
            item.Email = "What";
            item.Hash = "What";
            item.Salt = "What";
            item.Rating = 2;
            item.RoleId = _rollManager.UserRoleId;
            Assert.IsTrue(_db.UpdateUserItem(item));

            itemGet = _db.GetUserItemById(item.Id);
            Assert.AreEqual(item.Id, itemGet.Id);
            Assert.AreEqual(item.FirstName, itemGet.FirstName);
            Assert.AreEqual(item.LastName, itemGet.LastName);
            Assert.AreEqual(item.Hash, itemGet.Hash);
            Assert.AreEqual(item.Salt, itemGet.Salt);
            Assert.AreEqual(item.Email, itemGet.Email);
            Assert.AreEqual(item.Rating, itemGet.Rating);
            Assert.AreEqual(item.RoleId, itemGet.RoleId);
            Assert.AreEqual(item.ImageFileId, itemGet.ImageFileId);

            // Test Lookup by email
            itemGet = _db.GetUserItemByEmail("What");
            Assert.AreEqual(item.Id, itemGet.Id);
            Assert.AreEqual(item.FirstName, itemGet.FirstName);
            Assert.AreEqual(item.LastName, itemGet.LastName);
            Assert.AreEqual(item.Hash, itemGet.Hash);
            Assert.AreEqual(item.Salt, itemGet.Salt);
            Assert.AreEqual(item.Email, itemGet.Email);
            Assert.AreEqual(item.Rating, itemGet.Rating);
            Assert.AreEqual(item.RoleId, itemGet.RoleId);
            Assert.AreEqual(item.ImageFileId, itemGet.ImageFileId);

            // Test delete user
            _db.DeleteCurrencyItem(_currency.Id);
            _db.DeleteUserItem(item.Id);
            var users = _db.GetUserItems();
            foreach (var user in users)
            {
                Assert.AreNotEqual(item.Id, user.Id);
            }
        }

        /// <summary>
        /// Test Currency POCO methods
        /// </summary>
        [TestMethod()]
        public void TestCurrency()
        {
            //Test add currency
            
            Assert.AreNotEqual(0, _currency.Id);

            CurrencyItem itemGet = _db.GetCurrencyItemById(_currency.Id);
            Assert.AreEqual(_currency.Id, itemGet.Id);
            Assert.AreEqual(_currency.Name, itemGet.Name);
            Assert.AreEqual(_currency.OwnerId, itemGet.OwnerId);
            Assert.AreEqual(_currency.ImageFileId, itemGet.ImageFileId);


            // Testing UpdateCurrency
            _currency.Name = "BaseBall";

            Assert.IsTrue(_db.UpdateCurrencyItem(_currency));
            itemGet = _db.GetCurrencyItemById(_currency.Id);
            Assert.AreEqual(_currency.Id, itemGet.Id);
            Assert.AreEqual(_currency.Name, itemGet.Name);
            Assert.AreEqual(_currency.OwnerId, itemGet.OwnerId);
            Assert.AreEqual(_currency.ImageFileId, itemGet.ImageFileId);
            
        }

        /// <summary>
        /// Test UserCurrency POCO methods
        /// Making the assumtion that the database has the 2 default users
        /// </summary>
        [TestMethod()]
        public void TestUserCurrency()
        {
            UserItem user1 = _db.GetUserItemByEmail("chris@techelevator.com");
            UserItem user2 = _db.GetUserItemByEmail("ramstetter@techelevator.com");
            UserCurrencyItem userCurrency = new UserCurrencyItem();
            userCurrency.UserId = user1.Id;
            CurrencyItem currency = _db.GetCurrencyItemByOwnerId(user2.Id);
            userCurrency.CurrencyId = currency.Id;
            userCurrency.Amount = 1000;
            userCurrency.Id = _db.AddUserCurrency(userCurrency);

            UserCurrencyItem userCurrencyGet = _db.GetUserCurrencyById(userCurrency.Id);

            Assert.AreEqual(userCurrency.Id, userCurrencyGet.Id);
            Assert.AreEqual(userCurrency.UserId, userCurrencyGet.UserId);
            Assert.AreEqual(userCurrency.CurrencyId, userCurrencyGet.CurrencyId);
            Assert.AreEqual(userCurrency.Amount, userCurrencyGet.Amount);

            List<UserCurrencyItem> userCurrencyItemList = _db.GetOwnedCurrency(user1.Id);
            Assert.AreEqual(userCurrency.Id, userCurrencyItemList[0].Id);

            userCurrency.Amount = 94994;
            Assert.IsTrue(_db.UpdateUserCurrency(userCurrency));

            userCurrencyGet = _db.GetUserCurrencyById(userCurrency.Id);

            Assert.AreEqual(userCurrency.Id, userCurrencyGet.Id);
            Assert.AreEqual(userCurrency.UserId, userCurrencyGet.UserId);
            Assert.AreEqual(userCurrency.CurrencyId, userCurrencyGet.CurrencyId);
            Assert.AreEqual(userCurrency.Amount, userCurrencyGet.Amount);

            _db.DeleteUserCurrency(userCurrency.Id);
            userCurrencyItemList = _db.GetOwnedCurrency(user1.Id);
            Assert.AreEqual(0, userCurrencyItemList.Count);
           

        }

        /// <summary>
        /// Test the Product POCO methods
        /// </summary>
        [TestMethod()]
        public void TestProduct()
        {
            ProductItem product = new ProductItem();
            product.IsCharitable = false;
            product.Name = "Lama Cookies";
            product.CreatorId = _user.Id;
            product.Id = _db.AddProduct(product);

            ProductItem itemGet = _db.GetProductById(product.Id);

            Assert.AreEqual(product.Id, itemGet.Id);
            Assert.AreEqual(product.Name, itemGet.Name);
            Assert.AreEqual(product.IsCharitable, itemGet.IsCharitable);
            Assert.AreEqual(product.CreatorId, itemGet.CreatorId);
            Assert.AreEqual(product.ImageFileId, itemGet.ImageFileId);

            product.Name = "New Lama Cookies";
            product.IsCharitable = true;

            Assert.IsTrue(_db.UpdateProductItem(product));

            itemGet = _db.GetProductById(product.Id);

            Assert.AreEqual(product.Id, itemGet.Id);
            Assert.AreEqual(product.Name, itemGet.Name);
            Assert.AreEqual(product.IsCharitable, itemGet.IsCharitable);
            Assert.AreEqual(product.CreatorId, itemGet.CreatorId);
            Assert.AreEqual(product.ImageFileId, itemGet.ImageFileId);
            
            // Test delete user
            _db.DeleteProductItem(product.Id);
            var products = _db.GetProducts();
            foreach(var p in products)
            {
                Assert.AreNotEqual(product.Id, p.Id);
            }
        }

        /// <summary> 
        /// Test the Store POCO methods
        /// </summary>
        [TestMethod]
        public void TestStoreAndStoreProducts()
        {
            StoreItem store = new StoreItem();
            store.UserId = _user.Id;

            store.Id = _db.AddStore(store);

            ProductItem product = new ProductItem();
            product.IsCharitable = false;
            product.Name = "Lama Cookies";
            product.CreatorId = _user.Id;
            product.Id = _db.AddProduct(product);

            StoreProductItem storeProduct = new StoreProductItem();
            storeProduct.ProductId = product.Id;
            storeProduct.Qty = 100;
            storeProduct.StoreId = store.Id;
            storeProduct.CurrencyId = _currency.Id;

            storeProduct.Id = _db.AddStoreProductItem(storeProduct);

            StoreFrontModel storeFront = _db.GetStoreFront(_user.Id);

            //because we already know product is working we should not need to check everything
            Assert.AreEqual(store.Id, storeFront.Store.Id);
            Assert.AreEqual(store.UserId, storeFront.Store.UserId);
            Assert.AreEqual(1, storeFront.StoreProducts.Count);
            Assert.AreEqual(product.Id, storeFront.StoreProducts[0].ProductId);
            Assert.AreEqual(1, storeFront.Products.Count);
            Assert.AreEqual(product.Id, storeFront.Products[product.Id].Id);

            storeProduct.Qty = 83984;
            storeProduct.Cost = 9999;
            Assert.IsTrue(_db.UpdateStoreProductItem(storeProduct));
            storeFront = _db.GetStoreFront(_user.Id);

            //because we already know product is working we should not need to check everything
            Assert.AreEqual(store.Id, storeFront.Store.Id);
            Assert.AreEqual(store.UserId, storeFront.Store.UserId);
            Assert.AreEqual(1, storeFront.StoreProducts.Count);
            Assert.AreEqual(product.Id, storeFront.StoreProducts[0].ProductId);
            Assert.AreEqual(1, storeFront.Products.Count);
            Assert.AreEqual(product.Id, storeFront.Products[product.Id].Id);

            _db.RemoveProductFromStore(storeProduct.Id);
            var products = _db.GetStoreProductsItemsById(_user.Id);
            foreach(var p in products)
            {
                Assert.AreNotEqual(storeProduct.Id, p.Id);
            }

        }

        [TestMethod]
        public void TestUserAccount()
        {
            UserAccountModel model1 = new UserAccountModel();
            model1.User = _user;
            // must delete because its already in the database
            _db.DeleteCurrencyItem(_currency.Id);
            _db.DeleteUserItem(_user.Id);
            CurrencyItem currency = new CurrencyItem();

            currency.Name = "Ball";

            currency.OwnerId = _user.Id;
            model1.Currency = currency;

            StoreItem store = new StoreItem();
            store.UserId = _user.Id;

            model1.Store = store;

            UserAccountModel model2 = _db.AddUserAccount(model1);
            UserAccountModel model3 = _db.GetUserAccount(model2.User.Id);

            UserItem userItem = model1.User;
            UserItem userItemGet = model2.User;

            Assert.AreEqual(userItem.Id, userItemGet.Id);
            Assert.AreEqual(userItem.FirstName, userItemGet.FirstName);
            Assert.AreEqual(userItem.LastName, userItemGet.LastName);
            Assert.AreEqual(userItem.Hash, userItemGet.Hash);
            Assert.AreEqual(userItem.Salt, userItemGet.Salt);
            Assert.AreEqual(userItem.Email, userItemGet.Email);
            Assert.AreEqual(userItem.Rating, userItemGet.Rating);
            Assert.AreEqual(userItem.RoleId, userItemGet.RoleId);
            Assert.AreEqual(userItem.ImageFileId, userItemGet.ImageFileId);

            userItemGet = model3.User;

            Assert.AreEqual(userItem.Id, userItemGet.Id);
            Assert.AreEqual(userItem.FirstName, userItemGet.FirstName);
            Assert.AreEqual(userItem.LastName, userItemGet.LastName);
            Assert.AreEqual(userItem.Hash, userItemGet.Hash);
            Assert.AreEqual(userItem.Salt, userItemGet.Salt);
            Assert.AreEqual(userItem.Email, userItemGet.Email);
            Assert.AreEqual(userItem.Rating, userItemGet.Rating);
            Assert.AreEqual(userItem.RoleId, userItemGet.RoleId);
            Assert.AreEqual(userItem.ImageFileId, userItemGet.ImageFileId);

            CurrencyItem currencyItem = model1.Currency;
            CurrencyItem currencyItemGet = model2.Currency;
            Assert.AreEqual(currencyItem.Id, currencyItemGet.Id);
            Assert.AreEqual(currencyItem.Name, currencyItemGet.Name);
            Assert.AreEqual(currencyItem.OwnerId, currencyItemGet.OwnerId);
            Assert.AreEqual(currencyItem.ImageFileId, currencyItemGet.ImageFileId);

            currencyItemGet = model3.Currency;
            Assert.AreEqual(currencyItem.Id, currencyItemGet.Id);
            Assert.AreEqual(currencyItem.Name, currencyItemGet.Name);
            Assert.AreEqual(currencyItem.OwnerId, currencyItemGet.OwnerId);
            Assert.AreEqual(currencyItem.ImageFileId, currencyItemGet.ImageFileId);

            StoreItem storeItem = model1.Store;
            StoreItem storeItemGet = model2.Store;
            Assert.AreEqual(storeItem.Id, storeItemGet.Id);
            Assert.AreEqual(storeItem.UserId, storeItemGet.UserId);

        }

        [TestMethod]
        public void TestTrades()

        {
            TradeItem item = new TradeItem();

            item.OriginatorId = _user.Id;
            item.ProvidedCurrencyId = _currency.Id;
            item.ProvidedCurrencyQty = 45;
            item.RequestedCurrencyId = _currency.Id;
            item.RequestedCurrencyQty = 60;
            item.DesigneeId = _user.Id;
            // should this be a constant definded by class tradstatus
            item.TradeStatusId = 2;

            item.Id = _db.AddTradeItem(item);

            TradeItem itemGet = _db.GetTradeItem(item.Id);
            Assert.AreEqual(item.Id, itemGet.Id);
            Assert.AreEqual(item.OriginatorId, itemGet.OriginatorId);
            Assert.AreEqual(item.DesigneeId, itemGet.DesigneeId);
            Assert.AreEqual(item.ProvidedCurrencyId, itemGet.ProvidedCurrencyId);
            Assert.AreEqual(item.RequestedCurrencyId, itemGet.RequestedCurrencyId);
            Assert.AreEqual(item.ProvidedCurrencyQty, itemGet.ProvidedCurrencyQty);
            Assert.AreEqual(item.RequestedCurrencyQty, itemGet.RequestedCurrencyQty);
            Assert.AreEqual(item.TradeStatusId, itemGet.TradeStatusId);

            item.ProvidedCurrencyQty = 60;
            item.RequestedCurrencyQty = 90;
            item.TradeStatusId = 1;
            Assert.IsTrue(_db.UpdateTradeItem(item));
            itemGet = _db.GetTradeItem(item.Id);
            Assert.AreEqual(item.Id, itemGet.Id);
            Assert.AreEqual(item.OriginatorId, itemGet.OriginatorId);
            Assert.AreEqual(item.DesigneeId, itemGet.DesigneeId);
            Assert.AreEqual(item.ProvidedCurrencyId, itemGet.ProvidedCurrencyId);
            Assert.AreEqual(item.RequestedCurrencyId, itemGet.RequestedCurrencyId);
            Assert.AreEqual(item.ProvidedCurrencyQty, itemGet.ProvidedCurrencyQty);
            Assert.AreEqual(item.RequestedCurrencyQty, itemGet.RequestedCurrencyQty);
            Assert.AreEqual(item.TradeStatusId, itemGet.TradeStatusId);

            List<TradeItem> tradeItems = _db.GetTradeItemByOriginatorId(_user.Id);
            Assert.AreEqual(1, tradeItems.Count);
            itemGet = tradeItems[0];
            Assert.AreEqual(item.Id, itemGet.Id);
            Assert.AreEqual(item.OriginatorId, itemGet.OriginatorId);
            Assert.AreEqual(item.DesigneeId, itemGet.DesigneeId);
            Assert.AreEqual(item.ProvidedCurrencyId, itemGet.ProvidedCurrencyId);
            Assert.AreEqual(item.RequestedCurrencyId, itemGet.RequestedCurrencyId);
            Assert.AreEqual(item.ProvidedCurrencyQty, itemGet.ProvidedCurrencyQty);
            Assert.AreEqual(item.RequestedCurrencyQty, itemGet.RequestedCurrencyQty);
            Assert.AreEqual(item.TradeStatusId, itemGet.TradeStatusId);
            tradeItems = _db.GetTradeItemByDesigneeId(_user.Id);
            Assert.AreEqual(1, tradeItems.Count);
            itemGet = tradeItems[0];
            Assert.AreEqual(item.Id, itemGet.Id);
            Assert.AreEqual(item.OriginatorId, itemGet.OriginatorId);
            Assert.AreEqual(item.DesigneeId, itemGet.DesigneeId);
            Assert.AreEqual(item.ProvidedCurrencyId, itemGet.ProvidedCurrencyId);
            Assert.AreEqual(item.RequestedCurrencyId, itemGet.RequestedCurrencyId);
            Assert.AreEqual(item.ProvidedCurrencyQty, itemGet.ProvidedCurrencyQty);
            Assert.AreEqual(item.RequestedCurrencyQty, itemGet.RequestedCurrencyQty);
            Assert.AreEqual(item.TradeStatusId, itemGet.TradeStatusId);

            _db.DeleteTradeItem(item.Id);
            tradeItems = _db.GetTradeItemByDesigneeId(_user.Id);
            Assert.AreEqual(0, tradeItems.Count);


        }

        [TestMethod]
        public void TestTransactions()
        {
            StoreItem store = new StoreItem();
            store.UserId = _user.Id;

            store.Id = _db.AddStore(store);

            ProductItem product = new ProductItem();
            product.IsCharitable = false;
            product.Name = "Lama Cookies";
            product.CreatorId = _user.Id;
            product.Id = _db.AddProduct(product);

            StoreProductItem storeProduct = new StoreProductItem();
            storeProduct.ProductId = product.Id;
            storeProduct.Qty = 100;
            storeProduct.StoreId = store.Id;
            storeProduct.CurrencyId = _currency.Id;

            storeProduct.Id = _db.AddStoreProductItem(storeProduct);

            StoreFrontModel storeFront = _db.GetStoreFront(_user.Id);

            TransactionItem item = new TransactionItem();

            item.PurchaserId = _user.Id;
            item.Qty = 10;
            item.Cost = 5;
            item.CurrencyId = _currency.Id;
            item.StoreProductId = storeProduct.Id;

            item.Id = _db.AddTransactionItem(item);

            TransactionItem itemGet = _db.GetTransactionItemById(item.Id);

            Assert.AreEqual(item.Id, itemGet.Id);
            Assert.AreEqual(item.PurchaseDate.Date, itemGet.PurchaseDate.Date);
            Assert.AreEqual(item.PurchaserId, itemGet.PurchaserId);
            Assert.AreEqual(item.Qty, itemGet.Qty);
            Assert.AreEqual(item.StoreProductId, itemGet.StoreProductId);
            Assert.AreEqual(item.Cost, itemGet.Cost);

            item.Cost = 100;
            item.Qty = 90;

            Assert.IsTrue(_db.UpdateTransactionItem(item));

            itemGet = _db.GetTransactionItemById(item.Id);

            Assert.AreEqual(item.Id, itemGet.Id);
            Assert.AreEqual(item.PurchaseDate.Date, itemGet.PurchaseDate.Date);
            Assert.AreEqual(item.PurchaserId, itemGet.PurchaserId);
            Assert.AreEqual(item.Qty, itemGet.Qty);
            Assert.AreEqual(item.StoreProductId, itemGet.StoreProductId);
            Assert.AreEqual(item.Cost, itemGet.Cost);

            List<TransactionItem> transactionList = _db.GetTransactionItemByPurchaserId(item.PurchaserId);

            Assert.AreEqual(1, transactionList.Count);

            itemGet = transactionList[0];

            Assert.AreEqual(item.Id, itemGet.Id);
            Assert.AreEqual(item.PurchaseDate.Date, itemGet.PurchaseDate.Date);
            Assert.AreEqual(item.PurchaserId, itemGet.PurchaserId);
            Assert.AreEqual(item.Qty, itemGet.Qty);
            Assert.AreEqual(item.StoreProductId, itemGet.StoreProductId);
            Assert.AreEqual(item.Cost, itemGet.Cost);

            transactionList = _db.GetTransactionItemByStoreProductId(item.StoreProductId);

            Assert.AreEqual(1, transactionList.Count);

            itemGet = transactionList[0];

            Assert.AreEqual(item.Id, itemGet.Id);
            Assert.AreEqual(item.PurchaseDate.Date, itemGet.PurchaseDate.Date);
            Assert.AreEqual(item.PurchaserId, itemGet.PurchaserId);
            Assert.AreEqual(item.Qty, itemGet.Qty);
            Assert.AreEqual(item.StoreProductId, itemGet.StoreProductId);
            Assert.AreEqual(item.Cost, itemGet.Cost);

            transactionList = _db.GetTransactionItemByStoreId(storeProduct.StoreId);

            Assert.AreEqual(1, transactionList.Count);

            itemGet = transactionList[0];

            Assert.AreEqual(item.Id, itemGet.Id);
            Assert.AreEqual(item.PurchaseDate.Date, itemGet.PurchaseDate.Date);
            Assert.AreEqual(item.PurchaserId, itemGet.PurchaserId);
            Assert.AreEqual(item.Qty, itemGet.Qty);
            Assert.AreEqual(item.StoreProductId, itemGet.StoreProductId);
            Assert.AreEqual(item.Cost, itemGet.Cost);
        }

        /// <summary>
        /// Tests the File POCO methods
        /// </summary>
        [TestMethod()]
        public void TestFile()
        {
            // Test add file
            FileItem item = new FileItem();
            item.OriginalName = "baseball.jpg";
            item.StorageName = "12345678.bin";
            item.StoragePath = "C:\\Bob";
            item.Size = 100;
            item.Hash = "hashhashhashhash";
            int id = _db.AddFileItem(item);
            Assert.AreNotEqual(0, id);

            FileItem itemGet = _db.GetFileItemById(id);
            Assert.AreEqual(item.Id, itemGet.Id);
            Assert.AreEqual(item.OriginalName, itemGet.OriginalName);
            Assert.AreEqual(item.StorageName, itemGet.StorageName);
            Assert.AreEqual(item.StoragePath, itemGet.StoragePath);
            Assert.AreEqual(item.Size, itemGet.Size);
            Assert.AreEqual(item.Hash, itemGet.Hash);

            // Test update file
            item.OriginalName = "bottleCap.jpg";
            item.StorageName = "12345677.bin";
            item.StoragePath = "C:\\Bobby";
            item.Size = 110;
            item.Hash = "hashhahashasdfasdf";
            Assert.IsTrue(_db.UpdateFileItem(item));

            itemGet = _db.GetFileItemById(id);
            Assert.AreEqual(item.Id, itemGet.Id);
            Assert.AreEqual(item.OriginalName, itemGet.OriginalName);
            Assert.AreEqual(item.StorageName, itemGet.StorageName);
            Assert.AreEqual(item.StoragePath, itemGet.StoragePath);
            Assert.AreEqual(item.Size, itemGet.Size);
            Assert.AreEqual(item.Hash, itemGet.Hash);

            // Test delete file
            _db.DeleteFileItem(id);
            var files = _db.GetFileItems();
            foreach (var file in files)
            {
                Assert.AreNotEqual(id, file.Id);
            }
        }

        [TestMethod()]
        public void TestFileManager()
        {
            const string tempStoragePath = @"C:\BuddyBux\Test";
            try
            {
                // Setup test data
                byte[] baseball = Properties.Resources.baseball;
                Directory.CreateDirectory(tempStoragePath);
                string testFilePath = Path.Combine(tempStoragePath, "test.png");
                File.WriteAllBytes(testFilePath, baseball);

                // Store file data
                FileManager mgr = new FileManager(_db, tempStoragePath);
                var baseballFileItem = mgr.StoreFile(baseball, "baseball.png");
                var testFileItem = mgr.StoreFile(testFilePath);

                // Retreive file data
                var baseballFileModel = mgr.GetFile(baseballFileItem.Id);
                var testFileModel = mgr.GetFile(testFileItem.Id);

                // Hash retrieved data for test comparison
                string baseballHash = FileManager.HashFile(baseballFileModel.Data);
                string testHash = FileManager.HashFile(testFileModel.Data);

                // Verify test data with retreived data
                Assert.AreEqual(baseballFileItem.Hash, baseballHash, "Testing File Data");
                Assert.AreEqual(baseballFileItem.OriginalName, baseballFileModel.Name, "Testing File Name");

                Assert.AreEqual(testFileItem.Hash, testHash, "Testing File Data");
                Assert.AreEqual(testFileItem.OriginalName, testFileModel.Name, "Testing File Name");
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                // Remove test directory
                Directory.Delete(tempStoragePath, true);
            }
        }
    }
}
