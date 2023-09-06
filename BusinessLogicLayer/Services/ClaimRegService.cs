using DataAccesLayer.Models;
using Medical_Claim.DataAccess;
using Medical_Claim.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class ClaimRegService:IClaimRegService
    {
        private readonly DataContext _context;
        public ClaimRegService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<ClaimProcessRegistration>> GetallClaims()
        {
            return await _context.claimProcessRegistrations.ToListAsync();
        }
        public async Task<bool> ClaimRegister(ClaimProcessRegistrationDTO p)
        {
            var res = await _context.claimProcessRegistrations.Where(m => m.Email == p.Email).FirstOrDefaultAsync();
            if (res == null)
            {
                byte[] passHash, passSalt;
                CreatePasswordHash(p.Password, out passHash, out passSalt);
                ClaimProcessRegistration po = new ClaimProcessRegistration()
                {
                    FullName=p.FullName,
                    Email = p.Email,
                    PasswordHash = passHash,
                    PasswordSalt = passSalt
                    
                };
                Users user1 = new()
                {
                    Email = p.Email,
                    PasswordHash = passHash,
                    PasswordSalt = passSalt,
                    Role = "ClaimsProcessor"
                };
                _context.claimProcessRegistrations.Add(po);
                _context.users.Add(user1);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

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
