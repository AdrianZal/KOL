using Microsoft.AspNetCore.Mvc;
using Tutorial9.Services;

namespace Tutorial9.Controllers;
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveriesController : ControllerBase
    {
        private readonly IDbService _dbService;

        public DeliveriesController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetCustomerRentals(int id)
        {
            var res = await _dbService.GetDeliveries(id);
            if (res == null) return NotFound();
            return Ok(res);
        }
    }
