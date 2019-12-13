using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RetrospectiveDataApi.Exceptions;
using RetrospectiveDataApi.Models;
using RetrospectiveDataApi.Repositories.Interfaces;

namespace RetrospectiveDataApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RetrospectivesController : ControllerBase
    {
        private readonly ILogger<RetrospectivesController> _logger;
        private readonly IFileServiceRepository _fileServiceRepository;
        private readonly string _filePath;

        public RetrospectivesController(ILogger<RetrospectivesController> logger, IFileServiceRepository fileServiceRepository, IConfiguration configuration)
        {
            _logger = logger;
            _fileServiceRepository = fileServiceRepository;

            _filePath = configuration["FilePath"];

            if (string.IsNullOrEmpty(_filePath))
                throw new Exception("Please provide configuration filepath in appsettings.json");

        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var retrospectiveDataList = await _fileServiceRepository.GetRetrospectiveData(_filePath);

                if (retrospectiveDataList != null)
                    return Ok(retrospectiveDataList);
                else
                    return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get retrospective data");
                return StatusCode(StatusCodes.Status500InternalServerError, new {Message = "Internal server error"});
            }        
        }

        [HttpGet("{name}")]
        public async Task<ActionResult> Get(string name)
        {
            try
            {
                var retrospectiveDataList = await _fileServiceRepository.GetRetrospectiveData(_filePath);
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
                _logger.LogError(ex, "Failed to get retrospective data");
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Internal server error" });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RetrospectiveData retrospectiveData)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            try
            {
                var result = await _fileServiceRepository.AddRetrospectiveData(_filePath, retrospectiveData);

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
            if (date == null)
                return BadRequest("Invalid date");
            try
            {
                var retrospectiveDataList = await _fileServiceRepository.GetRetrospectiveData(_filePath);

                if (retrospectiveDataList != null)
                {
                    //pmalik: todo: filter on date here
                    return Ok(retrospectiveDataList);
                }
                else
                    return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get retrospective data");
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Internal server error" });
            }
        }


    }
}
