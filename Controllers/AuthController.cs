using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.DTO;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegister userForRegister){
   
            // validate request

            userForRegister.Username=userForRegister.Username.ToLower();

            if(await _repo.UserExists(userForRegister.Username)){

                return BadRequest("Username already exists");
            }

            var userToCreate = new User{
                Username = userForRegister.Username
            };

            var createdUser = await _repo.Register(userToCreate, userForRegister.Password);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLogin userForLogin){

            var userFromRep = await _repo.Login(userForLogin.Username, userForLogin.Password);
            return Ok();
        }
    }
}