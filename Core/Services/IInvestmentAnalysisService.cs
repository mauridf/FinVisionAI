using FinVisionAI.Core.Models;

namespace FinVisionAI.Core.Services
{
    public interface IInvestmentAnalysisService
    {
        Task<List<InvestmentProjection>> AnalyzeAsync(UserInput input);
    }

}
