using FinVisionAI.Core.Models;
using FinVisionAI.Core.Services;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Globalization;

namespace FinVisionAI.UI.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnAnalyzeClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(amountEntry.Text) ||
            riskPicker.SelectedIndex == -1 ||
            periodPicker.SelectedIndex == -1)
        {
            await DisplayAlert("Erro", "Preencha todos os campos corretamente.", "OK");
            return;
        }

        int[] periodOptions = { 12, 24, 36, 48 };
        int selectedPeriod = periodOptions[periodPicker.SelectedIndex];

        var input = new UserInput
        {
            Amount = ConvertFormattedCurrencyToDecimal(amountEntry.Text),
            RiskProfile = riskPicker.SelectedItem.ToString(),
            PeriodInMonths = selectedPeriod
        };

        var service = new ProjectionService();
        var projections = await service.GetProjectionsAsync(input);

        foreach (var p in projections)
        {
            // CORRE��O: Pegar o �ltimo valor da proje��o para o per�odo selecionado
            if (p.MonthlyProjection != null && p.MonthlyProjection.Count >= selectedPeriod)
            {
                p.ExpectedReturn12Months = p.MonthlyProjection[selectedPeriod - 1].Value;
            }
            else if (p.MonthlyProjection != null && p.MonthlyProjection.Count > 0)
            {
                // Se n�o tiver meses suficientes, pega o �ltimo dispon�vel
                p.ExpectedReturn12Months = p.MonthlyProjection.Last().Value;
            }

            p.SelectedPeriod = selectedPeriod;
            // Criar gr�ficos interativos para cada proje��o
            var series = new List<LineSeries<double>>
        {
            new LineSeries<double>
            {
                Values = p.MonthlyProjection.Select(m => (double)m.Value).ToArray(),
                Fill = null,
                Stroke = new SolidColorPaint(SKColors.Blue),
                GeometrySize = 8,
                GeometryStroke = new SolidColorPaint(SKColors.Blue),
                GeometryFill = new SolidColorPaint(SKColors.White),
                LineSmoothness = 0.2
            }
        };

            p.ChartSeries = series;
        }

        resultsView.ItemsSource = projections;
        resultsView.IsVisible = true;
    }

    private decimal ConvertFormattedCurrencyToDecimal(string formattedValue)
    {
        // Remove o s�mbolo de moeda, pontos e espa�os
        string digitsOnly = formattedValue
            .Replace("R$", "")
            .Replace(".", "")
            .Replace(",", ".")
            .Trim();

        if (decimal.TryParse(digitsOnly, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result))
        {
            return result;
        }

        return 0m; // Ou lance uma exce��o se preferir
    }
}