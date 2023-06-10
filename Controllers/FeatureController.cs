using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Linq.Expressions;
using TodoForCoders.Models;

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
        private readonly IConfiguration _configuration;

        // Constructor for dependecy injections
        public FeatureController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Get all features
        [HttpGet]
        public async Task<ActionResult<List<Feature>>> GetAllFeatures()
        {
            // Connecting to Database
            var _db = new NpgsqlConnection(_configuration.GetConnectionString("TodoForCoders"));

            // Perform query on database and store result in features variable
            var features = await _db.QueryAsync<Feature>("select * from Feature");

            // Return the status of the operation 
            return Ok(features);
        }

        // Get a feature by Id
        [HttpGet("{featureId}")]
        public async Task<ActionResult<Feature>> GetFeature(int featureId)
        {
            // Connect to database
            var _db = new NpgsqlConnection(_configuration.GetConnectionString("TodoForCoders"));

            // Perform query on database and store result in feture variable
            var feature = await _db.QueryAsync<Feature>("select * from Feature where featureId = @Id", new { Id = featureId });
            return Ok(feature);
        }

        [HttpPost]
        public async Task<ActionResult<Feature>> CreateFeature(Feature feature)
        {
            // Connect to database
            var _db = new NpgsqlConnection(_configuration.GetConnectionString("TodoForCoders"));

            // Perform query on database and store result in feture variable
            await _db.QueryAsync<Feature>("insert into Feature(Title, Description, IsCompleted) values(@Title, @Description, @IsCompleted)", feature);

            return Ok(feature);
        }

        [HttpPut]
        public async Task<ActionResult<List<Feature>>> UpdateFeature(Feature feature)
        {
            try
            {
                using var _db = new NpgsqlConnection(_configuration.GetConnectionString("TodoForCoders"));

                await _db.QueryAsync<Feature>("update Feature set Title=@Title, Description=@Description, IsCompleted=@IsCompleted where FeatureId = @FeatureId", feature);

                return Ok(feature);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{featureId}")]
        public async Task<ActionResult<Feature>> DeleteFeature(int featureId)
        {
            var _db = new NpgsqlConnection(_configuration.GetConnectionString("TodoForCoders"));

            var feature = await _db.QueryAsync<Feature>("delete from Feature where featureId = @Id", new { Id = featureId });

            return Ok(feature);
        }
    }
}
