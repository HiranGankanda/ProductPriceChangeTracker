using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PPCT.DataAccessLayer.DataModels.ProjectTableModels;
using PPCT.RepositoryServices.RetailStoreRepoServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PPCT.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class RetailStoresController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IRetailStoreServices _repo;
        public RetailStoresController(IRetailStoreServices repo, ILogger<RetailStoresController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // GET: api/RetailStores/GetAllRetailStores
        [HttpGet]
        [Route("GetAllRetailStores")]
        public async Task<ActionResult<IEnumerable<RetailStore>>> GetAllRetailStores()
        {
            _logger.LogInformation($"[API]GetAllRetailStores() hit at {DateTime.UtcNow.ToLongTimeString()}");
            return await _repo.GetRetailStoresListServiceAsync();
        }

        // GET: api/RetailStores/FindRetailStores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RetailStore>> FindRetailStores(int id)
        {
            _logger.LogInformation($"[API]FindRetailStores(int id) hit at {DateTime.UtcNow.ToLongTimeString()}");
            var retailStore = await _repo.FindRetailStoreDetailsServiceAsync(id);
            if (retailStore == null)
            {
                return NotFound();
            }
            return retailStore;
        }

        // PUT: api/RetailStores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRetailStore(int id, RetailStore retailStore)
        {
            if (id != retailStore.RetailStoreId)
            {
                _logger.LogInformation($"[API]UpdateRetailStore()[MISMATCH] hit at {DateTime.UtcNow.ToLongTimeString()}");
                return BadRequest();
            }
            bool Status = await _repo.UpdateRetailStoreServiceAsync(retailStore);

            if (Status == true)
                return Ok();
            else
                return BadRequest();
        }

        // POST: api/RetailStores
        [HttpPost]
        [Route("CreateRetailStore")]
        public async Task<ActionResult<RetailStore>> CreateRetailStore(RetailStore retailStore)
        {
            _logger.LogInformation($"[API]CreateRetailStore() hit at {DateTime.UtcNow.ToLongTimeString()}");
            bool Status = await _repo.CreateNewRetailStoreServiceAsync(retailStore);
            if (Status == true)
                return CreatedAtAction("GetAllRetailStores", new { id = retailStore.RetailStoreId }, retailStore);
            else
                return BadRequest();
        }

        // DELETE: api/RetailStores/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteretailStore(int id)
        {
            _logger.LogInformation($"[API]DeleteretailStore(int id) hit at {DateTime.UtcNow.ToLongTimeString()}");
            bool Status = await _repo.DeleteRetailStoreServiceAsync(id);
            if (Status == true)
                return NoContent();
            else
                return NotFound();
        }
    }
}