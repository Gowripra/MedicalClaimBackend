using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Models
{
    public class LoginRequestDTO
    {
        [EmailAddress(ErrorMessage = "Please provide a valid Email Address")]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
