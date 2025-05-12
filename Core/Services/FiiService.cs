using FinVisionAI.Core.Models;

namespace FinVisionAI.Core.Services
{
    public class FiiService : IInvestmentAnalysisService
    {
        public Task<List<InvestmentProjection>> AnalyzeAsync(UserInput input)
        {
            var projections = new List<InvestmentProjection>();

            decimal dividendYieldMonthly = 0.009m; // 0,9% ao mês = ~11,3% ao ano

            projections.Add(new InvestmentProjection
            {
                InvestmentType = "Fundos Imobiliários (FII)",
                Provider = "Simulação",
                ExpectedReturn12Months = input.Amount * (decimal)Math.Pow((double)(1 + dividendYieldMonthly), input.PeriodInMonths),
                RiskLevel = 5,
                Pros = "Rendimentos mensais, isenção de IR para PF, diversificação",
                Cons = "Volatilidade de mercado, riscos de vacância e inadimplência",
                MonthlyProjection = SimulateProjection(input.Amount, dividendYieldMonthly, input.PeriodInMonths)
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
