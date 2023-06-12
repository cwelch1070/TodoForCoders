using TodoForCoders.Models;

namespace TodoForCoders.Services
{
    public interface ITodoService
    {
        Task<IEnumerable<Feature>> GetAllFeatures();
        Task<IEnumerable<Feature>> GetFeature(int featureId);
        Task<Feature> CreateFeature(Feature feature);
        Task<Feature> UpdateFeature(Feature feature);
        Task<IEnumerable<Feature>> DeleteFeature(int featureId);
    }
}
