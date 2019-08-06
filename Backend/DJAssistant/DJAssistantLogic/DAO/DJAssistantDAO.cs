using DJAssistantLogic.Models.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Transactions;
using System.Data.SqlClient;

namespace DJAssistantLogic.DAO
{
    public class DJAssistantDAO: IDJAssistantDAO
    {
        private const string _getLastIdSQL = "SELECT CAST(SCOPE_IDENTITY() as int);";
        private string _connectionString;

        public DJAssistantDAO(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region DJ

        public int AddDJItem(DJItem item)
        {
            const string sql = "INSERT [DJ] (" +
                                    "email, " +
                                    "displayname, " +
                                    "Hash, " +
                                    "Salt) " +
                               "VALUES (" +
                                    "@email, " +
                                    "@displayname, " +
                                    "@Hash, " +
                                    "@Salt);";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@email", item.Email);
                cmd.Parameters.AddWithValue("@displayname", item.DisplayName);
                cmd.Parameters.AddWithValue("@Hash", item.Hash);
                cmd.Parameters.AddWithValue("@Salt", item.Salt);
                item.Id = (int)cmd.ExecuteScalar();
            }

            return item.Id;
        }
        public bool UpdateDJItem(DJItem item)
        {
            throw new NotImplementedException();
        }
        public bool DeleteDJItem(int DJId)
        {
            throw new NotImplementedException();
        }
        public DJItem GetDJItemById(int DJId)
        {
            throw new NotImplementedException();
        }
        public DJItem GetDJItemByEmail(string email)
        {
            DJItem user = null;
            const string sql = "SELECT * From [dj] WHERE Email = @Email;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    user = GetDJItemFromReader(reader);
                }
            }

            if (user == null)
            {
                throw new Exception("User does not exist.");
            }

            return user;
        }

        private DJItem GetDJItemFromReader(SqlDataReader reader)
        {
            DJItem item = new DJItem();

            item.Id = Convert.ToInt32(reader["Id"]);
            item.Email = Convert.ToString(reader["Email"]);
            item.DisplayName = Convert.ToString(reader["displayname"]);
            item.Salt = Convert.ToString(reader["Salt"]);
            item.Hash = Convert.ToString(reader["Hash"]);

            return item;
        }

        #endregion

        #region Genre

        public int AddGenrItem(GenreItem item)
        {
            throw new NotImplementedException();
        }
        public bool UpdateGenreItem(GenreItem item)
        {
            throw new NotImplementedException();
        }
        public bool DeleteGenreItem(int genreId)
        {
            throw new NotImplementedException();
        }

        public GenreItem GetGenreItemById(int genreId)
        {
            throw new NotImplementedException();
        }
        public GenreItem GetGenreItemByName(string name)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Party

        public int AddPartyItem(PartyItem item)
        {
            throw new NotImplementedException();
        }
        public bool UpdatePartyItem(PartyItem item)
        {
            throw new NotImplementedException();
        }
        public bool DeletePartyItem(int partyId)
        {
            throw new NotImplementedException();
        }
        public PartyItem GetPartyItemById(int partyId)
        {
            throw new NotImplementedException();
        }
        public List<PartyItem> GetUserItemsByDJId(int DJId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region PartySong

        public int AddPartySongItem(PartySongItem item)
        {
            throw new NotImplementedException();
        }
        public bool UpdatePartySongItem(PartySongItem item)
        {
            throw new NotImplementedException();
        }
        public bool DeletePartySongItem(int partySongId)
        {
            throw new NotImplementedException();
        }
        public PartySongItem GetPartySongItemById(int partySongId)
        {
            throw new NotImplementedException();
        }
        public List<PartySongItem> GetPartySongItemByPartyId(int partyId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region SongDJ

        public int AddSongDJItem(SongDJItem item)
        {
            throw new NotImplementedException();
        }
        public bool DeleteSongDJItem(SongDJItem item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region SongGenre

        public int AddSongGenreItem(SongGenreItem item)
        {
            throw new NotImplementedException();
        }
        public bool DeleteSongGenreItem(SongGenreItem item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Song

        public int AddSongItem(SongItem item)
        {
            throw new NotImplementedException();
        }
        public bool UpdateSongItem(SongItem item)
        {
            throw new NotImplementedException();
        }
        public bool DeleteSongItem(int songId)
        {
            throw new NotImplementedException();
        }
        public SongItem GetSongItemById(int songId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
