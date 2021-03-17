using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DepsWebApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace DepsWebApp.Controllers
{
    /// <summary>
    /// Rates controller that exchanging currency.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class RatesController : ControllerBase
    {
        private readonly ILogger<RatesController> _logger;
        private readonly IRatesService _rates;

        /// <summary>
        /// RatesController constructor.
        /// </summary>
        /// <param name="rates">Set <see cref="_logger"/></param>
        /// <param name="logger">Set <see cref="_rates"/></param>
        public RatesController(
            IRatesService rates,
            ILogger<RatesController> logger)
        {
            _rates = rates;
            _logger = logger;
        }

        /// <summary>
        /// Get amount in exchanged currency.
        /// </summary>
        /// <param name="srcCurrency">Source currency.</param>
        /// <param name="dstCurrency">Destination currency.</param>
        /// <param name="amount">Currency amount.</param>
        /// <returns>Decimal amount of currency.</returns>
        [HttpGet("{srcCurrency}/{dstCurrency}")]
        [ProducesResponseType(typeof(decimal), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<decimal>> Get(string srcCurrency, string dstCurrency, decimal? amount)
        {
            var exchange =  await _rates.ExchangeAsync(srcCurrency, dstCurrency, amount ?? decimal.One);
            if (!exchange.HasValue)
            {
                _logger.LogDebug($"Can't exchange from '{srcCurrency}' to '{dstCurrency}'");
                return BadRequest("Invalid currency code");
            }
            return exchange.Value.DestinationAmount;
        }
    }
}
