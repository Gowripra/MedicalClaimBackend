using BusinessLogicLayer.Services;
using DataAccesLayer.Models;
using Medical_Claim.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace Medical_Claim.Controllers
{
    [Authorize]
    //[AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class ClaimsController : ControllerBase
    {
        private readonly IClaimService _repo;
        public ClaimsController(IClaimService repo)
        {
            _repo = repo;
        }
        /// <summary>
        /// Get all Claims
        /// </summary>
        /// <returns></returns>
        [HttpGet("Getallclaims")]
        public async Task<ActionResult<List<Claims>>> Get()
        {
            Log.logWrite("Get method started..");
            try
            {
                Log.logWrite("Here you can get all the claims");
                var result = await _repo.GetAll();
                Log.logWrite("Get method ended..");
                return Ok(result);
                
            }
            catch (Exception ex)
            {
                Log.logWrite(ex.ToString() + " " + "Get" + " " + DateTime.Now.ToString());
                return BadRequest("Exception = " + ex.Message);
            }
            
        }
        /// <summary>
        /// Get claims by their ID's
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("id", Name = "GetClaimById")]
        public async Task<ActionResult> GetClaim(int id)
        {
            Log.logWrite("GetClaim method started..");
            try
            {
                Log.logWrite("You can get the claims with their ID's");
                ClaimsDTO claim = await _repo.GetClaim(id);
                if (id == 0)
                {
                    return BadRequest("id must be greaterthan 0");
                }
                else if (claim == null || claim.ClaimNumber == null)
                {
                    return NotFound("Claim with id = " + id + " is Not Found");
                }
                Log.logWrite("GetClaim method ended..");
                return Ok(claim);
            }
            catch (Exception ex)
            {
                Log.logWrite(ex.ToString() + " " + "GetClaim" + " " + DateTime.Now.ToString());
                return BadRequest("Exception = " + ex.Message);
            }
            
        }
        /// <summary>
        /// Add a claim by only policyholder
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        [Authorize(Roles = "PolicyHolder")]
        [HttpPost("Newclaimregister")]
        public async Task<ActionResult> NewClaim(ClaimsCreateDTO claims)
        {
            Log.logWrite("NewClaim method started..");
            try
            {
                Log.logWrite("You can add the new claim here..");
                var claim1 = await _repo.NewClaim(claims);
                if (claim1 == null || claim1.RemainingClaims == -1)
                {
                    return BadRequest("Maximum claim submission limit exceeded for this month");
                }
                Log.logWrite("NewClaim method ended..");
                return CreatedAtRoute("GetClaimById", new { id = claims.ClaimId }, claim1);
            }
            catch (Exception ex)
            {
                Log.logWrite(ex.ToString() + " " + "NewClaim" + " " + DateTime.Now.ToString());
                return BadRequest("Exception = " + ex.Message);
            }
        }
        /// <summary>
        /// Update the status of the claim by only claimprocessor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [Authorize(Roles = "ClaimsProcessor")]
        [HttpPut("Updateclaim")]
        public async Task<ActionResult> StatusUpdateClaim(int id,  string status)
        {
            Log.logWrite("StatusUpdateClaim method started..");
            try
            {
                //var controller = new PolicyHoldersController();
                //return await controller.GetbyId(id);
                
                Log.logWrite("Here claimprocessor can update the status of the claim");
                var claim = await _repo.GetClaim(id);
                if (await _repo.UpdateClaimStatus(id, status))
                {
                    Log.logWrite("StatusUpdateClaim method ended..");
                    return Ok("Claim Status Updated Successfully and email sent successfully");
                }
                //else if (claim.Status == "Rejected" || claim.Status == "rejected")
                //{
                //    MailMessage mailMessage = new MailMessage();
                //    mailMessage.From = new MailAddress("gowriprasadvajragada@gmail.com");
                //    mailMessage.To.Add("");
                //    mailMessage.Subject = "Rejected";
                //    mailMessage.Body = "your mail got rejecetd";
                //    SmtpClient smtpClient = new SmtpClient();
                //    smtpClient.Host = "smtp.gmail.com";
                //    smtpClient.Port = 587;
                //    smtpClient.UseDefaultCredentials = false;
                //    smtpClient.Credentials = new NetworkCredential("gowriprasadvajragada@gmail.com", "pafq teqx flyp kalz");
                //    smtpClient.EnableSsl = true;

                //    smtpClient.Send(mailMessage);
                //    return Ok("Email sent successfully");


                //}
                //else if (claim.Status == "Accepted" || claim.Status == "accepted")
                //{
                //    MailMessage mailMessage = new MailMessage();
                //    mailMessage.From = new MailAddress("gowriprasadvajragada@gmail.com");
                //    mailMessage.To.Add("");
                //    mailMessage.Subject = "Accepted";
                //    mailMessage.Body = "your mail got accepted";
                //    SmtpClient smtpClient = new SmtpClient();
                //    smtpClient.Host = "smtp.gmail.com";
                //    smtpClient.Port = 587;
                //    smtpClient.UseDefaultCredentials = false;
                //    smtpClient.Credentials = new NetworkCredential("gowriprasadvajragada@gmail.com", "pafq teqx flyp kalz");
                //    smtpClient.EnableSsl = true;

                //    smtpClient.Send(mailMessage);
                //    return Ok("Email sent successfully");


                //}
                else if (claim.Status == "Rejected" || claim.Status == "Approved" || claim.Status=="rejected"||claim.Status=="approved")
                {
                    return BadRequest("ClaimId = " + id + " 's Status is already updated");
                }
                else
                {
                    return NotFound("ClaimId = " + id + "is Not Found");
                }
            }
            catch (Exception ex)
            {
                Log.logWrite(ex.ToString() + " " + "StatusUpdateClaim" + " " + DateTime.Now.ToString());
                return BadRequest("Exception = " + ex.Message);
            }
        }
    }
}
