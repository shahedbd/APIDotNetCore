using System.Security.Claims;
using System.Threading.Tasks;
using API.DotNetCore.JWTAuths;
using Application.Common.ViewModel;
using Application.Core;
using Application.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace API.DotNetCore.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IBaseRepository _repository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IAuth _auth;

        public AuthController(UserManager<AppUser> userManager, IMapper mapper, IBaseRepository repository, IAuth auth)
        {
            _repository = repository;
            _userManager = userManager;
            _mapper = mapper;
            _auth = auth;
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]CredentialsViewModel credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);
            if (identity == null)
            {
                return BadRequest("Invalid username or password.");
            }

            int ExpireIn = 500;
            var tokenString = _auth.GenerateJSONWebToken(ExpireIn);
            var jwt = Ok(new { Token = tokenString, ExpireTime = ExpireIn * 60, RefreshToken = _auth.GenerateRefreshToken() });
            return new OkObjectResult(jwt);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                var result = _auth.GenerateClaimsIdentity(userName, userToVerify.Id);
                return await Task.FromResult(result);
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
