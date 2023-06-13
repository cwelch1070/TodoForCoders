using Dapper;
using Newtonsoft.Json;
using Npgsql;
using TodoForCoders.Models;

namespace TodoForCoders.Services
{
    // Inheriates the interface
    public class TodoService : ITodoService
    {
        // Defines _configuration as type of IConfiguration to store the connect string to the database
        private readonly IConfiguration _configuration;

        // Constructor allows private variable to be assigned a value
        public TodoService(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        // Gets all features in database
        public async Task<IEnumerable<Feature>> GetAllFeatures()
        {
            // Connecting to Database
            using var _db = new NpgsqlConnection(_configuration.GetConnectionString("TodoForCoders"));

            // Perform query on database and store result in features variable
            var featuresList = await _db.QueryAsync<Feature>("select * from Feature");

            // return the list of features
            return featuresList;
        }

        // Gets a single feature by Id from database
        public async Task<IEnumerable<Feature>> GetFeature(int featureId)
        {
            // Establish connection to database
            using var _db = new NpgsqlConnection(_configuration.GetConnectionString("TodoForCoders"));

            // Perform needed database query
            var feature = await _db.QueryAsync<Feature>("select * from Feature where featureId = @Id", new { Id = featureId });

            // Return the feature retrieved by the query
            return feature;

        }

        // Create a new feature 
        public async Task<Feature> CreateFeature(Feature feature)
        {
            // Connecting to database
            using var _db = new NpgsqlConnection(_configuration.GetConnectionString("TodoForCoders"));

            // Perform database query to add new row to database
            await _db.QueryAsync<Feature>("insert into Feature(Title, Description, IsCompleted) values(@Title, @Description, @IsCompleted)", feature);

            // Return the created Feature
            return feature;
        }

        // Update existing feature
        public async Task<Feature> UpdateFeature(Feature feature)
        {
            // Connect to database
            using var _db = new NpgsqlConnection(_configuration.GetConnectionString("TodoForCoders"));

            // Perform query to update feature by Id
            await _db.QueryAsync<Feature>("update Feature set Title=@Title, Description=@Description, IsCompleted=@IsCompleted where FeatureId = @FeatureId", feature);

            // return the updated feature
            return feature;
        }

        // Delete feature
        public async Task<IEnumerable<Feature>> DeleteFeature(int featureId)
        {
            // Connect to database
            using var _db = new NpgsqlConnection(_configuration.GetConnectionString("TodoForCoders"));

            // Perform query to delete feature by id
            var feature = await _db.QueryAsync<Feature>("delete from Feature where featureId = @Id", new { Id = featureId });

            // return deleted feature
            return feature;
        }
    }
}
