using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2C2P_Test.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger<TransactionsController> _logger;
        private readonly TransactionsDBContext _context;

        public TransactionsController(ILogger<TransactionsController> logger, TransactionsDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? Currency, DateTime? From, DateTime? To, string? Status)
        {
            TransactionRepo TRepo = new TransactionRepo(_context);
            return Ok(await TRepo.GetAll(Currency, Status, From, To));
        }
    }
}
