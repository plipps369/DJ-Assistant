using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
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
    public class SongController : Controller
    {
        private IDJAssistantDAO _db;

        public SongController(IDJAssistantDAO db)
        {
            _db = db;
        }

        [HttpPost("NewSong")]
        [Authorize]
        public IActionResult NewSong(SongModel model)
        {
            string email = User.Identity.Name;
            // start transaction
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    // Add song
                    SongItem item = new SongItem();
                    item.Artist = model.Artist;
                    item.Explicit = model.Explicit;
                    item.Length = model.Length;
                    item.Title = model.Title;
                    item.Id = _db.AddSongItem(item);
                    SongDJItem songDJ = new SongDJItem();
                    songDJ.SongId = item.Id;
                    songDJ.DJId = _db.GetDJItemByEmail(User.Identity.Name).Id;
                    _db.AddSongDJItem(songDJ);

                    // Add genre linked to song id
                    if (model.GenresId != null)
                    {
                        foreach (int x in model.GenresId)
                        {
                            SongGenreItem songGenre = new SongGenreItem();
                            songGenre.GenreId = x;
                            songGenre.SongId = item.Id;
                            _db.AddSongGenreItem(songGenre);
                        }
                    }
                    scope.Complete();
                }
            }
            catch
            {
                return BadRequest(new { Message = "Add song failed." });
            }
            return Ok();

        }

        [HttpPost("AddSong")]
        [Authorize]
        public IActionResult AddSongToParty(PartySongModel model)
        {
            IActionResult result = null;
            try
            {
                PartySongItem songPartyItem = new PartySongItem();
                songPartyItem.PartyId = model.PartyId;
                songPartyItem.SongId = model.SongId;
                _db.AddPartySongItem(songPartyItem);
                result = Ok();
            }
            catch
            {
                result = BadRequest(new { Message = "Add song to party failed" });
            }
            return result;
        }
    }
}