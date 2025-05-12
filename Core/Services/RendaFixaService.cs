using FinVisionAI.Core.Models;

namespace FinVisionAI.Core.Services
{
    public class RendaFixaService : IInvestmentAnalysisService
    {
        public Task<List<InvestmentProjection>> AnalyzeAsync(UserInput input)
        {
            var projections = new List<InvestmentProjection>();

            // Supondo um CDB com 110% do CDI (CDI ~10,65% ao ano)
            decimal cdi = 0.1065m;
            decimal effectiveAnnual = cdi * 1.10m;
            decimal monthlyRate = (decimal)Math.Pow((double)(1 + effectiveAnnual), 1.0 / input.PeriodInMonths) - 1;

            projections.Add(new InvestmentProjection
            {
                InvestmentType = "CDB 110% CDI",
                Provider = "Simulação",
                ExpectedReturn12Months = input.Amount * (decimal)Math.Pow((double)(1 + monthlyRate), input.PeriodInMonths),
                RiskLevel = 3,
                Pros = "Proteção do FGC, rendimento previsível",
                Cons = "Resgate limitado, rentabilidade depende do tempo",
                MonthlyProjection = SimulateProjection(input.Amount, monthlyRate, input.PeriodInMonths)
            });

            return Task.FromResult(projections);
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
