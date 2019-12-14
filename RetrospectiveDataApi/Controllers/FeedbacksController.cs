using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RetrospectiveDataApi.Entities;
using RetrospectiveDataApi.Exceptions;
using RetrospectiveDataApi.Repositories.Interfaces;

namespace RetrospectiveDataApi.Controllers
{
    [Route("api/Retrospectives/{retrospectiveName}/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IRetrospectiveDataRepository _retrospectiveDataRepository;
        private readonly IConfiguration _configuration;
        private readonly string _filePath;

        public FeedbacksController(IRetrospectiveDataRepository retrospectiveDataRepository, IConfiguration configuration)
        {
            _retrospectiveDataRepository = retrospectiveDataRepository ?? throw new ArgumentNullException("retrospectiveDataRepository");
            _configuration = configuration ?? throw new ArgumentNullException("configuration");

            _filePath = _configuration["FilePath"];

            if (string.IsNullOrEmpty(_filePath))
                throw new Exception("Please provide file location to save data in appsettings.json");

        }

        // POST: api/Retrospectives/{retrospectiveName}/Feedbacks
        [HttpPost]
        public async Task<IActionResult> Post(string retrospectiveName, [FromBody] Feedback feedback)
        {
            if (string.IsNullOrEmpty(retrospectiveName))
                return BadRequest("Invalid value in retrospective name");

            if (!ModelState.IsValid)
                return BadRequest("Invalid feedback data");

            try
            {
                var result = await _retrospectiveDataRepository.Add(_filePath, retrospectiveName, feedback);

                if (result != null)
                    return CreatedAtAction($"Get", "Retrospectives", new { name = retrospectiveName }, result);
                else
                    return BadRequest("Failed to add feedback data");
            }
            catch (RetrospectiveDataException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

    }
}
