using FinVisionAI.Core.Models;

namespace FinVisionAI.Core.Services
{
    public class AlphaVantageService : IInvestmentAnalysisService
    {
        private const string ApiKey = "SUA_CHAVE_API";
        private readonly HttpClient _httpClient = new();

        public async Task<List<InvestmentProjection>> AnalyzeAsync(UserInput input)
        {
            var projections = new List<InvestmentProjection>();

            // Exemplo: AAPL
            var symbol = "AAPL";
            var url = $"https://www.alphavantage.co/query?function=TIME_SERIES_MONTHLY&symbol={symbol}&apikey={ApiKey}";
            var response = _httpClient.GetStringAsync(url);

            // Aqui você faria parsing da resposta e criaria o retorno simulado:
            projections.Add(new InvestmentProjection
            {
                InvestmentType = "Ações (Apple - AAPL)",
                Provider = "AlphaVantage",
                ExpectedReturn12Months = input.Amount * 1.15m, // Simulado
                RiskLevel = 8,
                Pros = "Alta liquidez, potencial de valorização",
                Cons = "Alta volatilidade",
                MonthlyProjection = SimulateProjection(input.Amount, 0.0125m, input.PeriodInMonths)
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
