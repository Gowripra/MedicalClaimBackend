using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Models
{
    public class ClaimDetailsDTO
    {
        public int claimId { get; set; }
        public int PolicyNumber { get; set; }
        public string PolicyHolderName { get; set; }
    }
}
