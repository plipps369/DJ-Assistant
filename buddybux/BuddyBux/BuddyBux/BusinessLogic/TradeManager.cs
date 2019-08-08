using BuddyBux.DAO;
using BuddyBux.Models;
using BuddyBux.Models.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace BuddyBux.BusinessLogic
{
    public class TradeManager
    {
        private IBuddyBuxDAO _db;

        public TradeManager(IBuddyBuxDAO db)
        {
            _db = db;
        }

        /// <summary>
        /// Take a tradeModel converts it to a TradeItem that models the database
        /// </summary>
        /// <param name="tradeModel"></param>
        public int NewTrade(TradeModel tradeModel)
        {
            // needs error checking on input
            TradeItem item = new TradeItem();
            item.OriginatorId = _db.GetUserItemByEmail(tradeModel.Originator).Id;
            item.DesigneeId = _db.GetUserItemByEmail(tradeModel.Designee).Id;
            item.ProvidedCurrencyId = _db.GetCurrencyItemByOwnerId(item.OriginatorId).Id;
            item.ProvidedCurrencyQty = tradeModel.ProvidedCurrencyQty;
            item.RequestedCurrencyId = _db.GetCurrencyItemByOwnerId(item.DesigneeId).Id;
            item.RequestedCurrencyQty = tradeModel.RequestedCurrencyQty;
            item.TradeStatusId = (int) TradeItem.StatusId.Pending;
            return item.Id = _db.AddTradeItem(item);
        }

        public void AcceptTrade(int tradeId, string userName)
        {
            TradeItem tradeItem = _db.GetTradeItem(tradeId);
            UserItem user = _db.GetUserItemByEmail(userName);
            if (tradeItem.DesigneeId == user.Id)
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        UserCurrencyItem userCurrency1 = _db.GetUserCurrencyByUserIdCurrencyId(tradeItem.OriginatorId, tradeItem.RequestedCurrencyId);
                        UserCurrencyItem userCurrency2 = _db.GetUserCurrencyByUserIdCurrencyId(tradeItem.DesigneeId, tradeItem.ProvidedCurrencyId);
                        if(userCurrency1 == null)
                        {
                            userCurrency1.UserId = tradeItem.OriginatorId;
                            userCurrency1.CurrencyId = tradeItem.RequestedCurrencyId;
                            userCurrency1.Amount = tradeItem.RequestedCurrencyQty;
                            userCurrency1.Id = _db.AddUserCurrency(userCurrency1);
                        }
                        else
                        {
                            userCurrency1.Amount += tradeItem.RequestedCurrencyQty;
                            _db.UpdateUserCurrency(userCurrency1);
                        }

                        if (userCurrency2 == null)
                        {
                            userCurrency2.UserId = tradeItem.DesigneeId;
                            userCurrency2.CurrencyId = tradeItem.ProvidedCurrencyId;
                            userCurrency2.Amount = tradeItem.ProvidedCurrencyQty;
                            userCurrency2.Id = _db.AddUserCurrency(userCurrency2);
                        }
                        else
                        {
                            userCurrency2.Amount += tradeItem.ProvidedCurrencyQty;
                            _db.UpdateUserCurrency(userCurrency2);
                        }

                        tradeItem.TradeStatusId = (int) TradeItem.StatusId.Accepted;
                        _db.UpdateTradeItem(tradeItem);
                        scope.Complete();
                    }
                }
                catch(Exception e)
                {

                }
            }
            else
            {
                // need custom exception
                throw new Exception("Trade could not be accepted user not valid");
            }
        }

        public void RejectTrade(int tradeId, string userName)
        {
            TradeItem tradeItem = _db.GetTradeItem(tradeId);
            UserItem user = _db.GetUserItemByEmail(userName);
            if (tradeItem.DesigneeId == user.Id)
            {
                tradeItem.TradeStatusId = (int)TradeItem.StatusId.Rejected;
                _db.UpdateTradeItem(tradeItem);
            }
            else
            {
                // need custom exception?
                throw new Exception("Trade could not be rejected user not valid");
            }
        }

        public void GiveCurrency(string giver, string receiver, int qty)
        {
            TradeModel tradeModel = new TradeModel();
            tradeModel.Originator = giver;
            tradeModel.ProvidedCurrencyQty = qty;
            tradeModel.Designee = receiver;
            tradeModel.RequestedCurrencyQty = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                int tradeId = NewTrade(tradeModel);
                AcceptTrade(tradeId, receiver);
                scope.Complete();
            }
        }

        public void Transaction(string userName, int storeProductId, int qty)
        {
            StoreProductItem storeProduct = _db.GetStoreProductItemById(storeProductId);
            UserItem userItem = _db.GetUserItemByEmail(userName);
            List<UserCurrencyItem> userCurrencyItems =  _db.GetOwnedCurrency(userItem.Id);
            UserCurrencyItem currencyItem = null;
            foreach(var cI in userCurrencyItems)
            {
                if(storeProduct.CurrencyId == cI.CurrencyId)
                {
                    currencyItem = cI;
                }
            }
            if(currencyItem == null)
            {
                throw new Exception("No currency");
            }

            if(qty > storeProduct.Qty)
            {
                throw new Exception("Qty to high");
            }

            int totalCost = storeProduct.Cost * qty;
            if(currencyItem.Amount >= totalCost)
            {
                using(TransactionScope scope = new TransactionScope())
                {
                    TransactionItem trans = new TransactionItem();
                    trans.Cost = totalCost;
                    trans.CurrencyId = currencyItem.CurrencyId;
                    trans.PurchaseDate = DateTime.Now;
                    trans.PurchaserId = userItem.Id;
                    trans.Qty = qty;
                    trans.StoreProductId = storeProduct.Id;
                    _db.AddTransactionItem(trans);
                    currencyItem.Amount -= totalCost;
                    _db.UpdateUserCurrency(currencyItem);
                    storeProduct.Qty -= qty;
                    _db.UpdateStoreProductItem(storeProduct);
                    scope.Complete();
                }
            }
            else
            {
                throw new Exception("Currentcy to low");
            }
        }
    }
}
