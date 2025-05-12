namespace FinVisionAI.Core.Models
{
    public class UserInput
    {
        public decimal Amount { get; set; }
        public string RiskProfile { get; set; }
        public int PeriodInMonths { get; set; } = 12; // valor default
    }
}
