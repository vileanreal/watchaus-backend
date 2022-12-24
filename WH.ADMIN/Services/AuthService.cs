using Serilog;
using Utilities;
using WH.ADMIN.Models;
using WH.ADMIN.Models.Entities;

namespace WH.ADMIN.Services
{
    public class AuthService
    {
        public OperationResult<User> Login(string username, string password) {
            UserService userService = new UserService();
            var user = userService.GetUserDetails(username);
            if (user == null) {
                return OperationResult<User>.Failed("Invalid Username / Password.");
            }
            if (user.Password != CryptoHelper.Encrypt(password)) {
                return OperationResult<User>.Failed("Invalid Username / Password.");
            }
            if (user.Status != Status.ACTIVE)
            {
                return OperationResult<User>.Failed("User is not yet activated.");
            }

            return OperationResult<User>.Success(user, "Login successfully.");
        }
    }
}
