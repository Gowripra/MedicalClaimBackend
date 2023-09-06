using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Models
{
    public class Claims
    {
        [Key]
        public int ClaimId { get; set; }
        public string ClaimNumber { get; set; }
        public int PolicyNumber { get; set; }
        public string PolicyHolderName { get; set; }
        public string PolicyType { get; set; }
        public string CashType { get; set; }
        public DateTime DateofAdmitted { get; set; }
        public DateTime DateofDischarged { get; set; }

        public int RemainingClaims { get; set; } 
        public string Notes { get; set; }
        public string Status { get; set; }
    }
}
