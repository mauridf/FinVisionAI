using FinVisionAI.Core.Models;

namespace FinVisionAI.Core.Services
{
    public class ProjectionService
    {
        public async Task<List<InvestmentProjection>> GetProjectionsAsync(UserInput input)
        {
            var aggregator = new InvestmentAggregatorService();
            var results = await aggregator.GetAllProjectionsAsync(input);
            return results;
        }
    }

}
