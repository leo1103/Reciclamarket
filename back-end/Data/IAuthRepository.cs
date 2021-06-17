using System.Threading.Tasks;
using basurapp.api.Models;

namespace basurapp.api.Data
{
    public interface IAuthRepository
    {
         Task<BasurappUser> registerUser(BasurappUser user, string password);
         Task<BasurappUser> login(string username, string password);
         Task<bool> userExists(string username);
    }
}