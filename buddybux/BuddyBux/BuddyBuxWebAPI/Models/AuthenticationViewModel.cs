using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuddyBuxWebAPI.Models
{
    public class AuthenticationViewModel
    {
        public enum Direction
        {
            Up = 1,
            Down = 2,
            Left = 3,
            Right = 4
        }

        void Test(Direction direction)
        {
            Test(Direction.Down);
            direction.ToString();
        }

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
    }
}
