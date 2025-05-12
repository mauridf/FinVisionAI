using System.Globalization;
using LiveChartsCore.SkiaSharpView;
using Microcharts;

namespace FinVisionAI.Core.Models
{
    public class InvestmentProjection
    {
        public string InvestmentType { get; set; }
        public string Provider { get; set; } // Ex: Alpha Vantage, Tesouro Direto, etc.
        public decimal ExpectedReturn12Months { get; set; }
        public decimal RiskLevel { get; set; } // 0 a 10
        public string Pros { get; set; }
        public string Cons { get; set; }
        public int SelectedPeriod { get; set; }
        public string FormattedReturn =>
        $"Retorno em {SelectedPeriod} meses: {ExpectedReturn12Months.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"))}";
        public IEnumerable<LineSeries<double>> ChartSeries { get; set; }
        public List<(DateTime Month, decimal Value)> MonthlyProjection { get; set; }
        public string IconPath => InvestmentType switch
        {
            "Fundos Imobiliários (FII)" => "fii_icon.png",
            "Tesouro Direto (Prefixado 2027)" => "treasury_icon.png",
            "Ações (Apple - AAPL)" => "stocks_icon.png",
            "CDB 110% CDI" => "cdb_icon.png",
            _ => "investment_icon.png"
        };
    }

}
