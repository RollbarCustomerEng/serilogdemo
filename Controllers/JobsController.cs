using Microsoft.AspNetCore.Mvc;
using System.Net;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RollbarNETSeriLog.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {

        private readonly ILogger<JobsController> _logger;

        public JobsController(ILogger<JobsController> logger)
        {
            _logger = logger;
        }

        // GET: api/<JobsController>
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {

            _logger.LogInformation("Seri Job Started");

            string varReturnValue = "";

            foreach (var i in Enumerable.Range(0, 200))
            {
                //Reset Variable
                varReturnValue = "";

                try
                {
                    //Do some code function that could timeout
                    await GetResult();

                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex, ex.Message);
                }

            }

            _logger.LogInformation("Seri Job Completed");

            return new string[] { "Completed 200" };
        }


        private async Task<string> GetResult() {

            // do something in this code
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://chatai.floodgate.co.za/api/ping");
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.Timeout = 5;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }

        }

    }
}
