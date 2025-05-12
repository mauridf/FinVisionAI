using FinVisionAI.Core.Models;

namespace FinVisionAI.Core.Services
{
    public class AwesomeApiService : IInvestmentAnalysisService
    {
        private readonly HttpClient _httpClient = new();

        public async Task<List<InvestmentProjection>> AnalyzeAsync(UserInput input)
        {
            var projections = new List<InvestmentProjection>();

            var url = "https://economia.awesomeapi.com.br/json/last/USD-BRL,BTC-BRL";
            var response = _httpClient.GetStringAsync(url);

            // Faça parsing do JSON, simule rendimento etc.
            projections.Add(new InvestmentProjection
            {
                InvestmentType = "Bitcoin",
                Provider = "AwesomeAPI",
                ExpectedReturn12Months = input.Amount * 1.25m,
                RiskLevel = 10,
                Pros = "Potencial de alta valorização",
                Cons = "Extrema volatilidade, risco elevado",
                MonthlyProjection = SimulateProjection(input.Amount, 0.018m, input.PeriodInMonths)
            });

            return projections;
        }

        private List<(DateTime, decimal)> SimulateProjection(decimal amount, decimal monthlyRate, int months)
        {
            var list = new List<(DateTime, decimal)>();
            decimal current = amount;
            for (int i = 1; i <= months; i++)
            {
                current *= (1 + monthlyRate);
                list.Add((DateTime.Today.AddMonths(i), Math.Round(current, 2)));
            }
            return list;
        }
    }

}
