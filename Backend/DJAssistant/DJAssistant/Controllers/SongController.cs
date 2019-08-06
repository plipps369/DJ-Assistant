using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DJAssistantAPI.Models;
using DJAssistantLogic.DAO;
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
        public IActionResult NewSong(SongModel model)
        {

            // start transaction

            // Add song

            // Add genre linked to song id

            return Ok();
        }
}