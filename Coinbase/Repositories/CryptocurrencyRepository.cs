using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coinbase.Models;
using Microsoft.EntityFrameworkCore;

namespace Coinbase.Repositories
{
    public class CryptoCurrencyRepository: ICryptocurrencyRepository
    {
        public readonly DatabaseContext _database;
        public async Task<IEnumerable<Cryptocurrency>> All()
        {
            return await _database.Cryptocurrencies.ToListAsync();
            
            throw new System.NotImplementedException();
        }

        public async Task<Cryptocurrency> Get(int rank)
        {
            Cryptocurrency crypto = await _database.Cryptocurrencies.FirstOrDefaultAsync(crypto => crypto.Rank == rank);
            //throw new System.NotImplementedException();
            return crypto;
        }
        

        public async Task<Cryptocurrency> Create(Cryptocurrency cryptocurrency)
        {
            await _database.Cryptocurrencies.AddAsync(cryptocurrency);
            await _database.SaveChangesAsync();
            return cryptocurrency;
            throw new System.NotImplementedException();
        }

        public async Task<Cryptocurrency> Update(Cryptocurrency cryptocurrency)
        {
            await Get(cryptocurrency.Rank);
            _database.Cryptocurrencies.Update(cryptocurrency);
            await _database.SaveChangesAsync();
            return cryptocurrency;
            throw new System.NotImplementedException();
        }

        public async void Delete(int rank)
        {
            Cryptocurrency crypto = await Get(rank);
            _database.Cryptocurrencies.Remove(crypto);
            await _database.SaveChangesAsync();
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<CoinMarketCap>> MarketCap()
        {
            List<CoinMarketCap> cryptoMarketCaps = new List<CoinMarketCap>();

            foreach (Cryptocurrency crypto in _database.Cryptocurrencies)
            {
                cryptoMarketCaps.Add(new CoinMarketCap {Name = crypto.Name,MarketCap = crypto.MarketCap,AvailableSupply = crypto.AvailableSupply});
                return cryptoMarketCaps;

            }
            
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Cryptocurrency>> Search(string name)
        {
            name = name.ToLower();
            return await _database.Cryptocurrencies
                        .Where(crypto =>
                        crypto.Name.ToLower()
                        .Contains(name))
                        .ToListAsync();
                    
            
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Cryptocurrency>> PriceRange(double min, double max)
        {

            List<Cryptocurrency> cryptocurrencies = new List<Cryptocurrency>();
             //loop through each of the currencies
             foreach (Cryptocurrency currency in _database.Cryptocurrencies)
             {
                 //strip out the $ and , from the price string
                 currency.Price = currency.Price.Replace("$", "").Replace(",", "");
                 //convert the price to double so we can use it for range comparing
                 double price = double.Parse(currency.Price);
                 
                 //if the price is in the range, we add it to the list
                 if (price >= min && price <= max)
                 {
                     cryptocurrencies.Add(currency);
                 }
             }

             return cryptocurrencies;
             throw new NotImplementedException();
             
        }
    }
}