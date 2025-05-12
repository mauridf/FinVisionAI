using System.Globalization;
using System.Text.RegularExpressions;

namespace FinVisionAI.UI.Behaviors
{
    public class MoneyMaskBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            var entry = (Entry)sender;

            // Remove tudo que não é dígito
            string digitsOnly = Regex.Replace(args.NewTextValue, @"[^\d]", "");

            if (string.IsNullOrEmpty(digitsOnly))
            {
                entry.Text = "";
                return;
            }

            // Converte para decimal e formata como moeda
            if (decimal.TryParse(digitsOnly, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal value))
            {
                // Divide por 100 para considerar os centavos (se quiser digitar centavos)
                value /= 100;

                // Formata como moeda brasileira
                entry.Text = value.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"));

                // Posiciona o cursor no final
                entry.CursorPosition = entry.Text.Length;
            }
        }
    }
}
