using Custom_Identity_Auth.Models;
using Microsoft.AspNetCore.Identity;
using Supabase;
using System.Configuration;

namespace Custom_Identity_Auth.Controllers
{
    public class ApplicationUserStore : IUserStore<ApplicationUser>,IUserPasswordStore<ApplicationUser>
    {
        private HttpClient _httpClient;
        private readonly Client _supabase;

        public ApplicationUserStore(HttpClient httpClient,Client Supabase)
        {
            _httpClient = httpClient;
            _supabase = Supabase;

        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            if (user.Email == null )
            {
                throw new ArgumentNullException("No email Entered");
            }
            if (user.PasswordHash == null)
            {
                throw new ArgumentNullException("No password Entered");
            }
            Console.WriteLine($"My email is {user.Email} and idNo is {user.IdNo} and password is {user.PasswordHash}.");

            //var testload = Configuration["SUPABASE_URL"];
            //var response = await _supabase.Auth.SignUp(user.Email, user.PasswordHash);
            return await Task.FromResult(IdentityResult.Success);

        }

        public Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser?> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            // For now, pretend no user exists
            return Task.FromResult<ApplicationUser?>(null);
        }

        public Task<string?> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string?> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id ?? Guid.NewGuid().ToString());
        }

        public Task<string?> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
            //throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string? normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string? passwordHash, CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();
            return Task.FromResult(user.PasswordHash = passwordHash);
        }

        public Task SetUserNameAsync(ApplicationUser user, string? userName, CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();
            Console.WriteLine($"UserName:{userName}");
            if (userName == null)
            {
                throw new ArgumentNullException("No username Entered");
            }
            return Task.FromResult(user.UserName = userName);
        }

        public Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
