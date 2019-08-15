using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DJAssistantAPI.Models;
using DJAssistantLogic.DAO;
using DJAssistantLogic.Models.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

        [HttpPost]
        [Authorize]
        public IActionResult NewParty([FromBody] PartyModel model)
        {
            IActionResult result = Unauthorized();
            try
            {
                PartyItem item = new PartyItem();
                string email = User.Identity.Name;
                //string email = "mm@gmail.com";
                item.Description = model.Description;
                item.DJId = _db.GetDJItemByEmail(email).Id;
                item.Name = model.Name;
                item.Id = _db.AddPartyItem(item);

                // Add genre linked to song id
                if (model.GenresId != null)
                {
                    foreach (int x in model.GenresId)
                    {
                        PartyGenreItem partyGenre = new PartyGenreItem();
                        partyGenre.GenreId = x;
                        partyGenre.PartyId = item.Id;
                        _db.AddPartyGenreItem(partyGenre);
                    }
                }

                result = Ok();
            }
            catch
            {
                result = BadRequest(new { Message = "Add party failed." });
            }

            return result;
        }

        //GET api/values
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<string>> Get()
        {

            List<PartyItem> parties = null;
            try
            {
                parties = _db.GetPartyItemsByDJId(_db.GetDJItemByEmail(User.Identity.Name).Id);
            }
            catch
            {
                return BadRequest(new { Message = "Get parties Failed" });
            }
            return Ok(parties);
        }

        [HttpGet("{id}")]
        public ActionResult<string> GetPartyById(int id)
        {
            PartyItem party = null;
            ActionResult result = null;
            try
            {
                party = _db.GetPartyItemById(id);
            }
            catch
            {
                return BadRequest(new { Message = "Get party failed" });

            }

            if (party == null)
            {
                result = BadRequest(new { Message = "Party not found" });
            }
            else
            {
                result = Ok(party);
            }

            return result;
        }

        [HttpGet("PartySongs/{partyId}")]
        [Authorize]
        public ActionResult<IEnumerable<string>> GetPartySongs(int partyId)
        {
            List<PartySongItemWithDetails> partySongItems = _db.GetPartySongItemWithDetailsByPartyId(partyId);
            return Ok(partySongItems);
        }

       [HttpPost("MarkedAsPlayed/{partySongId}")]
       [Authorize]
        public IActionResult MarkedSongAsPlayedByParytSongId(int partySongId)
        {
            PartySongItem partySong = _db.GetPartySongItemById(partySongId);

            if(partySong.Played == true)
            {
                return BadRequest(new { Message = "song was already played" });
            }
            else
            {
                partySong.Played = true;
                partySong.PlayOrder = _db.GetTotalPlayedSongsByPartyId(partySong.PartyId);
                _db.UpdatePartySongItem(partySong);
            }

            return Ok();
        }
    }    
}