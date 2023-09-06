using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Models
{
    public class PolicyHolder1DTO
    {
        public int PolicyId { get; set; }
        
        public string PolicyHolderName { get; set; } = String.Empty;
       
        public int Age { get; set; }
        
        public string Gender { get; set; } = String.Empty;
        
        public DateTime DateofBirth { get; set; }
        
        public string Email { get; set; } = String.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
       
        public string PolicyType { get; set; } = String.Empty;
    }
}
