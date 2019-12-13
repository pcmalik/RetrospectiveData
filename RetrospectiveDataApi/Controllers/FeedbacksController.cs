using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetrospectiveDataApi.Models;

namespace RetrospectiveDataApi.Controllers
{
    [Route("api/Retrospectives/{retrospectiveName}/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        // POST: api/Retrospectives/{retrospectiveName}/Feedbacks
        [HttpPost]
        public async Task<IActionResult> Post(string retrospectiveName, [FromBody] Feedback feedback)
        {
            throw new NotImplementedException();
        }

    }
}
