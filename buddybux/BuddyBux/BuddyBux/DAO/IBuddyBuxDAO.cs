using BuddyBux.Models;
using BuddyBux.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuddyBux.DAO
{
    public interface IBuddyBuxDAO
    {
        #region User
        int AddUserItem(UserItem item);
        bool UpdateUserItem(UserItem item);
        bool DeleteUserItem(int userId);
        UserItem GetUserItemById(int userId);
        UserItem GetUserItemByEmail(string email);
        List<UserItem> GetUserItems();
        #endregion

        #region Role
        int AddRoleItem(RoleItem item);
        List<RoleItem> GetRoleItems();
        #endregion

        #region TradeStatus
        int AddTradeStatusItem(TradeStatusItem item);
        List<TradeStatusItem> GetTradeStatusItems();
        #endregion

        #region Trade

        int AddTradeItem(TradeItem item);
        bool UpdateTradeItem(TradeItem item);
        bool DeleteTradeItem(int tradeId);
        TradeItem GetTradeItem(int tradeId);
        List<TradeItem> GetTradeItemByOriginatorId(int originatorId);
        List<TradeItem> GetTradeItemByDesigneeId(int designeeId);

        #endregion

        #region File
        int AddFileItem(FileItem item);
        bool UpdateFileItem(FileItem item);
        bool DeleteFileItem(int id);
        FileItem GetFileItemById(int id);
        List<FileItem> GetFileItems();
        #endregion

        #region Currency
        int AddCurrencyItem(CurrencyItem item);
        bool UpdateCurrencyItem(CurrencyItem item);
        bool DeleteCurrencyItem(int currecnyId);
        CurrencyItem GetCurrencyItemByOwnerId(int userId);
        CurrencyItem GetCurrencyItemById(int id);
        List<UserCurrencyItem> GetOwnedCurrency(int userId);
        #endregion

        #region UserAccount
        UserAccountModel AddUserAccount(UserAccountModel model);
        bool UpdateUserAccount(UserAccountModel model);
        UserAccountModel GetUserAccount(int userId);
        #endregion

        #region Product
        int AddProduct(ProductItem item);
        bool UpdateProductItem(ProductItem item);
        bool DeleteProductItem(int id);
        ProductItem GetProductById(int id);
        List<ProductItem> GetProducts();
        #endregion

        #region StoreFront
        int AddStore(StoreItem item);
        int AddStoreProductItem(StoreProductItem item);
        bool UpdateStoreProductItem(StoreProductItem item);
        StoreItem GetStoreItemById(int userId);
        bool RemoveProductFromStore(int storeProductItemId);
        StoreFrontModel GetStoreFront(int userId);
        List<StoreProductItem> GetStoreProductsItemsById(int storeId);
        StoreProductItem GetStoreProductItemById(int storeProductItem);
        #endregion

        #region UserCurrency

        int AddUserCurrency(UserCurrencyItem item);
        bool UpdateUserCurrency(UserCurrencyItem item);
        bool DeleteUserCurrency(int id);
        UserCurrencyItem GetUserCurrencyById(int id);
        UserCurrencyItem GetUserCurrencyByUserIdCurrencyId(int userId, int currencyId);

        #endregion

        #region Transaction

        int AddTransactionItem(TransactionItem item);
        bool UpdateTransactionItem(TransactionItem item);
        bool DeleteTransactionItem(int transactionId);
        TransactionItem GetTransactionItemById(int transactionId);
        List<TransactionItem> GetTransactionItemByStoreProductId(int storeProductId);
        List<TransactionItem> GetTransactionItemByPurchaserId(int purchaserId);
        List<TransactionItem> GetTransactionItemByStoreId(int storeId);

        #endregion
    }
}
