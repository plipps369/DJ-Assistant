using BuddyBux.Models;
using BuddyBux.Models.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Transactions;

namespace BuddyBux.DAO
{
    public class BuddyBuxDAO : IBuddyBuxDAO
    {
        #region Variables

        private const string _getLastIdSQL = "SELECT CAST(SCOPE_IDENTITY() as int);";
        private string _connectionString;

        #endregion

        public BuddyBuxDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region User

        public int AddUserItem(UserItem item)
        {
            const string sql = "INSERT [User] (" +
                                    "FirstName, " +
                                    "LastName, " +
                                    "Email, " +
                                    "Hash, " +
                                    "Salt, " +
                                    "RoleId, " +
                                    "ImageFileId, " +
                                    "CreationDate, " +
                                    "Rating) " +
                               "VALUES (" +
                                    "@FirstName, " +
                                    "@LastName, " +
                                    "@Email, " +
                                    "@Hash, " +
                                    "@Salt, " +
                                    "@RoleId, " +
                                    "@ImageFileId, " +
                                    "@CreationDate, " +
                                    "@Rating);";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@FirstName", item.FirstName);
                cmd.Parameters.AddWithValue("@LastName", item.LastName);
                cmd.Parameters.AddWithValue("@Email", item.Email);
                cmd.Parameters.AddWithValue("@Hash", item.Hash);
                cmd.Parameters.AddWithValue("@Salt", item.Salt);
                cmd.Parameters.AddWithValue("@RoleId", item.RoleId);
                if (item.ImageFileId != BaseItem.InvalidId)
                {
                    cmd.Parameters.AddWithValue("@ImageFileId", item.ImageFileId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ImageFileId", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@CreationDate", item.CreationDate);
                cmd.Parameters.AddWithValue("@Rating", item.Rating);
                item.Id = (int)cmd.ExecuteScalar();
            }

            return item.Id;
        }

        public bool UpdateUserItem(UserItem item)
        {
            bool isSuccessful = false;

            const string sql = "UPDATE [User] SET FirstName = @FirstName, " +
                                                 "LastName = @LastName, " +
                                                 "Email = @Email, " +
                                                 "Hash = @Hash, " +
                                                 "Salt = @Salt, " +
                                                 "RoleId = @RoleId, " +
                                                 "ImageFileId = @ImageFileId, " +
                                                 "Rating = @Rating " +
                                                 "WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@FirstName", item.FirstName);
                cmd.Parameters.AddWithValue("@LastName", item.LastName);
                cmd.Parameters.AddWithValue("@Email", item.Email);
                cmd.Parameters.AddWithValue("@Hash", item.Hash);
                cmd.Parameters.AddWithValue("@Salt", item.Salt);
                cmd.Parameters.AddWithValue("@RoleId", item.RoleId);
                if(item.ImageFileId != BaseItem.InvalidId)
                {
                    cmd.Parameters.AddWithValue("@ImageFileId", item.ImageFileId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ImageFileId", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@Rating", item.Rating);
                cmd.Parameters.AddWithValue("@Id", item.Id);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    isSuccessful = true;
                }
            }

            return isSuccessful;
        }

        public bool DeleteUserItem(int userId)
        {
            bool isSuccessful = false;

            const string sql = "DELETE FROM [User] WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", userId);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    isSuccessful = true;
                }
            }

            return isSuccessful;
        }

        public UserItem GetUserItemById(int userId)
        {
            UserItem user = null;
            const string sql = "SELECT * From [User] WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", userId);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    user = GetUserItemFromReader(reader);
                }
            }

            if (user == null)
            {
                throw new Exception("User does not exist.");
            }

            return user;
        }

        public List<UserItem> GetUserItems()
        {
            List<UserItem> users = new List<UserItem>();
            const string sql = "Select * From [User];";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(GetUserItemFromReader(reader));
                }
            }

            return users;
        }

        public UserItem GetUserItemByEmail(string email)
        {
            UserItem user = null;
            const string sql = "SELECT * From [User] WHERE Email = @Email;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    user = GetUserItemFromReader(reader);
                }
            }

            if (user == null)
            {
                throw new Exception("User does not exist.");
            }

            return user;
        }

        private UserItem GetUserItemFromReader(SqlDataReader reader)
        {
            UserItem item = new UserItem();

            item.Id = Convert.ToInt32(reader["Id"]);
            item.FirstName = Convert.ToString(reader["FirstName"]);
            item.LastName = Convert.ToString(reader["LastName"]);
            item.Email = Convert.ToString(reader["Email"]);
            item.Salt = Convert.ToString(reader["Salt"]);
            item.Hash = Convert.ToString(reader["Hash"]);
            item.RoleId = Convert.ToInt32(reader["RoleId"]);
            item.CreationDate = Convert.ToDateTime(reader["CreationDate"]);
            item.Rating = Convert.ToInt32(reader["Rating"]);
            if (!reader.IsDBNull(reader.GetOrdinal("ImageFileId")))
            {
                item.ImageFileId = Convert.ToInt32(reader["ImageFileId"]);
            }           

            return item;
        }

        #endregion

        #region Role

        public int AddRoleItem(RoleItem item)
        {
            const string sql = "INSERT Role (Name) " +
                               "VALUES (@Name);";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@Name", item.Name);
                item.Id = (int) cmd.ExecuteScalar();
            }

            return item.Id;
        }

        public List<RoleItem> GetRoleItems()
        {
            List<RoleItem> roles = new List<RoleItem>();
            const string sql = "Select * From Role;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    roles.Add(GetRoleItemFromReader(reader));
                }
            }

            return roles;
        }

        private RoleItem GetRoleItemFromReader(SqlDataReader reader)
        {
            RoleItem item = new RoleItem();

            item.Id = Convert.ToInt32(reader["Id"]);
            item.Name = Convert.ToString(reader["Name"]);

            return item;
        }

        #endregion

        #region File

        public int AddFileItem(FileItem item)
        {
            const string sql = "INSERT [File] (OriginalName, StorageName, StoragePath, Size, Hash) " +
                               "VALUES (@OriginalName, @StorageName, @StoragePath, @Size, @Hash);";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@OriginalName", item.OriginalName);
                cmd.Parameters.AddWithValue("@StorageName", item.StorageName);
                cmd.Parameters.AddWithValue("@StoragePath", item.StoragePath);
                cmd.Parameters.AddWithValue("@Size", item.Size);
                cmd.Parameters.AddWithValue("@Hash", item.Hash);
                item.Id = (int)cmd.ExecuteScalar();
            }

            return item.Id;
        }

        public bool UpdateFileItem(FileItem item)
        {
            bool isSuccessful = false;

            const string sql = "UPDATE [File] SET OriginalName = @OriginalName, " +
                                                 "StorageName = @StorageName, " +
                                                 "StoragePath = @StoragePath, " +
                                                 "Size = @Size, " +
                                                 "Hash = @Hash " +
                                                 "WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@OriginalName", item.OriginalName);
                cmd.Parameters.AddWithValue("@StorageName", item.StorageName);
                cmd.Parameters.AddWithValue("@StoragePath", item.StoragePath);
                cmd.Parameters.AddWithValue("@Size", item.Size);
                cmd.Parameters.AddWithValue("@Hash", item.Hash);
                cmd.Parameters.AddWithValue("@Id", item.Id);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    isSuccessful = true;
                }
            }

            return isSuccessful;
        }

        public bool DeleteFileItem(int id)
        {
            bool isSuccessful = false;

            const string sql = "DELETE FROM [File] WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    isSuccessful = true;
                }
            }

            return isSuccessful;
        }

        public FileItem GetFileItemById(int id)
        {
            FileItem item = null;
            const string sql = "SELECT * From [File] WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    item = GetFileItemFromReader(reader);
                }
            }

            if (item == null)
            {
                throw new Exception("File item does not exist.");
            }

            return item;
        }

        public List<FileItem> GetFileItems()
        {
            List<FileItem> items = new List<FileItem>();
            const string sql = "Select * From [File];";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    items.Add(GetFileItemFromReader(reader));
                }
            }

            return items;
        }

        private FileItem GetFileItemFromReader(SqlDataReader reader)
        {
            FileItem item = new FileItem();

            item.Id = Convert.ToInt32(reader["Id"]);
            item.OriginalName = Convert.ToString(reader["OriginalName"]);
            item.StorageName = Convert.ToString(reader["StorageName"]);
            item.StoragePath = Convert.ToString(reader["StoragePath"]);
            item.Size = Convert.ToInt32(reader["Size"]);
            item.Hash = Convert.ToString(reader["Hash"]);

            return item;
        }

        #endregion

        #region TradeStatus

        public int AddTradeStatusItem(TradeStatusItem item)
        {
            const string sql = "INSERT TradeStatus (Status) " +
                               "VALUES (@Status);";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@Status", item.Status);
                item.Id = (int)cmd.ExecuteScalar();
            }

            return item.Id;
        }

        public List<TradeStatusItem> GetTradeStatusItems()
        {
            List<TradeStatusItem> roles = new List<TradeStatusItem>();
            const string sql = "Select * From TradeStatus;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    roles.Add(GetTradeStatusItemFromReader(reader));
                }
            }

            return roles;
        }

        private TradeStatusItem GetTradeStatusItemFromReader(SqlDataReader reader)
        {
            TradeStatusItem item = new TradeStatusItem();

            item.Id = Convert.ToInt32(reader["Id"]);
            item.Status = Convert.ToString(reader["Status"]);

            return item;
        }

        #endregion

        #region Trade

        public int AddTradeItem(TradeItem item)
        {
            const string sql = "INSERT [Trade] (" +
                                   "OriginatorId, " +
                                   "DesigneeId, " +
                                   "ProvidedCurrencyId, " +
                                   "RequestedCurrencyId, " +
                                   "ProvidedCurrencyQty, " +
                                   "RequestedCurrencyQty, " +
                                   "TradeStatusId) " +
                              "VALUES (" +
                                   "@OriginatorId, " +
                                   "@DesigneeId, " +
                                   "@ProvidedCurrencyId, " +
                                   "@RequestedCurrencyId, " +
                                   "@ProvidedCurrencyQty, " +
                                   "@RequestedCurrencyQty, " +
                                   "@TradeStatusId);";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@OriginatorId", item.OriginatorId);
                cmd.Parameters.AddWithValue("@DesigneeId", item.DesigneeId);
                cmd.Parameters.AddWithValue("@ProvidedCurrencyId", item.ProvidedCurrencyId);
                cmd.Parameters.AddWithValue("@RequestedCurrencyId", item.RequestedCurrencyId);
                cmd.Parameters.AddWithValue("@ProvidedCurrencyQty", item.ProvidedCurrencyQty);
                cmd.Parameters.AddWithValue("@RequestedCurrencyQty", item.RequestedCurrencyQty);
                cmd.Parameters.AddWithValue("@TradeStatusId", item.TradeStatusId);
                item.Id = (int)cmd.ExecuteScalar();
            }

            return item.Id;
        }

        public bool UpdateTradeItem(TradeItem item)
        {
            bool isSuccessful = false;

            const string sql = "UPDATE [Trade] SET OriginatorId = @OriginatorId, " +
                                                 "DesigneeId = @DesigneeId, " +
                                                 "ProvidedCurrencyId = @ProvidedCurrencyId, " +
                                                 "RequestedCurrencyId = @RequestedCurrencyId, " +
                                                 "ProvidedCurrencyQty = @ProvidedCurrencyQty, " +
                                                 "RequestedCurrencyQty = @RequestedCurrencyQty, " +
                                                 "TradeStatusId = @TradeStatusId " +
                                                 "WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@OriginatorId", item.OriginatorId);
                cmd.Parameters.AddWithValue("@DesigneeId", item.DesigneeId);
                cmd.Parameters.AddWithValue("@ProvidedCurrencyId", item.ProvidedCurrencyId);
                cmd.Parameters.AddWithValue("@RequestedCurrencyId", item.RequestedCurrencyId);
                cmd.Parameters.AddWithValue("@ProvidedCurrencyQty", item.ProvidedCurrencyQty);
                cmd.Parameters.AddWithValue("@RequestedCurrencyQty", item.RequestedCurrencyQty);
                cmd.Parameters.AddWithValue("@TradeStatusId", item.TradeStatusId);
                cmd.Parameters.AddWithValue("@Id", item.Id);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    isSuccessful = true;
                }
            }

            return isSuccessful;
        }

        public bool DeleteTradeItem(int tradeId)
        {
            bool isSuccessful = false;

            const string sql = "DELETE FROM [Trade] WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", tradeId);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    isSuccessful = true;
                }
            }

            return isSuccessful;
        }

        public TradeItem GetTradeItem(int tradeId)
        {
            TradeItem trade = null;
            const string sql = "SELECT * From [Trade] WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", tradeId);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    trade = GetTradeItemFromReader(reader);
                }
            }

            if (trade == null)
            {
                throw new Exception("User does not exist.");
            }

            return trade;
        }

        public List<TradeItem> GetTradeItemByOriginatorId(int originatorId)
        {
            List<TradeItem> trades = new List<TradeItem>();
            const string sql = "Select * From [Trade] Where OriginatorId = @OriginatorId;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@OriginatorId", originatorId);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    trades.Add(GetTradeItemFromReader(reader));
                }
            }

            return trades;
        }

        public List<TradeItem> GetTradeItemByDesigneeId(int designeeId)
        {
            List<TradeItem> trades = new List<TradeItem>();
            const string sql = "Select * From [Trade] Where DesigneeId = @DesigneeId;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@DesigneeId", designeeId);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    trades.Add(GetTradeItemFromReader(reader));
                }
            }

            return trades;
        }

        private TradeItem GetTradeItemFromReader(SqlDataReader reader)
        {
            TradeItem item = new TradeItem();

            item.Id = Convert.ToInt32(reader["Id"]);
            item.OriginatorId = Convert.ToInt32(reader["OriginatorId"]);
            item.DesigneeId = Convert.ToInt32(reader["DesigneeId"]);
            item.ProvidedCurrencyId = Convert.ToInt32(reader["ProvidedCurrencyId"]);
            item.RequestedCurrencyId = Convert.ToInt32(reader["RequestedCurrencyId"]);
            item.ProvidedCurrencyQty = Convert.ToInt32(reader["ProvidedCurrencyQty"]);
            item.RequestedCurrencyQty = Convert.ToInt32(reader["RequestedCurrencyQty"]);
            item.TradeStatusId = Convert.ToInt32(reader["TradeStatusId"]);

            return item;
        }

        #endregion

        #region UserAccount

        public UserAccountModel AddUserAccount(UserAccountModel model)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                model.User.Id = AddUserItem(model.User);
                model.Currency.OwnerId = model.User.Id;
                model.Currency.Id = AddCurrencyItem(model.Currency);
                model.Store.UserId = model.User.Id;
                model.Store.Id = AddStore(model.Store);
                scope.Complete();
            }

            return model;
        }

        public bool UpdateUserAccount(UserAccountModel model)
        {
            bool isSuccessful = false;

            using (TransactionScope scope = new TransactionScope())
            {
                if (UpdateUserItem(model.User) &&
                   UpdateCurrencyItem(model.Currency))
                {
                    isSuccessful = true;
                }
                scope.Complete();
            }

            return isSuccessful;
            
        }

        public UserAccountModel GetUserAccount(int userId)
        {
            UserAccountModel model = null;
            const string sql = "SELECT [User].id as UserId, [Currency].id  as CurrencyId, " +
                                      "[User].ImageFileId as UserImageFileId, " + 
                                      "[Currency].ImageFileId as CurrencyImageFileId, " +
                                      "[User].FirstName as FirstName, " +
                                      "[User].LastName as LastName, " +
                                      "[User].Email as Email, " +
                                      "[User].Salt as Salt, " +
                                      "[User].Hash as Hash, " +
                                      "[User].Rating as Rating, " +
                                      "[User].RoleId as RoleId, " +
                                      "[User].CreationDate as CreationDate, " +
                                      "[User].ImageFileId as UserImageFileId, " +
                                      "[Currency].Id as CurrencyId, " +
                                      "[Currency].Name as Name, " +
                                      "[Currency].OwnerId as OwnerId, " +
                                      "[Currency].ImageFileId as CurrencyImageFileId " +
                               "From [User] Join [Currency] On [User].Id = [Currency].OwnerId WHERE [User].Id = @Id;";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", userId);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    model = GetUserAcountModelFromReader(reader);
                }
            }

            if (model == null)
            {
                throw new Exception("UserId does not connect to a Currency.");
            }

            return model;
           
        }

        private UserAccountModel GetUserAcountModelFromReader(SqlDataReader reader)
        {

            UserAccountModel model = new UserAccountModel();

            model.User = new UserItem();
            model.Currency = new CurrencyItem();
            model.User.Id = Convert.ToInt32(reader["UserId"]);
            model.User.FirstName = Convert.ToString(reader["FirstName"]);
            model.User.LastName = Convert.ToString(reader["LastName"]);
            model.User.Email = Convert.ToString(reader["Email"]);
            model.User.Salt = Convert.ToString(reader["Salt"]);
            model.User.Hash = Convert.ToString(reader["Hash"]);
            model.User.RoleId = Convert.ToInt32(reader["RoleId"]);
            model.User.CreationDate = Convert.ToDateTime(reader["CreationDate"]);
            model.User.Rating = Convert.ToInt32(reader["Rating"]);
            if (!reader.IsDBNull(reader.GetOrdinal("UserImageFileId")))
            {
                model.User.ImageFileId = Convert.ToInt32(reader["UserImageFileId"]);
            }

            model.Currency.Id = Convert.ToInt32(reader["CurrencyId"]);
            model.Currency.Name = Convert.ToString(reader["Name"]);
            model.Currency.OwnerId = Convert.ToInt32(reader["OwnerId"]);
            if (!reader.IsDBNull(reader.GetOrdinal("CurrencyImageFileId")))
            {
                model.Currency.ImageFileId = Convert.ToInt32(reader["CurrencyImageFileId"]);
            }

            return model;

        }

        #endregion

        #region Currency

        public int AddCurrencyItem(CurrencyItem item)
        {
            const string sql = "INSERT [Currency] (" +
                                    "Name, " +
                                    "ImageFileId, " +
                                    "OwnerId) " +
                               "VALUES (" +
                                    "@Name, " +
                                    "@ImageFileId, " +
                                    "@OwnerId);";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                
                cmd.Parameters.AddWithValue("@Name", item.Name);
                if (item.ImageFileId != BaseItem.InvalidId)
                {
                    cmd.Parameters.AddWithValue("@ImageFileId", item.ImageFileId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ImageFileId", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@OwnerId", item.OwnerId);
                item.Id = (int)cmd.ExecuteScalar();
                
            }

            return item.Id;
        }

        public bool UpdateCurrencyItem(CurrencyItem item)
        {
            bool isSuccessful = false;
            
            const string sql = "UPDATE [Currency] SET ImageFileId = @ImageFileId, " +
                                                 "Name = @Name, " +
                                                 "OwnerId = @OwnerId " +
                                                 "WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);

                if (item.ImageFileId != BaseItem.InvalidId)
                {
                    cmd.Parameters.AddWithValue("@ImageFileId", item.ImageFileId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ImageFileId", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@OwnerId", item.OwnerId);
                cmd.Parameters.AddWithValue("@Id", item.Id);

                if (cmd.ExecuteNonQuery() == 1)
                {
                    isSuccessful = true;
                }

            }
            return isSuccessful;
        }

        public bool DeleteCurrencyItem(int currencyId)
        {
            bool isSuccessful = false;

            const string sql = "DELETE FROM [Currency] WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", currencyId);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    isSuccessful = true;
                }
            }

            return isSuccessful;
        }

        public CurrencyItem GetCurrencyItemByOwnerId(int ownerId)
        {
            CurrencyItem item = null;
            const string sql = "SELECT * From [Currency] WHERE OwnerId = @OwnerId;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@OwnerId", ownerId);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    item = GetCurrencyItemFromReader(reader);
                }
            }

            if (item == null)
            {
                throw new Exception("Item does not exist.");
            }

            return item;
        }

        public CurrencyItem GetCurrencyItemById(int id)
        {

            CurrencyItem item = null;
            const string sql = "SELECT * From [Currency] WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    item = GetCurrencyItemFromReader(reader);
                }
            }

            if (item == null)
            {
                throw new Exception("Item does not exist.");
            }

            return item;
        }

        private CurrencyItem GetCurrencyItemFromReader(SqlDataReader reader)
        {
            CurrencyItem item = new CurrencyItem();

            item.Id = Convert.ToInt32(reader["Id"]);
            item.Name = Convert.ToString(reader["Name"]);
            item.OwnerId = Convert.ToInt32(reader["OwnerId"]);
            if (!reader.IsDBNull(reader.GetOrdinal("ImageFileId")))
            {
                item.ImageFileId = Convert.ToInt32(reader["ImageFileId"]);
            }

            return item;
        }

        public List<UserCurrencyItem> GetOwnedCurrency(int userId)
        {
            List<UserCurrencyItem> userCurrency = new List<UserCurrencyItem>();
            const string sql = "Select * From [UserCurrency] Where UserId = @UserId;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    userCurrency.Add(GetUserCurrencyFromReader(reader));
                }
            }

            return userCurrency;

        }

        private UserCurrencyItem GetUserCurrencyFromReader(SqlDataReader reader)
        {
            UserCurrencyItem item = new UserCurrencyItem();
            item.Id = Convert.ToInt32(reader["Id"]);
            item.UserId = Convert.ToInt32(reader["UserId"]);
            item.CurrencyId = Convert.ToInt32(reader["CurrencyId"]);
            item.Amount = Convert.ToInt32(reader["Amount"]);
            return item;
        }

        #endregion

        #region Product

        public int AddProduct(ProductItem item)
        {
            const string sql = "INSERT [Product] (" +
                                    "Name, " +
                                    "ImageFileId, " +
                                    "IsCharitable, " +
                                    "CreatorId) " +
                               "VALUES (" +
                                    "@Name, " +
                                    "@ImageFileId, " +
                                    "@IsCharitable, " +
                                    "@CreatorId);";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@IsCharitable", item.IsCharitable);
                cmd.Parameters.AddWithValue("@CreatorId", item.CreatorId);
                if (item.ImageFileId != BaseItem.InvalidId)
                {
                    cmd.Parameters.AddWithValue("@ImageFileId", item.ImageFileId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ImageFileId", DBNull.Value);
                }
                item.Id = (int)cmd.ExecuteScalar();
            }

            return item.Id;
        }

        public bool UpdateProductItem(ProductItem item)
        {
            bool isSuccessful = false;

            const string sql = "UPDATE [Product] SET Name = @Name, " +
                                                 "ImageFileId = @ImageFileId, " +
                                                 "IsCharitable = @IsCharitable, " +
                                                 "CreatorId = @CreatorId " +
                                                 "WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@IsCharitable", item.IsCharitable);
                if (item.ImageFileId != BaseItem.InvalidId)
                {
                    cmd.Parameters.AddWithValue("@ImageFileId", item.ImageFileId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ImageFileId", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@CreatorId", item.CreatorId);
                cmd.Parameters.AddWithValue("@Id", item.Id);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    isSuccessful = true;
                }
            }

            return isSuccessful;
        }

        public bool DeleteProductItem(int id)
        {
            bool isSuccessful = false;

            const string sql = "DELETE FROM [Product] WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    isSuccessful = true;
                }
            }

            return isSuccessful;
        }

        public ProductItem GetProductById(int id)
        {
            ProductItem Product = null;
            const string sql = "SELECT * From [Product] WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product = GetProdcuctItemFromReader(reader);
                }
            }

            if (Product == null)
            {
                throw new Exception("Product does not exist.");
            }

            return Product;
        }

        private ProductItem GetProdcuctItemFromReader(SqlDataReader reader)
        {
            ProductItem item = new ProductItem();

            item.Id = Convert.ToInt32(reader["Id"]);
            item.Name = Convert.ToString(reader["Name"]);
            item.IsCharitable = Convert.ToBoolean(reader["IsCharitable"]);
            if (!reader.IsDBNull(reader.GetOrdinal("ImageFileId")))
            {
                item.ImageFileId = Convert.ToInt32(reader["ImageFileId"]);
            }
            item.CreatorId = Convert.ToInt32(reader["CreatorId"]);
            return item;
        }

        public List<ProductItem> GetProducts()
        {
            List<ProductItem> productList = new List<ProductItem>();
            const string sql = "Select * From [Product]";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    productList.Add(GetProdcuctItemFromReader(reader));
                }
            }

            return productList;
        }


        #endregion

        #region Store

        public int AddStore(StoreItem item)
        {
            
            const string sql = "INSERT [Store] (UserId) " +
                               "VALUES (@UserId);";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@UserId", item.UserId);
                item.Id = (int)cmd.ExecuteScalar();
            }

            return item.Id;
        }

        public int AddStoreProductItem(StoreProductItem item)
        {
            const string sql = "INSERT [StoreProduct] (" +
                                    "ProductId, " +
                                    "StoreId, " +
                                    "Qty, " +
                                    "Cost, " +
                                    "CurrencyId) " +
                               "VALUES (" +
                                    "@ProductId, " +
                                    "@StoreId, " +
                                    "@Qty, " +
                                    "@Cost, " +
                                    "@CurrencyId);";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@ProductId", item.ProductId);
                cmd.Parameters.AddWithValue("@StoreId", item.StoreId);
                cmd.Parameters.AddWithValue("@Qty", item.Qty);
                cmd.Parameters.AddWithValue("@Cost", item.Cost);
                cmd.Parameters.AddWithValue("@CurrencyId", item.CurrencyId);
                item.Id = (int)cmd.ExecuteScalar();
            }

            return item.Id;
            
        }

        public bool UpdateStoreProductItem(StoreProductItem item)
        {
            bool isSuccessful = false;

            const string sql = "UPDATE [StoreProduct] SET ProductId = @ProductId, " +
                                                 "StoreId = @StoreId, " +
                                                 "Qty = @Qty, " +
                                                 "Cost = @Cost, " +
                                                 "CurrencyId = @CurrencyId " +
                                                 "WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ProductId", item.ProductId);
                cmd.Parameters.AddWithValue("@StoreId", item.StoreId);
                cmd.Parameters.AddWithValue("@Qty", item.Qty);
                cmd.Parameters.AddWithValue("@Cost", item.Cost);
                cmd.Parameters.AddWithValue("@CurrencyId", item.CurrencyId);
                cmd.Parameters.AddWithValue("@Id", item.Id);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    isSuccessful = true;
                }
            }

            return isSuccessful;
        }

        public StoreItem GetStoreItemById(int userId)
        {
            StoreItem item = null;
            const string sql = "SELECT * From [Store] WHERE UserId = @UserId;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    item = GetStoreItemFromReader(reader);
                }
            }

            if (item == null)
            {
                throw new Exception("Product does not exist.");
            }

            return item;
        }

        private StoreItem GetStoreItemFromReader(SqlDataReader reader)
        {
            StoreItem item = new StoreItem();

            item.Id = Convert.ToInt32(reader["Id"]);
            item.UserId = Convert.ToInt32(reader["UserId"]);
            return item;
        }

        public bool RemoveProductFromStore(int storeProductItemId)
        {
            bool isSuccessful = false;

            const string sql = "DELETE FROM [StoreProduct] WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", storeProductItemId);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    isSuccessful = true;
                }
            }

            return isSuccessful;
        }

        public StoreFrontModel GetStoreFront(int userId)
        {
            StoreFrontModel storeFront = new StoreFrontModel();
            storeFront.Store = GetStoreItemById(userId);

            List<StoreProductItem> storeProducts = new List<StoreProductItem>();
            storeFront.StoreProducts = storeProducts;


            const string sql = "SELECT [Product].Id as ProductId, " +
                                      "[StoreProduct].Id as StoreProductId, " +
                                      "[StoreProduct].Cost as Cost, " +
                                      "[StoreProduct].CurrencyId as CurrencyId, " +
                                      "[StoreProduct].ProductId as ProductId, " +
                                      "[StoreProduct].Qty as Qty, " +
                                      "[StoreProduct].StoreId as StoreId, " +
                                      "[Product].CreatorId as CreatorId, " +
                                      "[Product].ImageFileId as ImageFileId, " +
                                      "[Product].IsCharitable as IsCharitable," +
                                      "[Product].Name as Name " +
                                       "FROM[StoreProduct] join [Product] on [Product].Id = [StoreProduct].ProductId " +
                                       "WHERE [StoreProduct].StoreId = @StoreId";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@StoreId", storeFront.Store.Id);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    
                    storeProducts.Add(GetStoreProductsFromReader(reader));
                    ProductItem product = GetProductFromReader(reader);
                    storeFront.Products[product.Id] = product;
                    
                }
            }

            return storeFront;
        }

        //I'm not using this anymore but was not sure if we might need it. used a join on the database ratherthan
        //two calls to the data base.
        public List<StoreProductItem> GetStoreProductsItemsById(int storeId)
        {
            List<StoreProductItem> storeProducts = new List<StoreProductItem>();

            const string sql = "Select * From [StoreProduct] Where StoreId = @storeId;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@StoreId", storeId);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    storeProducts.Add(GetStoreProductsFromReader(reader));
                }
            }

            return storeProducts;
        }

        private StoreProductItem GetStoreProductsFromReader(SqlDataReader reader)
        {
            StoreProductItem item = new StoreProductItem();
            item.Id = Convert.ToInt32(reader["StoreProductId"]);
            item.Cost = Convert.ToInt32(reader["Cost"]);
            item.CurrencyId = Convert.ToInt32(reader["CurrencyId"]);
            item.ProductId = Convert.ToInt32(reader["ProductId"]);
            item.Qty = Convert.ToInt32(reader["Qty"]);
            item.StoreId = Convert.ToInt32(reader["StoreId"]);
            return item;
        }


        private ProductItem GetProductFromReader(SqlDataReader reader)
        {
            ProductItem item = new ProductItem();

            item.Id = Convert.ToInt32(reader["ProductId"]);
            item.CreatorId = Convert.ToInt32(reader["CreatorId"]);
            if (!reader.IsDBNull(reader.GetOrdinal("ImageFileId")))
            {
                item.ImageFileId = Convert.ToInt32(reader["ImageFileId"]);
            }
            item.IsCharitable = Convert.ToBoolean(reader["IsCharitable"]);
            item.Name = Convert.ToString(reader["Name"]);

            return item;
            
        }

        StoreProductItem GetStoreProductItemById(int storeProductItem)
        {
            throw (new Exception());
        }

        #endregion

        #region UserCurrency

        public int AddUserCurrency(UserCurrencyItem item)
        {
            const string sql = "INSERT [UserCurrency] (CurrencyId, UserId, Amount) " +
                               "VALUES (@CurrencyId, @UserId, @Amount);";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@CurrencyId", item.CurrencyId);
                cmd.Parameters.AddWithValue("@UserId", item.UserId);
                cmd.Parameters.AddWithValue("@Amount", item.Amount);
                item.Id = (int)cmd.ExecuteScalar();
            }

            return item.Id;
        }

        public bool UpdateUserCurrency(UserCurrencyItem item)
        {
            bool isSuccessful = false;

            const string sql = "UPDATE [UserCurrency] SET CurrencyId = @CurrencyId, " +
                                                 "UserId = @UserId, " +
                                                 "Amount = @Amount " +
                                                 "WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@CurrencyId", item.CurrencyId);
                cmd.Parameters.AddWithValue("@UserId", item.UserId);
                cmd.Parameters.AddWithValue("@Amount", item.Amount);
                cmd.Parameters.AddWithValue("@Id", item.Id);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    isSuccessful = true;
                }
            }

            return isSuccessful;
        }

        public bool DeleteUserCurrency(int id)
        {
            bool isSuccessful = false;

            const string sql = "DELETE FROM [UserCurrency] WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    isSuccessful = true;
                }
            }

            return isSuccessful;
        }

        public UserCurrencyItem GetUserCurrencyById(int id)
        {
            UserCurrencyItem item = null;
            const string sql = "SELECT * From [UserCurrency] WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    item = GetUserCurrencyFromReader(reader);
                }
            }

            if (item == null)
            {
                throw new Exception("Item does not exist.");
            }

            return item;
        }

        public UserCurrencyItem GetUserCurrencyByUserIdCurrencyId(int userId, int currencyId)
        {
            UserCurrencyItem userCurrency = null;

            const string sql = "SELECT * From [UserCurrency] WHERE UserId = @UserId " +
                                                            "and CurrencyId = @CurrencyId;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@CurrencyId", currencyId);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    userCurrency = GetUserCurrencyFromReader(reader);
                }
            }

            return userCurrency;
        }

        #endregion

        #region Transaction

        public int AddTransactionItem(TransactionItem item)
        {
            const string sql = "INSERT [Transaction] (" +
                                   "StoreProductId, " +
                                   "PurchaseDate, " +
                                   "Qty, " +
                                   "PurchaserId, " +
                                   "Cost, " +
                                   "CurrencyId) " +
                              "VALUES (" +
                                   "@StoreProductId, " +
                                   "@PurchaseDate, " +
                                   "@Qty, " +
                                   "@PurchaserId, " +
                                   "@Cost, " +
                                   "@CurrencyId);";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@StoreProductId", item.StoreProductId);
                cmd.Parameters.AddWithValue("@PurchaseDate", item.PurchaseDate);
                cmd.Parameters.AddWithValue("@Qty", item.Qty);
                cmd.Parameters.AddWithValue("@PurchaserId", item.PurchaserId);
                cmd.Parameters.AddWithValue("@Cost", item.Cost);
                cmd.Parameters.AddWithValue("@CurrencyId", item.CurrencyId);
                item.Id = (int)cmd.ExecuteScalar();
            }

            return item.Id;
        }

        public bool UpdateTransactionItem(TransactionItem item)
        {
            bool isSuccessful = false;

            const string sql = "UPDATE [Transaction] SET StoreProductId = @StoreProductId, " +
                                                 "PurchaseDate = @PurchaseDate, " +
                                                 "Qty = @Qty, " +
                                                 "PurchaserId = @PurchaserId, " +
                                                 "Cost = @Cost, " +
                                                 "CurrencyId = @CurrencyId " +
                                                 "WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@StoreProductId", item.StoreProductId);
                cmd.Parameters.AddWithValue("@PurchaseDate", item.PurchaseDate);
                cmd.Parameters.AddWithValue("@Qty", item.Qty);
                cmd.Parameters.AddWithValue("@PurchaserId", item.PurchaserId);
                cmd.Parameters.AddWithValue("@Cost", item.Cost);
                cmd.Parameters.AddWithValue("@CurrencyId", item.CurrencyId);
                cmd.Parameters.AddWithValue("@Id", item.Id);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    isSuccessful = true;
                }
            }

            return isSuccessful;
        }

        public bool DeleteTransactionItem(int transactionId)
        {
            bool isSuccessful = false;

            const string sql = "DELETE FROM [Transaction] WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", transactionId);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    isSuccessful = true;
                }
            }

            return isSuccessful;
        }

        public TransactionItem GetTransactionItemById(int transactionId)
        {
            TransactionItem transaction = null;
            const string sql = "SELECT * From [Transaction] WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", transactionId);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    transaction = GetTransactionItemFromReader(reader);
                }
            }

            if (transaction == null)
            {
                throw new Exception("Transaction does not exist.");
            }

            return transaction;
        }

        public List<TransactionItem> GetTransactionItemByStoreProductId(int storeProductId)
        {
            List<TransactionItem> users = new List<TransactionItem>();
            const string sql = "Select * From [Transaction] Where StoreProductId = @StoreProductId;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@StoreProductId", storeProductId);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(GetTransactionItemFromReader(reader));
                }
            }

            return users;
        }

        public List<TransactionItem> GetTransactionItemByPurchaserId(int purchaserId)
        {
            List<TransactionItem> transactions = new List<TransactionItem>();
            const string sql = "Select * From [Transaction] Where PurchaserId = @PurchaserId;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@PurchaserId", purchaserId);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    transactions.Add(GetTransactionItemFromReader(reader));
                }
            }

            return transactions;
        }

        public List<TransactionItem> GetTransactionItemByStoreId(int storeId)
        {
            List<TransactionItem> transactions = new List<TransactionItem>();
            const string sql = "Select [Transaction].Id as Id, " +
                                      "[Transaction].StoreProductId as StoreProductId, " +
                                      "[Transaction].PurchaseDate as PurchaseDate, " +
                                      "[Transaction].Qty as Qty, " +
                                      "[Transaction].PurchaserId as PurchaserId, " +
                                      "[Transaction].Cost as Cost, " +
                                      "[Transaction].CurrencyId as CurrencyId " +
                                      "From [Transaction] " +
                                      "Join [StoreProduct] on [Transaction].StoreProductId = [StoreProduct].Id " +
                                      "Where [StoreProduct].StoreId = @StoreId;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@StoreId", storeId);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    transactions.Add(GetTransactionItemFromReader(reader));
                }
            }

            return transactions;
        }

        private TransactionItem GetTransactionItemFromReader(SqlDataReader reader)
        {
            TransactionItem item = new TransactionItem();

            item.Id = Convert.ToInt32(reader["Id"]);
            item.StoreProductId = Convert.ToInt32(reader["StoreProductId"]);
            item.PurchaseDate = Convert.ToDateTime(reader["PurchaseDate"]);
            item.Qty = Convert.ToInt32(reader["Qty"]);
            item.PurchaserId = Convert.ToInt32(reader["PurchaserId"]);
            item.Cost = Convert.ToInt32(reader["Cost"]);
            item.CurrencyId = Convert.ToInt32(reader["CurrencyId"]);
            return item;
        }

        StoreProductItem IBuddyBuxDAO.GetStoreProductItemById(int storeProductItem)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
