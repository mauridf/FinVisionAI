using System.Globalization;

namespace FinVisionAI
{
    public class CurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal decimalValue)
            {
                return decimalValue.ToString("C", culture); // Usar o formato de moeda correto
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                stringValue = stringValue.Replace("R$", "").Trim();
                if (decimal.TryParse(stringValue, out decimal result))
                {
                    return result;
                }
            }
            return 0;
        }
    }
}
