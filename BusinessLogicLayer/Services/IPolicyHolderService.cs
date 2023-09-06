using DataAccesLayer.Models;
using Medical_Claim.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public interface IPolicyHolderService
    {
        public Task<List<PolicyHolder>> Getall();
        public Task<bool> Register(PolicyHolderDTO p);
        public Task<PolicyHolder1DTO> GetbyId(int id);
        //public Task<bool> Login(LoginDTO login);
    }
}
