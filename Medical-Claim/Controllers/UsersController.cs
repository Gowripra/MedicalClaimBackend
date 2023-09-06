using BusinessLogicLayer.Services;
using DataAccesLayer.Models;
using Medical_Claim.Logger;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medical_Claim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _repo;
        public UsersController(IUserService repo)
        {
            _repo = repo;
        }
        /// <summary>
        /// Login for both roles
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Login(LoginRequestDTO loginRequest)
        {
            try
            {
                Log.logWrite("By using this method you can login into the portal");
                var response = await _repo.Login(loginRequest);
                if (response == null || response.Email == "")
                {
                    Log.logWrite("Invalid credentials");
                    return BadRequest("Invalid Email or Password");
                }
                Log.logWrite("Login successfull...welcome user");
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.logWrite(ex.ToString() + " " + "Login" + " " + DateTime.Now.ToString());
                return BadRequest("Exception = " + ex.Message);
            }
        }
    }
}
