using Application.Core;
using Application.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DotNetCore.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PersonalInfoesController : ControllerBase
    {
        private readonly IBaseRepository _repository;
        private readonly ILogger _logger;
        public PersonalInfoesController(IBaseRepository repository, ILogger<PersonalInfoesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // GET: api/PersonalInfoes
        [HttpGet, Authorize]
        public IEnumerable<PersonalInfo> GetPersonalInfo()
        {
            var result = _repository.GetAll<PersonalInfo>();
            _logger.LogInformation("Get All PersonalInfo called. Total: " + result.Count());
            return result;
        }

        // GET: api/PersonalInfoes/5
        [HttpGet("{id}"), Authorize]
        public async Task<IActionResult> GetPersonalInfo([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var personalInfo = await _repository.GetAsync<PersonalInfo>(id);

            if (personalInfo == null)
            {
                return NotFound();
            }

            return Ok(personalInfo);
        }

        // PUT: api/PersonalInfoes/5
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutPersonalInfo([FromRoute] long id, [FromBody] PersonalInfo personalInfo)
        {
            return CreatedAtAction("GetPersonalInfo", new { id = personalInfo.ID }, personalInfo);
            //return NoContent();
        }

        // POST: api/PersonalInfoes
        [HttpPost, Authorize]
        public async Task<IActionResult> PostPersonalInfo([FromBody] PersonalInfo personalInfo)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                _repository.Add<PersonalInfo>(personalInfo);
                await _repository.SaveAsync();

                return CreatedAtAction("GetPersonalInfo", new { id = personalInfo.ID }, personalInfo);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // DELETE: api/PersonalInfoes/5
        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> DeletePersonalInfo([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var personalInfo = await _repository.GetAsync<PersonalInfo>(id);
            if (personalInfo == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsyn<PersonalInfo>(personalInfo);
            await _repository.SaveAsync();

            return Ok(personalInfo);
        }

        private bool PersonalInfoExists(long id)
        {
            var result = _repository.Get<PersonalInfo>(id);
            if (result.ID > 0) return true;
            else return false;

        }
    }
}