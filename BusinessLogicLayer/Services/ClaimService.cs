using AutoMapper;
using DataAccesLayer.Models;
using Medical_Claim.DataAccess;
using Medical_Claim.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
//using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class ClaimService:IClaimService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ClaimService(DataContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ClaimsDTO>> GetAll()
        {
            var claims = await _context.claims.ToListAsync();
            return _mapper.Map<List<ClaimsDTO>>(claims);
        }
        public async Task<ClaimsDTO> GetClaim(int id)
        {

            if (_context.claims.Count() > 0)
            {
                Claims claim = await _context.claims.AsNoTracking().FirstOrDefaultAsync(x => x.ClaimId == id);
                if (claim != null)
                {
                    return _mapper.Map<ClaimsDTO>(claim);
                }
            }

            return new ClaimsDTO();
        }
        //public async Task<ClaimDetailsDTO> GetClaimDetails(string email)
        //{
        //    PolicyHolder claims=await _context.policyHolders.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
        //    if (claims != null)
        //    {
        //        return < ClaimDetailsDTO > (claims);
        //    }
        //}
        public async Task<ClaimsDTO> NewClaim(ClaimsCreateDTO claimsCreateDTO)
        {
            var claimsOfMonth = await _context.claims.Where(x => x.PolicyNumber == claimsCreateDTO.PolicyNumber
            && x.PolicyHolderName == claimsCreateDTO.PolicyHolderName && x.DateofDischarged.Month == DateTime.Now.Month
            && x.DateofDischarged.Year == DateTime.Now.Year).ToListAsync();
            int numberofclaimsinmonth = claimsOfMonth.Count();
            if (numberofclaimsinmonth <= 3)
            {
                Claims claim = new Claims()
                {
                    PolicyNumber = claimsCreateDTO.PolicyNumber,
                    PolicyHolderName = claimsCreateDTO.PolicyHolderName,
                    ClaimNumber = "",
                    DateofAdmitted = claimsCreateDTO.DateofAdmitted,
                    DateofDischarged = claimsCreateDTO.DateofDischarged,
                    RemainingClaims = 3 - numberofclaimsinmonth - 1,
                    CashType = claimsCreateDTO.CashType,
                    PolicyType = claimsCreateDTO.PolicyType,
                    Status = "Pending",
                    Notes = claimsCreateDTO.Notes
                };
                int claimsnumberincrement = _context.claims.Count() +1;
                claim.ClaimNumber = "CL" + claimsnumberincrement;
                if ((claim.RemainingClaims + 1) > 0)
                {
                    await _context.claims.AddAsync(claim);
                    await _context.SaveChangesAsync();
                    return _mapper.Map<ClaimsDTO>(claim);
                }
                return new ClaimsDTO() { RemainingClaims = -1 };
            }

            return new ClaimsDTO() { RemainingClaims = -1 };

        }
        public async Task<bool> UpdateClaimStatus(int id, string status)
        {

            ClaimsDTO claim = await GetClaim(id);
            if (claim != null && claim.ClaimNumber != null && claim.Status == "Pending")
            {
                claim.Status = status;
                _context.claims.Update(_mapper.Map<Claims>(claim));
                await _context.SaveChangesAsync();
                return true;
            }
            else if (claim.Status == "Rejected" || claim.Status == "rejected")
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("gowriprasadvajragada@gmail.com");
                mailMessage.To.Add("anudeepraju02@gmail.com");
                mailMessage.Subject = "Rejected";
                mailMessage.Body = "your mail got rejecetd";
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("gowriprasadvajragada@gmail.com", "pafq teqx flyp kalz");
                smtpClient.EnableSsl = true;

                smtpClient.Send(mailMessage);
                claim.Status = status;
                _context.claims.Update(_mapper.Map<Claims>(claim));
                await _context.SaveChangesAsync();
                return true;
            }
            else if (claim.Status == "Accepted" || claim.Status == "accepted")
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("gowriprasadvajragada@gmail.com");
                mailMessage.To.Add("gowriprasadvajragada@gmail.com");
                mailMessage.Subject = "Accepted";
                mailMessage.Body = "your mail got accepted";
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("gowriprasadvajragada@gmail.com", "pafq teqx flyp kalz");
                smtpClient.EnableSsl = true;

                smtpClient.Send(mailMessage);
                claim.Status = status;
                _context.claims.Update(_mapper.Map<Claims>(claim));
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
