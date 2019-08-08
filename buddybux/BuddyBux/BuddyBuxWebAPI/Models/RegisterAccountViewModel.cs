using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuddyBuxWebAPI.Models
{
    public class RegisterAccountViewModel
    {
        /// <summary>
        /// The user's email.
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// The user's password.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// The user's password.
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// The user's password.
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// The user's currency name.
        /// </summary>
        [Required]
        public string CurrencyName { get; set; }
    }
}
