﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Models
{
    public class LoginResponseDTO
    {
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public int PolicyNumber { get; set; }
        public string PolicyHolderName { get; set; }
        public string PolicyType { get; set; }
    }
}
