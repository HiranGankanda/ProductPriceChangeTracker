using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PPCT.DataSupport.DataModels.ProjectTableModels;
using PPCT.RepositoryServices.VATRepoServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PPCT.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class VATPriceController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IVATService _repo;
        public VATPriceController(IVATService repo, ILogger<VATPriceController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // GET: api/VATPrice/GetVATPricesList
        [HttpGet]
        [Route("GetVATPricesList")]
        public async Task<ActionResult<IEnumerable<RetailStoreVATPercentage>>> GetVATPricesList()
        {
            _logger.LogInformation($"[API]GetVATPricesList() hit at {DateTime.UtcNow.ToLongTimeString()}");
            return await _repo.GetVATPricesListServiceAsync();
        }

        // GET: api/VATPrice/FindVATPrice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RetailStoreVATPercentage>> FindVATPrice(int id)
        {
            _logger.LogInformation($"[API]FindVATPrice(int id) hit at {DateTime.UtcNow.ToLongTimeString()}");
            var vatPrice = await _repo.FindVATPriceDetailsServiceAsync(id);
            if (vatPrice == null)
            {
                return NotFound();
            }
            return vatPrice;
        }

        // PUT: api/VATPrice/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVATPrice(int id, RetailStoreVATPercentage vatPrice)
        {
            if (id != vatPrice.VATPercentageId)
            {
                _logger.LogInformation($"[API]UpdateVATPrice()[MISMATCH] hit at {DateTime.UtcNow.ToLongTimeString()}");
                return BadRequest();
            }
            bool Status = await _repo.UpdateVATPriceServiceAsync(vatPrice, User.Identity.Name);

            if (Status == true)
                return Ok();
            else
                return BadRequest();
        }

        // POST: api/VATPrice
        [HttpPost]
        [Route("CreateRetailStore")]
        public async Task<ActionResult<RetailStoreVATPercentage>> CreateNewVATPrice(RetailStoreVATPercentage vatPrice)
        {
            _logger.LogInformation($"[API]CreateNewVATPrice() hit at {DateTime.UtcNow.ToLongTimeString()}");
            bool Status = await _repo.CreateNewVATPriceServiceAsync(vatPrice, User.Identity.Name);
            if (Status == true)
                return CreatedAtAction("GetVATPricesList", new { id = vatPrice.VATPercentageId }, vatPrice);
            else
                return BadRequest();
        }

        // DELETE: api/VATPrice/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteVATPrice(int id)
        {
            _logger.LogInformation($"[API]DeleteVATPrice(int id) hit at {DateTime.UtcNow.ToLongTimeString()}");
            bool Status = await _repo.DeleteVATPriceServiceAsync(id, User.Identity.Name);
            if (Status == true)
                return NoContent();
            else
                return NotFound();
        }
    }
}
