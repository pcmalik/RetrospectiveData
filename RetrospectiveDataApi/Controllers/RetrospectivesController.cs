using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RetrospectiveDataApi.Exceptions;
using RetrospectiveDataApi.Models;
using RetrospectiveDataApi.Repositories.Interfaces;

namespace RetrospectiveDataApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RetrospectivesController : ControllerBase
    {
        private readonly IRetrospectiveDataRepository _fileServiceRepository;
        private readonly IConfiguration _configuration;
        private readonly string _filePath;

        public RetrospectivesController(IRetrospectiveDataRepository fileServiceRepository, IConfiguration configuration)
        {
            _fileServiceRepository = fileServiceRepository??throw new ArgumentNullException("fileServiceRepository");
            _configuration = configuration ?? throw new ArgumentNullException("configuration");

            _filePath = _configuration["FilePath"];

            if (string.IsNullOrEmpty(_filePath))
                throw new Exception("Please provide file location to save data in appsettings.json");

        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var retrospectiveDataList = await _fileServiceRepository.Get(_filePath);

                if (retrospectiveDataList != null)
                    return Ok(retrospectiveDataList);
                else
                    return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpGet("{name}")]
        public async Task<ActionResult> Get(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest("Invalid name value");

            try
            {
                var retrospectiveDataList = await _fileServiceRepository.Get(_filePath);
                var retrospectiveData = retrospectiveDataList?.FirstOrDefault(x => x.Name == name);

                if (retrospectiveData != null)
                {
                    return Ok(retrospectiveData);
                }
                else
                    return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RetrospectiveData retrospectiveData)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid retrospective data");

            try
            {
                var result = await _fileServiceRepository.Add(_filePath, retrospectiveData);

                if (result != null)
                    return CreatedAtAction($"Get", new { name = result.Name }, result);
                else
                    return BadRequest("Failed to add retrospective data");
            }
            catch (DuplicateItemException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpGet("Search")]
        public async Task<ActionResult> Search(string date)
        {
            if (string.IsNullOrEmpty(date))
                    return BadRequest("Invalid date value");
            try
            {
                var retrospectiveDataList = await _fileServiceRepository.Get(_filePath);
                var retrospectiveData = retrospectiveDataList?.Where(x => x.Date == date);

                if (retrospectiveData != null)
                {
                    return Ok(retrospectiveData);
                }
                else
                    return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"Internal server error: {ex.Message}" });
            }
        }


    }
}
