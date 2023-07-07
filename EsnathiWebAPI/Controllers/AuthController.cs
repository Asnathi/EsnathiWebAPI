using EsnathiWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EsnathiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly IUserRepository _userRepository;

        public AuthController(JwtTokenService jwtService, IUserRepository userRepository)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public ActionResult Login(LoginRequestModel model)
        {
            var user = _userRepository.GetUserByUsernameAndPassword(model.Username, model.Password);
            if (user == null)
                return Unauthorized("Invalid username or password.");

            var token = _jwtService.GenerateToken(user);
            return Ok(new { token });
        }

        [HttpPost("register")]
        public ActionResult Register(RegisterRequestModel model)
        {
            var existingUser = _userRepository.GetUserByUsername(model.Username);
            if (existingUser != null)
                return BadRequest("Username already exists.");

            var user = new RegisterRequestModel
            {
                Username = model.Username,
                Password = model.Password,
                // Set other properties as needed
            };

            _userRepository.AddUser(user);

            var token = _jwtService.GenerateToken(user);
            return Ok(new { token });
        }

    }
}
