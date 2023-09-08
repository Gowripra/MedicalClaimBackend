using Medical_Claim.DataAccess;
using Medical_Claim.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccesLayer.Models;
using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Medical_Claim.Logger;
using Microsoft.AspNetCore.Cors;

namespace Medical_Claim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("AllowOrigin")]
    public class PolicyHoldersController : ControllerBase
    {
        
        private readonly IPolicyHolderService _repo;
        public PolicyHoldersController(IPolicyHolderService repo)
        {
            _repo = repo;
        }
        /// <summary>
        /// Get all policyholder details 
        /// </summary>
        /// <returns></returns>
        
        [HttpGet]
        public async Task<ActionResult<List<PolicyHolder>>> Getall()
        {
            Log.logWrite("Getall method started..");
            try
            {
                Log.logWrite("Here are the policyholder details");
                Log.logWrite("Getall method ended..");
                return Ok(await _repo.Getall());
            }
            catch(Exception ex)
            {
                Log.logWrite(ex.ToString() + " " + "Getall" + " " + DateTime.Now.ToString());
                return BadRequest(ex.Message + " is happened");
            }
        }
        /// <summary>
        /// Get particular policyholder by their ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("ById")]
        public async Task<ActionResult> GetbyId(int id)
        {
            Log.logWrite("GetbyId method started..");
            try
            {
                Log.logWrite("You can get the plociholder details with their ID's");
                PolicyHolder1DTO claim = await _repo.GetbyId(id);
                if (id == 0)
                {
                    return BadRequest("id must be greaterthan 0");
                }
                else if (claim == null || claim.PolicyId == null)
                {
                    return NotFound("Claim with id = " + id + " is Not Found");
                }
                Log.logWrite("GetbyId method ended..");
                return Ok(claim);
            }
            catch (Exception ex)
            {
                Log.logWrite(ex.ToString() + " " + "GetbyId" + " " + DateTime.Now.ToString());
                return BadRequest("Exception = " + ex.Message);
            }
        }
        /// <summary>
        /// Register/Add a policyholder details
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Add(PolicyHolderDTO p)
        {
            Log.logWrite("Add method started..");
            try
            {
                Log.logWrite("Here you can add/register the policyholder details ");
                if (await _repo.Register(p))
                {
                    Log.logWrite("Add method ended..");
                    return Ok("user added");
                }
                else
                {
                    return BadRequest("user already existed");
                }
            }
            catch (Exception ex)
            {
                Log.logWrite(ex.ToString() + " " + "Add" + " " + DateTime.Now.ToString());
                return BadRequest(ex.Message + " is happened");
            }

        }
        //[HttpPost("login")]
        //public async Task<ActionResult> Login(LoginDTO login)
        //{
        //    if(await _repo.Login(login))
        //    {
        //        return Ok("Login success");
        //    }
        //    else { return BadRequest("Login failed"); }
            
        //}

       
    }
}
