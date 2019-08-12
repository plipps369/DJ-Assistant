using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DJAssistantLogic.DAO;
using DJAssistantLogic.Models.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DJAssistantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : Controller
    {
        private IDJAssistantDAO _db;

        public GenreController(IDJAssistantDAO db)
        {
            _db = db;
        }

        // GET api/values
        [HttpGet]
        //[Authorize]
        public ActionResult<IEnumerable<string>> Get()
        {

            List<GenreItem> genres = null;
            try
            {
                genres = _db.GetGenreItems();
            }
            catch
            {
                return NotFound();
            }

            return Ok(genres);
        }
    }
}