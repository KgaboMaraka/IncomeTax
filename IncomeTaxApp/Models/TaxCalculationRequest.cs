namespace IncomeTaxApp.Models
{
    public class TaxCalculationRequest
    {
        public string PostalCode { get; set; }
        public decimal Income { get; set; }
        public decimal TaxAmount { get; set; }
    }
}
