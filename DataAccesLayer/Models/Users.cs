using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        [EmailAddress(ErrorMessage = "Please provide a valid Email Address")]
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Role is Required")]
        public string Role { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
