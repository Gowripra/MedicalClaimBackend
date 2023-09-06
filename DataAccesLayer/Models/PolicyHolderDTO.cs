using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Models
{
    public class PolicyHolderDTO
    {
        public int PolicyId { get; set; }
        public string PolicyHolderName { get; set; } = String.Empty;
        public int Age { get; set; }
        public string Gender { get; set; } = String.Empty;
        public DateTime DateofBirth { get; set; }
        public string Email { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string PolicyType { get; set; } = String.Empty;
    }
}
