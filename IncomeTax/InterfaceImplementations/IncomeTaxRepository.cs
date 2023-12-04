using IncomeTax.DbContexts;
using IncomeTax.Interfaces;
using IncomeTax.Models;
using System;

namespace IncomeTax.InterfaceImplementations
{
    public class IncomeTaxRepository : IIncomeTaxRepository
    {
        private readonly IncomeTaxDbContext _dbContext;

        public IncomeTaxRepository(IncomeTaxDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SaveTaxCalculationRequest(IncomeTaxRequest request)
        {
            _dbContext.IncomeTaxRequest.Add(request);
            _dbContext.SaveChanges();
        }
    }
}
