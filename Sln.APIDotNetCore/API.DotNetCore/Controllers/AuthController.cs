using API.DotNetCore.JWTAuths;
using Application.Common.ViewModel;
using Application.Core.Identity;
using Application.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Claims;
using System.Threading.Tasks;


namespace API.DotNetCore.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IBaseRepository _repository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAuth _auth;
        private IConfiguration _config;


        public AuthController(UserManager<AppUser> userManager, IMapper mapper, IBaseRepository repository, IAuth auth, IConfiguration config)
        {
            _repository = repository;
            _userManager = userManager;
            _mapper = mapper;
            _auth = auth;
            _config = config;
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]CredentialsViewModel credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = await GetIdentityUser(credentials.UserName, credentials.Password);
            if (identity == false)
            {
                return BadRequest("Invalid username or password.");
            }

            int ExpireIn = Convert.ToInt16(_config["Jwt:expires"]);
            if (identity == true)
            {
                var tokenString = _auth.GenerateJSONWebToken(ExpireIn);
                // HttpContext.Session.SetString("JWToken", tokenString);
                return Ok(new { Token = tokenString, ExpireTime = ExpireIn, RefreshToken = _auth.GenerateRefreshToken() });
            }
            else
            {
                return Unauthorized();
            }

        }

        private async Task<bool> GetIdentityUser(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult(false);

            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(userName);
            if (userToVerify == null) return await Task.FromResult(false);

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult(true);
        }
    }
}
