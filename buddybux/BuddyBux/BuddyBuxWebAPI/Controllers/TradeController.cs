using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuddyBux.BusinessLogic;
using BuddyBux.DAO;
using BuddyBux.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuddyBuxWebAPI.Controllers
{
    public class TradeController : ControllerBase
    {
        private IBuddyBuxDAO _db;
        private TradeManager tradeManager;

        public TradeController(IBuddyBuxDAO db)
        {
            _db = db;
            tradeManager = new TradeManager(_db);
        }

        [HttpGet("my-trades-orignated")]
        [Authorize]
        public IActionResult GetMyTradesOrignated()
        {
            // Assume the user is not authorized
            IActionResult result = Unauthorized();

            try
            {
                var trades = _db.GetTradeItemByOriginatorId(_db.GetUserItemByEmail(User.Identity.Name).Id);
                string t = User.Identity.Name;
                Console.WriteLine(t);
                result = Ok(trades);
            }
            catch (Exception)
            {
                result = BadRequest(new { Message = "Get Trades failed." });
            }

            return result;
        }

        [HttpGet("my-trades-offered")]
        [Authorize]
        public IActionResult GetMyTradesDesignee()
        {
            // Assume the user is not authorized
            IActionResult result = Unauthorized();

            try
            {
                var trades = _db.GetTradeItemByDesigneeId(_db.GetUserItemByEmail(User.Identity.Name).Id);
                string t = User.Identity.Name;
                Console.WriteLine(t);
                result = Ok(trades);
            }
            catch (Exception)
            {
                result = BadRequest(new { Message = "Get Trades failed." });
            }

            return result;
        }

        [HttpPost("new-trade")]
        [Authorize]
        public IActionResult AddProductToStore(TradeModel newTrade)
        {
            // Assume the user is not authorized
            IActionResult result = Unauthorized();

            try
            {
                tradeManager.NewTrade(newTrade);
                result = Ok(newTrade);

            }
            catch (Exception)
            {
                result = BadRequest(new { Message = "Add trade failed." });
            }

            return result;
        }

        [HttpPost("accept-trade")]
        [Authorize]
        public IActionResult AcceptTrade(int tradeId)
        {
            // Assume the user is not authorized
            IActionResult result = Unauthorized();

            try
            {
                tradeManager.AcceptTrade(tradeId, User.Identity.Name);
                //not sure what I should do with result here
                //result = Ok(newTrade);

            }
            catch (Exception)
            {
                result = BadRequest(new { Message = "Add trade failed." });
            }

            return result;
        }

        [HttpPost("reject-trade")]
        [Authorize]
        public IActionResult RejectTrade(int tradeId)
        {
            // Assume the user is not authorized
            IActionResult result = Unauthorized();

            try
            {
                tradeManager.RejectTrade(tradeId, User.Identity.Name);
                //not sure what I should do with result here
                //result = Ok(newTrade);

            }
            catch (Exception)
            {
                result = BadRequest(new { Message = "Add trade failed." });
            }

            return result;
        }

    }
}