﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DJAssistantLogic.DAO;
using DJAssistantLogic.Models.Database;
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
        public ActionResult<IEnumerable<string>> Get()
        {
            List<GenreItem> genres = _db.GetGenreItems();
            return Ok(genres);
        }
    }
}