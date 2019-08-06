using DJAssistantAPI.Providers.Security;
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

        private PasswordHasher _hasher = new PasswordHasher();

        private string  _password = "a";

        [TestInitialize]
        public void Initialize()
        {
            // Initialize a new transaction scope. This automatically begins the transaction.
            _tran = new TransactionScope();
            _hasher = new PasswordHasher();
            // Test add user
            _dj = new DJItem();
            _dj.DisplayName = "DJ DJ DJ";
            
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
