using Application.Common.ViewModel;
using Application.Core;
using Application.Core.Identity;
using Application.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.DotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : Controller
    {
        private readonly IBaseRepository _repository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<AppUser> userManager, IMapper mapper, IBaseRepository repository)
        {
            _repository = repository;
            _userManager = userManager;
            _mapper = mapper;
        }

        // POST api/accounts
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegistrationViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                AppUser appUser = _mapper.Map<AppUser>(model);
                // UserViewModel viewModel = _mapper.Map<UserViewModel>(user);

                var result = await _userManager.CreateAsync(appUser, model.Password);

                if (!result.Succeeded) return new OkObjectResult("Failed");

                await _repository.AddAsync(new Customer { IdentityId = appUser.Id, Location = model.Location });
                await _repository.SaveAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return new OkObjectResult("Account created");
        }
    }
}