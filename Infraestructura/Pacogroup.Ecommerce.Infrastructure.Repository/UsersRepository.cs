using System.Data;
using System.Reflection.Metadata;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Pacogroup.Ecommerce.Domain.Entity;
using Pacogroup.Ecommerce.Infrastructure.Data;
using Pacogroup.Ecommerce.Infrastructure.Interfaces;

namespace Pacogroup.Ecommerce.Infrastructure.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DapperContext _dapperContext;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UsersRepository(DapperContext dapperContext, IPasswordHasher<User> passwordHasher)
        {
            _dapperContext = dapperContext;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return await Task.FromResult(result == PasswordVerificationResult.Success);
        }

        public async Task<bool> CreateUserAsync(User user, string password)
        {
            using var connection = _dapperContext.CreateConnection();
            var query = "UsersInsert";
            var parameters = new DynamicParameters();
            parameters.Add("Id", Guid.NewGuid().ToString());
            parameters.Add("FirstName", user.FirstName);
            parameters.Add("LastName", user.LastName);
            parameters.Add("Email", user.Email);
            parameters.Add("UserName", user.UserName);
            parameters.Add("PasswordHash", _passwordHasher.HashPassword(user, password));

            var result = await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);

            return result > 0;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            using var connection = _dapperContext.CreateConnection();
            var query = "UsersGetByEmail";
            var parameters = new DynamicParameters();
            parameters.Add("Email", email);

            var user = await connection.QuerySingleOrDefaultAsync<User>(query, param: parameters, commandType: CommandType.StoredProcedure);

            return user;
        }
    }
}