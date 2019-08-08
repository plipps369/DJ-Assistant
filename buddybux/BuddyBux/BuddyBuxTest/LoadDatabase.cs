using BuddyBux.BusinessLogic;
using BuddyBux.DAO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuddyBuxTest
{
    [TestClass]
    public class LoadDatabase
    {
        private const string _connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=BuddyBux;Integrated Security=true";

        //[TestMethod]
        public void PopulateDatabase()
        {
            IBuddyBuxDAO db = new BuddyBuxDAO(_connectionString);

            TestManager.PopulateDatabaseWithUserAccounts(db);
            TestManager.PopulateDatabaseWithTradeStatus(db);
        }
    }
}
