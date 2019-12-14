using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RetrospectiveDataApi.Entities;
using RetrospectiveDataApi.Exceptions;
using RetrospectiveDataApi.Models;
using RetrospectiveDataApi.Repositories.Interfaces;

namespace RetrospectiveDataApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RetrospectivesController : ControllerBase
    {
        private const string DATE_VALIDATION_MESSAGE = "Please specify valid date value in dd-mm-yyyy format";
        private readonly IRetrospectiveDataRepository _retrospectiveDataRepository;
        private readonly IConfiguration _configuration;
        private readonly string _filePath;

        public RetrospectivesController(IRetrospectiveDataRepository retrospectiveDataRepository, IConfiguration configuration)
        {
            _retrospectiveDataRepository = retrospectiveDataRepository??throw new ArgumentNullException("retrospectiveDataRepository");
            _configuration = configuration ?? throw new ArgumentNullException("configuration");

            _filePath = _configuration["FilePath"];

            //File configured in appsettings.json serves as the data source for this api
            if (string.IsNullOrEmpty(_filePath))
                throw new Exception("Please make sure json file configured in appsettings.json exists in the specified folder location");

        }

        #region public methods

        //GET: api/Retrospectives
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var retrospectiveDataList = await _retrospectiveDataRepository.Get(_filePath);

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

        //GET: /api/Retrospectives/{name}
        [HttpGet("{name}")]
        public async Task<ActionResult> Get(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest("Invalid name value");

            try
            {
                var retrospectiveDataList = await _retrospectiveDataRepository.Get(_filePath);
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

        //POST: /api/Retrospectives
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RetrospectiveDataModel retrospectiveDataModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid retrospective data");

            try
            {
                RetrospectiveData retrospectiveData = ConvertModelToEntity(retrospectiveDataModel);

                var result = await _retrospectiveDataRepository.Add(_filePath, retrospectiveData);

                if (result != null)
                    return CreatedAtAction($"Get", new { name = result.Name }, result);
                else
                    return BadRequest("Failed to add retrospective data");
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

        //GET: /api/Retrospectives/Filter
        [HttpGet("Filter")]
        public async Task<ActionResult> Filter(string date)
        {
            try
            {
                DateTime retroDate;
                if (!DateTime.TryParse(date, out retroDate))
                    throw new RetrospectiveDataException(DATE_VALIDATION_MESSAGE);

                var retrospectiveDataList = await _retrospectiveDataRepository.Get(_filePath);
                var result = retrospectiveDataList?.Where(x => x.Date == retroDate);

                if (result != null && result.Count() > 0)
                {
                    return Ok(result);
                }
                else
                    return NoContent();

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
        #endregion

        #region private methods
        private static RetrospectiveData ConvertModelToEntity(RetrospectiveDataModel retrospectiveDataModel)
        {
            DateTime retroDate;
            if (!DateTime.TryParse(retrospectiveDataModel.Date, out retroDate))
                throw new RetrospectiveDataException(DATE_VALIDATION_MESSAGE);

            return new RetrospectiveData()
            {
                Name = retrospectiveDataModel.Name,
                Summary = retrospectiveDataModel.Summary,
                Date = retroDate,
                Participants = retrospectiveDataModel.Participants
            };
        }
        #endregion
    }
}
