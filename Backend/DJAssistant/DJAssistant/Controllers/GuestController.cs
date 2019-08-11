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
            List<SongItem> songs = _db.GetSongsByPartyId(_db.GetPartyByName(partyName).Id);

            return Ok(songs);
        }

        [HttpPost]
        public ActionResult<string> RequestSongByPartyNameAndSongId(SongRequestModel songModel)
        {
            PartySongItem partySong = new PartySongItem();
            PartyItem party = _db.GetPartyByName(songModel.PartyName);
            partySong.PartyId = party.Id;
            partySong.Played = false;
            partySong.SongId = songModel.SongId;
            partySong.PlayOrder = _db.GetTotalSongsRequestedByPartyId(party.Id);
            return Ok();
        }
    }
}