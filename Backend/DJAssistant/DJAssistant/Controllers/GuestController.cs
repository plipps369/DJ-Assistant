using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DJAssistantAPI.Models;
using DJAssistantLogic.DAO;
using DJAssistantLogic.Models.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DJAssistantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private IDJAssistantDAO _db;

        public GuestController(IDJAssistantDAO db)
        {
            _db = db;
        }
        [HttpGet("{partyName}")]
        public ActionResult<string> GetSongListForPartyByName(string partyName)
        {
            List<SongItem> songs = null;
            //try
            //{
            songs = _db.GetSongsByPartyId(_db.GetPartyByName(partyName).Id);
            //}
            //catch
            //{
            //    return BadRequest(new { Message = "Get songs failed." });
            //}
            return Ok(songs);
        }

        [HttpPost]
        public IActionResult RequestSongByPartyNameAndSongId(SongRequestModel songModel)
        {
            IActionResult result = Unauthorized();

            //try
            //{
                PartySongItem partySong = new PartySongItem();
                PartyItem party = _db.GetPartyByName(songModel.PartyName);
                partySong.PartyId = party.Id;
                partySong.Played = false;
                partySong.SongId = songModel.SongId;
                partySong.PlayOrder = _db.GetTotalSongsRequestedByPartyId(party.Id) + 1;
                partySong.Id = _db.AddPartySongItem(partySong);

                result =  Ok();
            //}
            //catch
            //{
            //    result = BadRequest(new { Message = "Request failed." });
            //}

                
            
            


            return result;
        }

        [HttpGet("lastFive/{partyName}")]
        public ActionResult<string> GetLastFiveByPartyName(string partyName)
        {
            List<PartySongItemWithDetails> partySongItems = _db.GetPartySongsPlayedByPartyName(partyName);
            return Ok(partySongItems);
        }

        [HttpGet("nextFive/{partyName}")]
        public ActionResult<string> GetNextFiveByName(string partyName)
        {
            List<PartySongItemWithDetails> partySongItems = _db.GetPartySongsNotPlayedByPartyName(partyName);
            return Ok(partySongItems);
        }
    }
}