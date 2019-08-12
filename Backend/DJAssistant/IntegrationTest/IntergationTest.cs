using DJAssistantAPI.Providers.Security;
using DJAssistantLogic.DAO;
using DJAssistantLogic.Models.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;

namespace IntegrationTest
{
    [TestClass]
    public class IntergationTest
    {
        private TransactionScope _tran;
       
        private DJItem _dj;

        private SongItem _song;

        //private string  _password = "a";

        //private ITokenGenerator _tokenGenerator;
        //private IPasswordHasher _passwordHasher;
        private IDJAssistantDAO _db;

        [TestInitialize]
        public void Initialize()
        {
            // Initialize a new transaction scope. This automatically begins the transaction.
            _tran = new TransactionScope();
            //_hasher = new PasswordHasher();
            // Test add user
            _db = new DJAssistantDAO("Data Source=.\\SQLEXPRESS;Initial Catalog=DJAssistant;Integrated Security=True");
            _dj = new DJItem();
            _dj.DisplayName = "DJ DJ DJ";
            _dj.Email = "Chris@techelevator.com";
            _dj.Salt = "salt";
            _dj.Hash = "Hash";

            _dj.Id = _db.AddDJItem(_dj);

            _song = new SongItem();
            _song.Artist = "Paul";
            _song.Explicit = true;
            _song.Length = 0;
            _song.Title = "Paul is a good guy.";

            _song.Id = _db.AddSongItem(_song);
        }

        [TestMethod]
        public void AddDJ()
        {
            DJItem dj = _db.GetDJItemByEmail(_dj.Email);
            Assert.AreEqual(_dj.DisplayName, dj.DisplayName);
            Assert.AreEqual(_dj.Email, dj.Email);
            Assert.AreEqual(_dj.Hash, dj.Hash);
            Assert.AreEqual(_dj.Salt, dj.Salt);
        }

        [TestMethod]
        public void AddSong()
        {
            SongItem song = _db.GetSongItemById(_song.Id);
            Assert.AreEqual(_song.Artist, song.Artist);
            Assert.AreEqual(_song.Explicit, song.Explicit);
            Assert.AreEqual(_song.Length, song.Length);
            Assert.AreEqual(_song.Title, song.Title);
        }

        [TestMethod]
        public void GenreTest()
        {
            GenreItem genre = new GenreItem();
            genre.Name = "Pauls party";
            _db.AddGenreItem(genre);
        }

        /// <summary>
        /// Cleanup runs after every single test
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            _tran.Dispose();
        }
    }
}
