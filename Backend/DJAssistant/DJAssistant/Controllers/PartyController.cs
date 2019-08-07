using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DJAssistantAPI.Models;
using DJAssistantLogic.DAO;
using DJAssistantLogic.Models.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DJAssistantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartyController : Controller
    {
        private IDJAssistantDAO _db;

        public PartyController(IDJAssistantDAO db)
        {
            _db = db;
        }

        [HttpPost("NewParty")]
        [Authorize]
        public IActionResult NewParty(PartyModel model)
        {
            PartyItem item = new PartyItem();
            string email = User.Identity.Name;
            item.Description = model.Description;
            item.DJId = _db.GetDJItemByEmail(email).Id;
            item.Name = model.Name;
            item.Id = _db.AddPartyItem(item);

            return Ok();
        }

        // GET api/values
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<string>> Get()
        {
            List<PartyItem> parties = _db.GetPartyItemsByDJId(_db.GetDJItemByEmail(User.Identity.Name).Id);
            return Ok(parties);
        }
    }
}