using System.Threading.Tasks;
using Coinbase.Models;
using Microsoft.EntityFrameworkCore;

namespace Coinbase.Repositories
{
    public class AuthenticationRepository: IAuthenticationRepository
    {
        private readonly DbSet<Authentication> _auths;

        public AuthenticationRepository(DatabaseContext database)
        {
            _auths = database.ApiKeys;
        }
        public async Task<Authentication> Authenticate(string key)
        {
            Authentication authenticated = await _auths.FirstOrDefaultAsync(auth => auth.ApiKey == key);
            return authenticated;
            throw new System.NotImplementedException();
        }
    }
}