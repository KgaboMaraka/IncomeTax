using IncomeTax.Models;

namespace IncomeTax.Interfaces
{
    public interface IIncomeTaxRepository
    {
        void SaveTaxCalculationRequest(IncomeTaxRequest request);
    }
}
