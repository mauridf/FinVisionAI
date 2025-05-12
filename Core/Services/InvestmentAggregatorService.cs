using FinVisionAI.Core.Models;

namespace FinVisionAI.Core.Services
{
    public class InvestmentAggregatorService
    {
        private readonly List<IInvestmentAnalysisService> _services;

        public InvestmentAggregatorService()
        {
            _services = new List<IInvestmentAnalysisService>
            {
                new AlphaVantageService(),
                new AwesomeApiService(),
                new TesouroDiretoService(),
                new FiiService(),
                new RendaFixaService()
            };
        }

        public async Task<List<InvestmentProjection>> GetAllProjectionsAsync(UserInput input)
        {
            var tasks = _services.Select(s => s.AnalyzeAsync(input));
            var results = await Task.WhenAll(tasks);
            return results.SelectMany(r => r).ToList();
        }
    }
}