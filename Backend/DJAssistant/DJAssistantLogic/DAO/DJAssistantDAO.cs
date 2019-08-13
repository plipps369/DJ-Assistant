using DJAssistantLogic.Models.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using System.Transactions;


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

            //if (user == null)
            //{
            //    throw new Exception("User does not exist.");
            //}

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

        public int AddGenreItem(GenreItem item)
        {
            // Does @Name work here? Syntax in SQL after VALUES is ('thing');
            const string sql = "INSERT INTO Genre (Name)" +
                               "VALUES (@Name);";
            // Connect to the database
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                // Parameretize query
                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@Name", item.Name);


                // Execute SQL command
                item.Id = (int)cmd.ExecuteScalar();
            }

            return item.Id;
        }

        public bool UpdateGenreItem(GenreItem item)
        {
            throw new NotImplementedException();
        }
        public bool DeleteGenreItem(int genreId)
        {
            throw new NotImplementedException();
        }
 
        private GenreItem GetGenreItemFromReader(SqlDataReader reader)
        {
            GenreItem item = new GenreItem();
            item.Id = Convert.ToInt32(reader["id"]);
            item.Name = Convert.ToString(reader["name"]);

            return item;
        }

        public GenreItem GetGenreItemById(int genreId)
        {
            // Does @Name work here? Syntax in SQL after VALUES is ('thing');
            const string sql = "SELECT id, Name FROM Genre " +
                               "WHERE id = (@genreID);";
            GenreItem result = null;
            // Connect to the database
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                // Parameretize query
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@genreId", genreId);
                var reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    result = GetGenreItemFromReader(reader);
                }
            }

            return result;
        }
        
        public GenreItem GetGenreItemByName(string name)
        {
            // Does @Name work here? Syntax in SQL after VALUES is ('thing');
            const string sql = "SELECT id, Name FROM Genre " +
                               "WHERE name = (@name);";
            GenreItem result = null;
            // Connect to the database
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                // Parameretize query
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result = GetGenreItemFromReader(reader);
                }
            }

            return result;
        }

        public List<GenreItem> GetGenreItems()
        {
            List<GenreItem> genres = new List<GenreItem>();

            const string sql = "Select * From [Genre];";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    genres.Add(GetGenreItemFromReader(reader));
                }
            }

            return genres;
        }

        #endregion

        #region Party

        public int AddPartyItem(PartyItem item)
        {
            const string sql = "INSERT [Party] (" +
                                     "DJ_id, " +
                                     "name, " +
                                     "Description) " +
                                "VALUES (" +
                                     "@DJid, " +
                                     "@name, " +
                                     "@Description);";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@DJid", item.DJId);
                cmd.Parameters.AddWithValue("@name", item.Name);
                cmd.Parameters.AddWithValue("@Description", item.Description);
             
                item.Id = (int)cmd.ExecuteScalar();
            }

            return item.Id;
        }
        public bool UpdatePartyItem(PartyItem item)
        {
            throw new NotImplementedException();
        }
        public bool DeletePartyItem(int partyId)
        {
            bool isSuccessful = false;

            const string sql = "DELETE FROM [Party] WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", partyId);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    isSuccessful = true;
                }
            }

            return isSuccessful;
        }
        public PartyItem GetPartyItemById(int partyId)
        {
            PartyItem party = null;
            const string sql = "SELECT * From [party] WHERE id = @partyId;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partyId", partyId);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    party = GetPartyItemFromReader(reader);
                }
            }

            //if (user == null)
            //{
            //    throw new Exception("User does not exist.");
            //}

            return party;
        }

        public List<PartyItem> GetPartyItemsByDJId(int DJId)
        {
            List<PartyItem> parties = new List<PartyItem>();

            const string sql = "Select * From [Party] Where DJ_id = @DJId;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@DJid", DJId);
                var reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    parties.Add(GetPartyItemFromReader(reader));
                }
            }

            return parties;
        }

        public PartyItem GetPartyByName(string name)
        {
            PartyItem party = null;

            const string sql = "Select * From [Party] Where Name = @Name;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Name", name);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    party = GetPartyItemFromReader(reader);
                }
            }


            return party;

        }

        private PartyItem GetPartyItemFromReader(SqlDataReader reader)
        {
            PartyItem item = new PartyItem();

            item.Id = Convert.ToInt32(reader["Id"]);
            item.DJId = Convert.ToInt32(reader["DJ_id"]);
            item.Description = Convert.ToString(reader["Description"]);
            item.Name = Convert.ToString(reader["Name"]);
           

            return item;
        }

        #endregion

        #region PartySong

        public int AddPartySongItem(PartySongItem item)
        {
            const string sql = "INSERT [Party_Song] (" +
                                   "Song_Id, " +
                                   "Party_Id, " +
                                   "Play_Order, " +
                                   "Played) " +
                              "VALUES (" +
                                   "@SongId, " +
                                   "@PartyId, " +
                                   "@Play_Order, " +
                                   "@Played);";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@SongId", item.SongId);
                cmd.Parameters.AddWithValue("@PartyId", item.PartyId);
                cmd.Parameters.AddWithValue("@Play_Order", item.PlayOrder);
                cmd.Parameters.AddWithValue("@Played", item.Played);
                item.Id = (int)cmd.ExecuteScalar();
            }

            return item.Id;
        }
        public bool UpdatePartySongItem(PartySongItem item)
        {
            bool isSuccessful = false;

            const string sql = "UPDATE [Party_Song] SET Song_id = @SongId, " +
                                                 "Party_id = @PartyId, " +
                                                 "Play_order = @PlayOrder, " +
                                                 "Played = @Played " +
                                                 "WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@SongId", item.SongId);
                cmd.Parameters.AddWithValue("@PartyId", item.PartyId);
                cmd.Parameters.AddWithValue("@PlayOrder", item.PlayOrder);
                cmd.Parameters.AddWithValue("@Played", item.Played);
                cmd.Parameters.AddWithValue("@Id", item.Id);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    isSuccessful = true;
                }
            }

            return isSuccessful;
        }
        public bool DeletePartySongItem(int partySongId)
        {
            throw new NotImplementedException();
        }

        public PartySongItem GetPartySongItemById(int partySongId)
        {
            PartySongItem partySong = null;
            const string sql = "SELECT * From [Party_Song] WHERE Id = @partySongId;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partySongId", partySongId);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    partySong = GetParySongItemFromReader(reader);
                }
            }

            //if (user == null)
            //{
            //    throw new Exception("User does not exist.");
            //}

            return partySong;
        }

        public List<PartySongItemWithDetails> GetPartySongItemWithDetailsByPartyId(int partyId)
        {
            List<PartySongItemWithDetails> partySongs = new List<PartySongItemWithDetails>();

            const string sql = "Select * From [Party_Song] " +
                               "Join [Song] on Party_Song.Song_Id = Song.Id " +
                               "Where Party_Id = @partyId;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partyId", partyId);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    partySongs.Add(GetPartySongItemWithDetailsFromReader(reader));
                }
            }

            return partySongs;
        }

        private PartySongItem GetParySongItemFromReader(SqlDataReader reader)
        {
            PartySongItem item = new PartySongItem();

            item.Id = Convert.ToInt32(reader["Id"]);
            item.SongId = Convert.ToInt32(reader["Song_id"]);
            item.PartyId = Convert.ToInt32(reader["Party_id"]);
            item.PlayOrder = Convert.ToInt32(reader["Play_Order"]);
            item.Played = Convert.ToBoolean(reader["Played"]);

            return item;
        }
        public int GetTotalSongsRequestedByPartyId(int partyId)
        {
            int num = 0;
            const string sql = "Select count(Party_Song.Id) as total From [Party_Song] Where Party_Id = @partyId;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partyId", partyId);
                var reader = cmd.ExecuteReader();
                reader.Read();
                num = Convert.ToInt32(reader["total"]);
            }
            return num;
        }

        public List<PartySongItemWithDetails> GetPartySongsNotPlayedByPartyName(string partyName)
        {
            List<PartySongItemWithDetails> partySongs = new List<PartySongItemWithDetails>();

            const string sql = "Select top 5 * " +
                               "From [Party_Song] " +
                               "Join [Song] on Party_Song.Song_Id = Song.Id " +
                               "Join [Party] on Party_Song.Party_Id = Party.Id " +
                               "Where Party.Name = @partyName and Played = 0 " +
                               "Order by play_order asc;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partyName", partyName);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    partySongs.Add(GetPartySongItemWithDetailsFromReader(reader));
                }
            }

            return partySongs;
        }

        public List<PartySongItemWithDetails> GetPartySongsPlayedByPartyName(string partyName)
        {
            List<PartySongItemWithDetails> partySongs = new List<PartySongItemWithDetails>();

            const string sql = "Select top 5 * " +
                               "From [Party_Song] " +
                               "Join [Song] on Party_Song.Song_Id = Song.Id " +
                               "Join [Party] on Party_Song.Party_Id = Party.Id " +
                               "Where Party.Name = @partyName and Played = 1 " +
                               "Order by play_order desc;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partyName", partyName);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    partySongs.Add(GetPartySongItemWithDetailsFromReader(reader));
                }
            }

            return partySongs;
        }

        private PartySongItemWithDetails GetPartySongItemWithDetailsFromReader(SqlDataReader reader)
        {
            PartySongItemWithDetails item = new PartySongItemWithDetails();

            item.Id = Convert.ToInt32(reader["Id"]);
            item.SongId = Convert.ToInt32(reader["Song_id"]);
            item.PartyId = Convert.ToInt32(reader["Party_id"]);
            item.PlayOrder = Convert.ToInt32(reader["Play_Order"]);
            item.Played = Convert.ToBoolean(reader["Played"]);
            item.Artist = Convert.ToString(reader["Artist"]);
            item.Title = Convert.ToString(reader["Title"]);

            return item;
        }

        #endregion

        #region SongDJ

        public void AddSongDJItem(SongDJItem item)
        {
            const string sql = "INSERT [Song_DJ] (" +
                                    "Song_id, " +
                                    "DJ_id) " +
                               "VALUES (" +
                                    "@SongId, " +
                                    "@DJId);";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@DJId", item.DJId);
                cmd.Parameters.AddWithValue("@SongId", item.SongId);
                
                cmd.ExecuteScalar();
            }

            return;
        }
        public bool DeleteSongDJItem(SongDJItem item)
        {
            bool isSuccessful = false;

            const string sql = "DELETE FROM [Song_DJ] WHERE Song_id = @SongId and DJ_id = @DJId;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@SongId", item.SongId);
                cmd.Parameters.AddWithValue("@DJid", item.DJId);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    isSuccessful = true;
                }
            }

            return isSuccessful;
        }

        #endregion

        #region SongGenre

        public void AddSongGenreItem(SongGenreItem item)
        {
            const string sql = "INSERT [Song_Genre] (" +
                                    "Genre_Id, " +
                                    "Song_Id) " +
                               "VALUES (" +
                                    "@GenreId, " +
                                    "@SongId);";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@GenreId", item.GenreId);
                cmd.Parameters.AddWithValue("@SongId", item.SongId);
                cmd.ExecuteScalar();
            }

            
        }
        public bool DeleteSongGenreItem(SongGenreItem item)
        {
            bool isSuccessful = false;

            const string sql = "DELETE FROM [Song_Genre] WHERE Song_id = @SongId and Genre_id = @GenreId;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@SongId", item.SongId);
                cmd.Parameters.AddWithValue("@GenreId", item.GenreId);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    isSuccessful = true;
                }
            }

            return isSuccessful;
        }

        #endregion

        #region PartyGenre
        public void AddPartyGenreItem(PartyGenreItem partyGenre)
        {
            const string sql = "INSERT [Party_Genre] (" +
                                   "Genre_Id, " +
                                   "Party_Id) " +
                              "VALUES (" +
                                   "@GenreId, " +
                                   "@PartyId);";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@GenreId", partyGenre.GenreId);
                cmd.Parameters.AddWithValue("@PartyId", partyGenre.PartyId);
                cmd.ExecuteScalar();
            }
        }
        #endregion

        #region Song

        public int AddSongItem(SongItem item)
        {
            const string sql = "INSERT [Song] (" +
                                    "Title, " +
                                    "Artist, " +
                                    "Length, " +
                                    "Explicit) " +
                               "VALUES (" +
                                    "@Title, " +
                                    "@Artist, " +
                                    "@Length, " +
                                    "@Explicit);";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql + _getLastIdSQL, conn);
                cmd.Parameters.AddWithValue("@Title", item.Title);
                cmd.Parameters.AddWithValue("@Artist", item.Artist);
                cmd.Parameters.AddWithValue("@Length", item.Length);
                cmd.Parameters.AddWithValue("@Explicit", item.Explicit);
                item.Id = (int)cmd.ExecuteScalar();
            }

            return item.Id;
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
            SongItem item = null;
            const string sql = "SELECT * From [Song] WHERE Id = @Id;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", songId);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    item = GetSongItemFromReader(reader);
                }
            }

            //if (user == null)
            //{
            //    throw new Exception("User does not exist.");
            //}

            return item;
        }

        public List<SongItem> GetSongsByPartyId(int partyID)
        {
            List<SongItem> songs = new List<SongItem>();

            if (NumberOfGenresByPartyId(partyID) == 0)
            {
                const string sql = "select Song.Id, Song.Title, Song.Artist, Song.Length, Song.Explicit " +
                                  "from [Song] " +
                                  "join Song_DJ on Song.Id = Song_DJ.Song_id " +
                                  "where Song_DJ.DJ_id = (select DJ_id from Party where Party.id = @partyId);";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@partyId", partyID);
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        songs.Add(GetSongItemFromReader(reader));
                    }
                }
            }
            else
            {

                const string sql = "select Song.Id, Song.Title, Song.Artist, Song.Length, Song.Explicit " +
                                   "from [Song] " +
                                   "join Song_Genre on Song.id = Song_Genre.Song_id " +
                                   "join Song_DJ on Song.Id = Song_DJ.Song_id " +
                                   "where Song_Genre.Genre_id in (select Genre_id " +
                                                     "from Party_Genre " +
                                                     "where Party_Genre.Party_Id = @partyId) " +
                                         "and Song_DJ.DJ_id = (select DJ_id from Party where Party.id = @partyId);";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@partyId", partyID);
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        songs.Add(GetSongItemFromReader(reader));
                    }
                }
            }
            return songs;
        }

        private int NumberOfGenresByPartyId(int partyId)
        {
            int num = 0;
            const string sql = "Select count(Party_Genre.Genre_Id) as total From [Party_Genre] Where Party_Id = @partyId;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@partyId", partyId);
                var reader = cmd.ExecuteReader();
                reader.Read();
                num = Convert.ToInt32(reader["total"]);
            }
            return num;
        }

        private SongItem GetSongItemFromReader(SqlDataReader reader)
        {
            SongItem item = new SongItem();

            item.Id = Convert.ToInt32(reader["Id"]);
            item.Title = Convert.ToString(reader["Title"]);
            item.Artist = Convert.ToString(reader["Artist"]);
            item.Length = Convert.ToInt32(reader["Length"]);
            item.Explicit = Convert.ToBoolean(reader["Explicit"]);

            return item;
        }

        public List<SongItem> GetSongsByDJId(int dJId)
        {
            List<SongItem> songs = new List<SongItem>();

            const string sql = "select Song.Id, Song.Title, Song.Artist, Song.Length, Song.Explicit " +
                               "from [Song] " +
                               "join Song_DJ on Song.Id = Song_DJ.Song_id " +
                               "where Song_DJ.DJ_Id = @dJId;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@dJId", dJId);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    songs.Add(GetSongItemFromReader(reader));
                }
            }

            return songs;
        }

        #endregion
    }
}
