using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DJAssistantAPI.Models;
using DJAssistantAPI.Providers.Security;
using DJAssistantLogic.DAO;
using DJAssistantLogic.Models;
using DJAssistantLogic.Models.Database;
using Microsoft.AspNetCore.Mvc;

namespace DJAssistantAPI.Controllers
{
    /// <summary>
    /// Creates a new account controller used to authenticate the user.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DJController : Controller
    {

        private ITokenGenerator tokenGenerator;
        private IPasswordHasher passwordHasher;
        private IDJAssistantDAO _db;

        /// <summary>
        /// Creates a new account controller.
        /// </summary>
        /// <param name="tokenGenerator">A token generator used when creating auth tokens.</param>
        /// <param name="passwordHasher">A password hasher used when hashing passwords.</param>
        /// <param name="userDao">A data access object to store user data.</param>
        public DJController(ITokenGenerator tokenGenerator, IPasswordHasher passwordHasher, IDJAssistantDAO db)
        {
            this.tokenGenerator = tokenGenerator;
            this.passwordHasher = passwordHasher;
            this._db = db;
        }

        /// <summary>
        /// Registers a user and provides a bearer token.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public IActionResult Register(DJRegistrationModel model)
        {
            // Does user already exist
            if (_db.GetDJItemByEmail(model.Email) != null)
            {
                return BadRequest(new
                {
                    Message = "Username has already been taken."
                });
            }

            // Generate a password hash
            var passwordHash = passwordHasher.ComputeHash(model.Password);

            // Create a user object
            var dJ = new DJItem { Email = model.Email, Hash = passwordHash.Password, Salt = passwordHash.Salt, DisplayName = model.DisplayName };

            // Save the user
            _db.AddDJItem(dJ);

            // Generate a token
            var token = tokenGenerator.GenerateToken(dJ.Email, "DJ");

            // Return the token
            return Ok(token);
        }


        /// <summary>
        /// Authenticates the user and provides a bearer token.
        /// </summary>
        /// <param name="model">An object including the user's credentials.</param> 
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Login(DJLoginModel model)
        {
            // Assume the user is not authorized
            IActionResult result = Unauthorized();

            // Get the user by username
            var dJ = _db.GetDJItemByEmail(model.email);

            // If we found a user and the password has matches
            if (dJ != null && passwordHasher.VerifyHashMatch(dJ.Hash, model.password, dJ.Salt))
            {
                // Create an authentication token
                var token = tokenGenerator.GenerateToken(dJ.DisplayName, "DJ"
                    );

                // Switch to 200 OK
                result = Ok(token);
            }

            return result;
        }
    }

}