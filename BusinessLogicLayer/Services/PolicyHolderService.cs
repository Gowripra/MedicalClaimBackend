using AutoMapper;
using DataAccesLayer.Models;
using Medical_Claim.DataAccess;
using Medical_Claim.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class PolicyHolderService : IPolicyHolderService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public PolicyHolderService(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<PolicyHolder>> Getall()
        {
            return await _context.policyHolders.ToListAsync();
        }
        public async Task<bool> Register(PolicyHolderDTO p)
        {
            var res = await _context.policyHolders.Where(m => m.Email == p.Email).FirstOrDefaultAsync();
            if (res == null)
            {
                byte[] passHash, passSalt;
                CreatePasswordHash(p.Password, out passHash, out passSalt);
                PolicyHolder po = new PolicyHolder()
                {
                    PolicyHolderName = p.PolicyHolderName,
                    Age = p.Age,
                    Gender = p.Gender,
                    DateofBirth = p.DateofBirth,
                    Email = p.Email,
                    PasswordHash = passHash,
                    PasswordSalt = passSalt,
                    PolicyType = p.PolicyType
                };
                Users user1 = new()
                {
                    Email = p.Email,
                    PasswordHash = passHash,
                    PasswordSalt = passSalt,
                    Role = "PolicyHolder"
                };
                _context.policyHolders.Add(po);
                _context.users.Add(user1);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<PolicyHolder1DTO> GetbyId(int id)
        {
            PolicyHolder claim = await _context.policyHolders.AsNoTracking().FirstOrDefaultAsync(x => x.PolicyId == id);
            if (claim != null)
            {
                return _mapper.Map<PolicyHolder1DTO>(claim);
            }
            return new PolicyHolder1DTO(); 
        }
        //public async Task<bool> Login(LoginDTO login)
        //{
        //    var user = _context.policyHolders.Where(u => u.Email == login.email).FirstOrDefault();
        //     if (user != null)
        //    {
        //        if (VerifyPassword(login.password, user.PasswordHash, user.PasswordSalt))
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        //private static bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        //{
        //    using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
        //    {
        //        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); // Create hash using password salt.
        //        for (int i = 0; i < computedHash.Length; i++)
        //        { // Loop through the byte array
        //            if (computedHash[i] != passwordHash[i]) return false; // if mismatch
        //        }
        //    }
        //    return true; //if no mismatches.
        //}
    }
}
