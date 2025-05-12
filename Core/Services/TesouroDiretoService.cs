using FinVisionAI.Core.Models;

namespace FinVisionAI.Core.Services
{
    public class TesouroDiretoService : IInvestmentAnalysisService
    {
        public Task<List<InvestmentProjection>> AnalyzeAsync(UserInput input)
        {
            var projections = new List<InvestmentProjection>();

            // Simulação com taxa anual de 10,5% (média atual de títulos prefixados)
            decimal annualRate = 0.105m;
            decimal monthlyRate = (decimal)Math.Pow((double)(1 + annualRate), 1.0 / input.PeriodInMonths) - 1;

            projections.Add(new InvestmentProjection
            {
                InvestmentType = "Tesouro Direto (Prefixado 2027)",
                Provider = "Simulação",
                ExpectedReturn12Months = input.Amount * (decimal)Math.Pow((double)(1 + monthlyRate), input.PeriodInMonths),
                RiskLevel = 2,
                Pros = "Segurança, garantia do Tesouro Nacional",
                Cons = "Liquidez diária pode gerar perdas antes do vencimento",
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
