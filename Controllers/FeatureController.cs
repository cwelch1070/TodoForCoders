using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using TodoForCoders.Models;
using TodoForCoders.Services;
using Newtonsoft.Json.Linq;

namespace TodoForCoders.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeatureController : Controller
    {
        // This line defines _configuration as type IConfiguration.
        // This is the same as writing private readonly string _configuration
        // only slightly more complex because IConfiguration is an interface.
        // Therefore, this does not assign a value to _configuration it makes it a type of IConfiguration.
        private readonly ITodoService _todoService;

        // Constructor for dependecy injections
        public FeatureController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        // Get all features
        [HttpGet]
        public async Task<ActionResult<List<Feature>>> GetAll()
        {
            // Perform query on database and store result in features variable
            var result = await _todoService.GetAllFeatures();

            // Return the status of the operation 
            return Ok(result);
        }

        // Get a feature by Id
        [HttpGet("{featureId}")]
        public async Task<ActionResult<Feature>> GetFeature(int featureId)
        {
            try
            {
                // Call TodoService to perform query
                var result = await _todoService.GetFeature(featureId);

                var jsonResult = JsonConvert.SerializeObject(result, Formatting.Indented);

                if(jsonResult == "[]")
                {
                    throw new Exception("Feature does not exist");
                }

                Console.WriteLine("From Get by Id: " + jsonResult);

                return Ok(jsonResult);
            } catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Feature>> CreateFeature(Feature feature)
        {
            // Perform query on database and store result in feture variable
            var result = await _todoService.CreateFeature(feature);

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<List<Feature>>> UpdateFeature(Feature feature)
        {
            // Add way to verify feature being updated exists in the database
            try
            {
                var result = await _todoService.UpdateFeature(feature);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{featureId}")]
        public async Task<ActionResult<Feature>> DeleteFeature(int featureId)
        {
            // Add way to verify feature exists before deleteing and if doesnt 
            // return an alert that it does not exist
            var result = await _todoService.DeleteFeature(featureId);

            return Ok(result);
        }
    }
}
