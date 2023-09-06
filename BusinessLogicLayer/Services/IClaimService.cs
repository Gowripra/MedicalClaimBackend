using DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public interface IClaimService
    {
        public Task<List<ClaimsDTO>> GetAll();
        public Task<ClaimsDTO> GetClaim(int id);
        public Task<ClaimsDTO> NewClaim(ClaimsCreateDTO claimsCreateDTO);
        public Task<bool> UpdateClaimStatus(int id, string status);
        //public Task<ClaimDetailsDTO> GetClaimDetails(string email);
    }
}
