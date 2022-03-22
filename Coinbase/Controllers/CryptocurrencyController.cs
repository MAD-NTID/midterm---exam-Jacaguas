using System.Threading.Tasks;
using Coinbase.Models;
using Coinbase.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace Coinbase.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/cryptocurrencies/")]
    public class CryptocurrencyController : ControllerBase
    {
        private readonly ICryptocurrencyRepository _repository;

        public CryptocurrencyController(ICryptocurrencyRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> AllCryptocurrencies()
        {
            return Ok(await _repository.All());
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewCrypto(Cryptocurrency crypto)
        {
            return Ok(await _repository.Create(crypto));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateExistingCurrency(Cryptocurrency crypto)
        {
            return Ok(await _repository.Update(crypto));
        }

        [HttpGet("{rank}")]
        public async Task<IActionResult> GetById(int rank)
        {
            return Ok(await _repository.Get(rank));
        }

        [HttpDelete("{rank}")]
        public async Task<IActionResult> Delete(int rank)
        {
            _repository.Delete(rank);
            return Ok("Cryptocurrency was successfully deleted");
        }

        [HttpGet("/marketcap")]
        public async Task<IActionResult> GetListOfMarketCaps()
        {
            return Ok(await _repository.MarketCap());
        }

        [HttpGet("/search/name/{name}")]
        public async Task<IActionResult> SearchByName(string name)
        {
            return Ok(await _repository.Search(name));
        }

        [HttpGet("pricerange/{start}/{end}")]
        public async Task<IActionResult> GetAllInPriceRange(double min, double max)
        {
            return Ok(await _repository.PriceRange(min,max));

        }

    }
}