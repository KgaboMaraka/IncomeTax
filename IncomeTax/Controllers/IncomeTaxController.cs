using IncomeTax.Interfaces;
using IncomeTax.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace IncomeTax.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeTaxController : ControllerBase
    {
        private readonly IIncomeTaxService _incomeTaxService;
        private readonly IIncomeTaxRepository _incomeTaxRepository;

        public IncomeTaxController(IIncomeTaxService incomeTaxService, IIncomeTaxRepository incomeTaxRepository)
        {
            _incomeTaxService = incomeTaxService;
            _incomeTaxRepository = incomeTaxRepository;
        }

        /// <response code="200">The request was successfull.</response>
        /// <response code="400">Invalid input</response>
        /// <response code="500">The API has an exception</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        [HttpPost("Calculate")]
        public ObjectResult CalculateTax([Required] string postalCode, [Required] decimal income)
        {
            if(income == 0)
                return StatusCode((int)HttpStatusCode.BadRequest, "Invalid income.");

            IncomeTaxRequest incomeTaxRequest = new IncomeTaxRequest(postalCode, income);

            if(incomeTaxRequest.TaxCalculationType == TaxCalculationType.Invalid) 
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "Invalid postal code.");
            }

            var taxAmount = _incomeTaxService.CalculateTax(incomeTaxRequest);
            _incomeTaxRepository.SaveTaxCalculationRequest(incomeTaxRequest);

            return StatusCode((int)HttpStatusCode.OK, taxAmount);
        }
    }
}
