using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuddyBux.BusinessLogic;
using BuddyBux.DAO;
using BuddyBux.Models;
using BuddyBux.Models.Database;
using BuddyBuxWebAPI.Models;
using BuddyBuxWebAPI.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuddyBuxWebAPI.Controllers
{
    /// <summary>
    /// Creates a new account controller used to authenticate the user.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private ITokenGenerator _tokenGenerator;
        private IBuddyBuxDAO _db;

        /// <summary>
        /// Creates a new account controller.
        /// </summary>
        /// <param name="tokenGenerator">A token generator used when creating auth tokens.</param>
        /// <param name="db">Access to the BuddyBux database.</param>
        public AccountController(ITokenGenerator tokenGenerator, IBuddyBuxDAO db)
        {
            _tokenGenerator = tokenGenerator;
            _db = db;
        }

        /// <summary>
        /// Registers a user and provides a bearer token.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public IActionResult Register(RegisterAccountViewModel model)
        {
            IActionResult result = null;

            // Does user already exist
            try
            {
                // Generate a password hash
                var pwdMgr = new PasswordManager(model.Password);
                var roleMgr = new RoleManager(_db);

                // Create a user object
                var user = new UserItem
                {
                    Hash = pwdMgr.Hash,
                    Salt = pwdMgr.Salt,
                    RoleId = roleMgr.UserRoleId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email
                };

                // Create a currency object
                var currency = new CurrencyItem
                {
                    Name = model.CurrencyName
                };

                var userAccount = new UserAccountModel();
                userAccount.User = user;
                userAccount.Currency = currency;
                userAccount.Store = new StoreItem();

                // Save the user account
                _db.AddUserAccount(userAccount);

                // Generate a token
                var token = _tokenGenerator.GenerateToken(user.Email, roleMgr.RoleName);

                result = Ok(token);
            }
            catch(Exception)
            {
                result = BadRequest(new { Message = "Username has already been taken." });
            }

            // Return the token
            return result;
        }
        
        /// <summary>
        /// Authenticates the user and provides a bearer token.
        /// </summary>
        /// <param name="model">An object including the user's credentials.</param> 
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Login(AuthenticationViewModel model)
        {
            // Assume the user is not authorized
            IActionResult result = Unauthorized();

            try
            {
                var roleMgr = new RoleManager(_db);
                roleMgr.User = _db.GetUserItemByEmail(model.Email);

                // Generate a password hash
                var pwdMgr = new PasswordManager(model.Password, roleMgr.User.Salt);

                if (pwdMgr.Verify(roleMgr.User.Hash))
                {
                    // Create an authentication token
                    var token = _tokenGenerator.GenerateToken(roleMgr.User.Email, roleMgr.RoleName);

                    // Switch to 200 OK
                    result = Ok(token);
                }
            }
            catch (Exception)
            {
                result = BadRequest(new { Message = "Username or password is invalid." });
            }
            
            return result;
        }
    }
}