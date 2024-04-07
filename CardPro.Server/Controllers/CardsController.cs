using CardPro.Server.Models;
using CardPro.Server.Services;
using CardPro.Server.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;

namespace CardPro.Server.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class CardsController : ControllerBase
    {
        private readonly ICardsService cardsService;
        private readonly IMemoryCache cache;
        private readonly CacheSettings cacheSettings;

        public CardsController(ICardsService cardsService, IMemoryCache cache, IOptions<CacheSettings> cacheSettings)
        {
            this.cardsService = cardsService;
            this.cache = cache;
            this.cacheSettings = cacheSettings.Value;
        }

        [HttpGet]
        [Route("banks")]
        public ActionResult<IEnumerable<Bank>> GetBanks()
        {
            var banks = new List<Bank>();
            var cacheEntryOptions = new MemoryCacheEntryOptions();
            cacheEntryOptions.AbsoluteExpirationRelativeToNow = cacheSettings.AbsoluteExpirationRelativeToNow;
            cacheEntryOptions.SlidingExpiration = cacheSettings.SlidingExpiration;
            if (!cache.TryGetValue("banks", out banks))
            {
                banks = cardsService.GetBanks();
                cache.Set("banks", banks, cacheEntryOptions);
            }
            return Ok(banks);
        }

        [HttpGet]
        [Route("cards")]
        public ActionResult<IEnumerable<Bank>> GetCards([FromQuery] string? isBlocked = null, [FromQuery] string? cardNumber = null, [FromQuery] string? bankCode = null)
        {
            var cards = cardsService.GetCards(isBlocked, cardNumber, bankCode);
            if (cards.Count == 0)
            {
                return NotFound("No cards found");
            }
            return Ok(cards);
        }

        [HttpPut]
        [Route("cards")]
        public ActionResult IncreaseCreditLimit(string cardNumber, string requestedCreditLimit, string employmentType, string averageMonthlyIncome)
        {
            try
            {
                cardsService.IncreaseCreditLimit(cardNumber, requestedCreditLimit, employmentType, averageMonthlyIncome);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok("Request accepted - credit limit has updated.");
        }
    }
}
