using System.Security.Cryptography.X509Certificates;
using Utilities;
using WH.ADMIN.DBManager;
using WH.ADMIN.Helper;
using WH.ADMIN.Models;
using WH.ADMIN.Models.Entities;
using WH.PORTAL.Services;

namespace WH.ADMIN.Services
{
    public class UserService
    {

        public bool IsUserExist(string username) { 
            var user = GetUserDetails(username);
            return user != null;
        }

        public User GetUserDetails(string username) {
            using var manager = new UserManager();
            return manager.SelectUser("username", username);
        }

        public OperationResult AddUser(User user, long addedBy) {
            
            if (IsUserExist(user.Username)) {
                return OperationResult.Failed("Username already exist.");
            }

            var password = PasswordHelper.GeneratePassword();

            user.Password = CryptoHelper.Encrypt(password);

            var logDescription = new UsersSercurityLogs() {
                UserId = addedBy,
                Description = $"Added new user: {user.Username}",
                Status = Status.SUCCESS
            };

            using UserManager userManager = new UserManager();
            userManager.BeginTransaction();
            userManager.InsertUser(user);
            userManager.InsertSecurityLogs(logDescription);
            userManager.Commit();

            CommonService commonService = new CommonService();
            var emailTemplate = commonService.GetEmailTemplate("USER ENROLLMENT");
            var subject = emailTemplate.Subject;
            var body = emailTemplate.Body.Replace("<first_name>", user.FirstName)
                                         .Replace("<last_name>", user.LastName)
                                         .Replace("<username>", user.Username)
                                         .Replace("<password>", password);

            EmailHelper emailHelper = new EmailHelper();
            emailHelper.SendEmail(user.Email, subject, body);

            return OperationResult.Success("User added successfully.");
        }
    }
}
