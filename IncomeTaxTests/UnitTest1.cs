using IncomeTax.DbContexts;
using IncomeTax.InterfaceImplementations;
using IncomeTax.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace IncomeTaxTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void IncomeTax_ReturnsCorrectTax()
        {
            // Arrange
            var taxCalculatorService = new IncomeTaxService();
            var progressiveRequest1 = new IncomeTaxRequest( "7441", 70000);
            var progressiveRequest2 = new IncomeTaxRequest("1000", 70000);
            var flatValueRequest = new IncomeTaxRequest("A100", 70000);
            var flatRateRequest = new IncomeTaxRequest("7000", 70000);

            // Act
            var progressiveResult1 = taxCalculatorService.CalculateTax(progressiveRequest1);
            var progressiveResult2 = taxCalculatorService.CalculateTax(progressiveRequest2);
            var flatValueResult = taxCalculatorService.CalculateTax(flatValueRequest);
            var flatRateResult = taxCalculatorService.CalculateTax(flatRateRequest);

            // Assert
            Assert.Multiple(() => {
            Assert.That(progressiveResult1, Is.EqualTo(13687.50));
            Assert.That(progressiveResult2, Is.EqualTo(13687.50));
            Assert.That(flatValueResult, Is.EqualTo(3500.00));
            Assert.That(flatRateResult, Is.EqualTo(12250.00));
            });
        }

        [Test]
        public void SaveIncomeTaxRequest_ToDatabase()
        {
            var options = new DbContextOptionsBuilder<IncomeTaxDbContext>()
                .UseInMemoryDatabase(databaseName: "Tax")
            .Options;

            using (var dbContext = new IncomeTaxDbContext(options))
            {
                var incomeTaxRepository = new IncomeTaxRepository(dbContext);
                var request = new IncomeTaxRequest("7441", 10000);

                incomeTaxRepository.SaveTaxCalculationRequest(request);

                Assert.That(dbContext.IncomeTaxRequest.Count(), Is.EqualTo(1));
            }
        }
    }
}