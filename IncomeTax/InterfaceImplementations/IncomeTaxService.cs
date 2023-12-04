using IncomeTax.Interfaces;
using IncomeTax.Models;

namespace IncomeTax.InterfaceImplementations
{
    public class IncomeTaxService : IIncomeTaxService
    {
        public decimal CalculateTax(IncomeTaxRequest request)
        {
            try
            {
                switch (request.TaxCalculationType)
                {
                    case TaxCalculationType.FlatRate:
                        request.TaxAmount = Math.Round((request.Income / 100) * 17.5m, 2);
                        break;
                    case TaxCalculationType.FlatValue:
                        if(request.Income < 200000)
                            request.TaxAmount = Math.Round((request.Income / 100) * 5m, 2);
                        else
                            request.TaxAmount = 10000;
                        break;
                    case TaxCalculationType.Progressive:
                        request.TaxAmount = CalculateProgressiveTax(request.Income);
                        break;
                }

                request.Date = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("South Africa Standard Time"));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return request.TaxAmount;
        }

        private decimal CalculateProgressiveTax(decimal income)
        {
            try
            {
                decimal tax = 0;
                decimal untaxtedAmount = 0;
                decimal previousBracketMax = 0;
                decimal currentTaxableValue = 0;

                Dictionary<int, decimal> brackets = new Dictionary<int, decimal>()
                {
                    {10, 8350 },
                    {15, 33950 },
                    {25, 82250 },
                    {28, 171550 },
                    {33, 372950 },
                    {35, decimal.MaxValue }
                };

                var lowerBracket = brackets.OrderBy(kvp => kvp.Key).First();

                tax += Math.Round(lowerBracket.Value * (lowerBracket.Key / 100m));
                untaxtedAmount = income - lowerBracket.Value;

                if (income <= lowerBracket.Value)
                {
                    return tax;
                }

                foreach (KeyValuePair<int, decimal> kvp in brackets)
                {
                    currentTaxableValue = kvp.Value - previousBracketMax;

                    if (kvp.Key != lowerBracket.Key)
                    {
                        if (income > kvp.Value)
                        {
                            tax += currentTaxableValue * (kvp.Key / 100m);
                            untaxtedAmount = untaxtedAmount - currentTaxableValue;
                        }
                        else
                        {
                            tax += untaxtedAmount * (kvp.Key / 100m);
                            break;
                        }
                    }

                    previousBracketMax = kvp.Value;
                }

                return tax;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
