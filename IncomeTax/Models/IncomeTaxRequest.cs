using Azure.Core;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IncomeTax.Models
{
    public class IncomeTaxRequest
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string PostalCode { get; set; }

        [NotMapped]
        public TaxCalculationType TaxCalculationType { get; set; }

        public decimal Income { get; set; }

        public decimal TaxAmount { get; set; }

        public DateTime Date { get; set; }

        public IncomeTaxRequest(string postalCode, decimal income)
        {
            PostalCode = postalCode;
            Income = income;

            switch (PostalCode)
            {
                case "7441":
                case "1000":
                    TaxCalculationType = TaxCalculationType.Progressive;
                    break;
                case "A100":
                    TaxCalculationType = TaxCalculationType.FlatValue;
                    break;
                case "7000":
                    TaxCalculationType = TaxCalculationType.FlatRate;
                    break;
                default: TaxCalculationType = TaxCalculationType.Invalid;
                    break;
            }
        }
    }

    public enum TaxCalculationType
    {
        Progressive,
        FlatValue,
        FlatRate,
        Invalid
    }
}
