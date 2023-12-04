using IncomeTax.Models;

namespace IncomeTax.Interfaces
{
    public interface IIncomeTaxService
    {
        decimal CalculateTax(IncomeTaxRequest request);
    }
}
