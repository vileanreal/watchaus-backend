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

        public OperationResult AddUser(User user, Session session) {

            var branchService = new BranchService();

            if (user.RoleId == Roles.OPERATOR &&
                (user.BranchId == null || user.BranchId == 0)) {
                return OperationResult.Failed("BranchId is required if role is Operator.");
            }

            if (IsUserExist(user.Username))
            {
                return OperationResult.Failed("Username already exist.");
            }

            if (user.BranchId != null && user.BranchId != 0 && 
                !branchService.IsBranchExist(user.BranchId ?? 0))
            {
                return OperationResult.Failed("Branch doesn't exist.");
            }

            var password = PasswordHelper.GeneratePassword();

            user.Password = CryptoHelper.Encrypt(password);

            var logDescription = new UsersSercurityLogs() {
                UserId = session.Id,
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


        public OperationResult UpdateUser(User user, Session session) {

            var branchService = new BranchService();


            if (user.RoleId == Roles.OPERATOR &&
                user.BranchId == null)
            {
                return OperationResult.Failed("BranchId is required if role is Operator.");
            }

            if (!IsUserExist(user.Username))
            {
                return OperationResult.Failed("Username doesn't exist.");
            }

            if (user.BranchId > 0 &&
                !branchService.IsBranchExist(user.BranchId ?? 0))
            {
                return OperationResult.Failed("Branch doesn't exist.");
            }

            var logDescription = new UsersSercurityLogs()
            {
                UserId = session.Id,
                Description = $"Updated user details: {user.Username}",
                Status = Status.SUCCESS
            };

            using UserManager userManager = new UserManager();
            userManager.BeginTransaction();
            userManager.UpdateUser(user);
            userManager.InsertSecurityLogs(logDescription);
            userManager.Commit();

            return OperationResult.Success("User details is updated successfully.");
        }


        public List<User> GetUserList() {
            using var manager = new UserManager();
            return manager.SelectActiveUsers();
        }

        public OperationResult DeleteUser(string username, Session session) {

            var user = GetUserDetails(username);

            if (user == null)
            {
                return OperationResult.Failed("Username doesn't exist.");
            }

            if (user.RoleId == Roles.SUPERADMIN)
            {
                return OperationResult.Failed("You can't delete user with SUPERADMIN role");
            }

            var logDescription = new UsersSercurityLogs()
            {
                UserId = session.Id,
                Description = $"Deleted user: {username}",
                Status = Status.SUCCESS
            };
            
            using UserManager userManager = new UserManager();
            userManager.BeginTransaction();
            userManager.UpdateUserStatus(username, Status.DELETED);
            userManager.InsertSecurityLogs(logDescription);
            userManager.Commit();

            return OperationResult.Success("User is successfully deleted.");
        }
    }
}
