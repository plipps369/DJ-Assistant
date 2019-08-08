using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuddyBux.BusinessLogic
{
    public enum eTransactionTypeId
    {
        ReceivedCurrency = 1,
        SentCurrency = 2,
        PurchasedItem = 3,
        SoldItem = 4,
        PurchaseComplete = 5,
        PurchaseContested = 6
    }

    public static class TransactionManager
    {
        //AddTransactionType("Received Currency");
        //AddTransactionType("Sent Currency");
        //AddTransactionType("Purchased Item");
        //AddTransactionType("Sold Item");
        //AddTransactionType("Purchase Complete");
        //AddTransactionType("Purchase Contested");
        
        //public static void CurrencyReceivedTransaction(int recievedAmt, int fromUserId, int toUserId, ICryptDAL dal)
        //{
        //    var fromUserCurrency = dal.GetUserCurrencyByCurrencyOwner(fromUserId);
        //    var toUser = dal.GetUserById(toUserId);
        //    TransactionItem item = new TransactionItem();
        //    item.CurrencyId = fromUserCurrency.Currency.Id;
        //    item.Description = $"{toUser.FirstName} recieved {recievedAmt} {fromUserCurrency.Currency.CurrencyName} " +
        //                       $"from {fromUserCurrency.Owner.FirstName}.";
        //    item.MarketItemId = BaseItem.InvalidId;
        //    item.Qty = recievedAmt;
        //    item.TransactionDate = DateTime.Now;
        //    item.TypeId = (int) eTransactionTypeId.ReceivedCurrency;
        //    item.UserId = toUserId;
        //    dal.AddTransactionItem(item);
        //}

        //public static void CurrencySentTransaction(int sentAmount, int fromUserId, int toUserId, ICryptDAL dal)
        //{
        //    var fromUserCurrency = dal.GetUserCurrencyByCurrencyOwner(fromUserId);
        //    var toUser = dal.GetUserById(toUserId);
        //    TransactionItem item = new TransactionItem();
        //    item.CurrencyId = fromUserCurrency.Currency.Id;
        //    item.Description = $"{sentAmount} {fromUserCurrency.Currency.CurrencyName} " +
        //                       $"was sent from {fromUserCurrency.Owner.FirstName} to {toUser.FirstName}.";
        //    item.MarketItemId = BaseItem.InvalidId;
        //    item.Qty = sentAmount;
        //    item.TransactionDate = DateTime.Now;
        //    item.TypeId = (int) eTransactionTypeId.SentCurrency;
        //    item.UserId = fromUserId;
        //    dal.AddTransactionItem(item);
        //}

        //public static void PurchasedItemTransaction(int cost, int purchaserUserId, int sellerUserId, int marketItemId, ICryptDAL dal)
        //{
        //    var sellerUserCurrency = dal.GetUserCurrencyByCurrencyOwner(sellerUserId);
        //    var purchaser = dal.GetUserById(purchaserUserId);
        //    var marketItem = dal.GetMarketItem(marketItemId);
        //    TransactionItem item = new TransactionItem();
        //    item.CurrencyId = sellerUserCurrency.Currency.Id;
        //    item.Description = $"{purchaser.FirstName} bought the {marketItem.Name} " +
        //                       $"from {sellerUserCurrency.Owner.FirstName} for {cost} {sellerUserCurrency.Currency.CurrencyName}s.";
        //    item.MarketItemId = marketItemId;
        //    item.Qty = cost;
        //    item.TransactionDate = DateTime.Now;
        //    item.TypeId = (int)eTransactionTypeId.PurchasedItem;
        //    item.UserId = purchaserUserId;
        //    dal.AddTransactionItem(item);
        //}

        //public static void SoldItemTransaction(int cost, int purchaserUserId, int sellerUserId, int marketItemId, ICryptDAL dal)
        //{
        //    var sellerUserCurrency = dal.GetUserCurrencyByCurrencyOwner(sellerUserId);
        //    var purchaser = dal.GetUserById(purchaserUserId);
        //    var marketItem = dal.GetMarketItem(marketItemId);
        //    TransactionItem item = new TransactionItem();
        //    item.CurrencyId = sellerUserCurrency.Currency.Id;
        //    item.Description = $"{sellerUserCurrency.Owner.FirstName} sold the {marketItem.Name} " +
        //                       $"to {purchaser.FirstName} for {cost} {sellerUserCurrency.Currency.CurrencyName}s.";
        //    item.MarketItemId = marketItemId;
        //    item.Qty = cost;
        //    item.TransactionDate = DateTime.Now;
        //    item.TypeId = (int)eTransactionTypeId.SoldItem;
        //    item.UserId = sellerUserId;
        //    dal.AddTransactionItem(item);
        //}
    }
}
