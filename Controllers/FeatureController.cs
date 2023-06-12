using Dapper;
using Microsoft.AspNetCore.Mvc;
using TodoForCoders.Models;
using TodoForCoders.Services;

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
            // Perform query on database and store result in feture variable
            var feature = await _todoService.GetFeature(featureId);

            return Ok(feature);
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
            var result = await _todoService.DeleteFeature(featureId);

            return Ok(result);
        }
    }
}
