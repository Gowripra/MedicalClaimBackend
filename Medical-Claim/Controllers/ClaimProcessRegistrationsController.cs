using BusinessLogicLayer.Services;
using DataAccesLayer.Models;
using Medical_Claim.Logger;
using Medical_Claim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medical_Claim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="ClaimsProcessor")]
    [EnableCors("AllowOrigin")]
    public class ClaimProcessRegistrationsController : ControllerBase
    {
        private readonly IClaimRegService _repo;
        public ClaimProcessRegistrationsController(IClaimRegService repo)
        {
            _repo = repo;
        }
        /// <summary>
        /// Get all claimprocessor details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<ClaimProcessRegistration>>> GetallClaims()
        {
            Log.logWrite("GetallClaim method started..");
            try
            {
                Log.logWrite("Here you can get all the claimprocessors details");
                Log.logWrite("GetallClaim method ended..");
                return Ok(await _repo.GetallClaims());
            }
            catch(Exception ex)
            {
                Log.logWrite(ex.ToString() + " " + "LoginUser" + " " + DateTime.Now.ToString());
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Register/Add a claimprocessor details
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("claimregister")]
        public async Task<ActionResult> Add(ClaimProcessRegistrationDTO p)
        {
            Log.logWrite("Add method started..");
            try
            {
                Log.logWrite("You can Add/register the claimprocessors details");
                if (await _repo.ClaimRegister(p))
                {
                    Log.logWrite("Add method ended..");
                    return Ok("user added");
                }
                else
                {
                    return BadRequest("user already existed");
                }
            }
            catch(Exception ex)
            {
                Log.logWrite(ex.ToString() + " " + "LoginUser" + " " + DateTime.Now.ToString());
                return BadRequest(ex.Message);
            }

        }
    }
}
