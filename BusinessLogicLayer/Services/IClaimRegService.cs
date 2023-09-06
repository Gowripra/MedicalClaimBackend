using DataAccesLayer.Models;
using Medical_Claim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public interface IClaimRegService
    {
        public Task<List<ClaimProcessRegistration>> GetallClaims();
        public Task<bool> ClaimRegister(ClaimProcessRegistrationDTO p);
    }
}
