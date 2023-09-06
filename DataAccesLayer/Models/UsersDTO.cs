using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Models
{
    public class UsersDTO
    {
        public int Id { get; set; }
        [EmailAddress(ErrorMessage = "Please provide a valid Email Address")]

        public string Email { get; set; }

        public string Role { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
