using System;
using System.Threading.Tasks;
using basurapp.api.Models;
using Microsoft.EntityFrameworkCore;

namespace basurapp.api.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext privateContext;

        public AuthRepository(DataContext context)
        {
            privateContext = context;
        }

        public bool verifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hMac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hMac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for(int i=0;i < passwordHash.Length;i++)
                {
                    if(computedHash[i]!=passwordHash[i])
                        return false;
                }
            }    
            return true;   
        }

        public void createPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hMac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hMac.Key;
                passwordHash = hMac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> userExists(string username)
        {
            if(await privateContext.BasurappUser.AnyAsync(x=>x.userName==username))
                return true;
            
            return false;
        }

        public async Task<BasurappUser> registerUser(BasurappUser user, string password)
        {
            byte [] passwordHash, passwordSalt;
            createPasswordHash(password, out passwordHash, out passwordSalt);

            user.passwordHash = passwordHash;
            user.passwordSalt = passwordSalt;

            await privateContext.BasurappUser.AddAsync(user);
            await privateContext.SaveChangesAsync();

            return user;
        }

        public async Task<BasurappUser> login(string username, string password)
        {
            var user = await privateContext.BasurappUser.FirstOrDefaultAsync(x=>x.userName==username);

            if(user==null)
                return null;

            if(!verifyPasswordHash(password, user.passwordHash, user.passwordSalt))
                return null;
            
            return user;
        }
    }
}