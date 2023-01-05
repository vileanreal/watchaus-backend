using DBHelper;
using Serilog;
using WH.ADMIN.Models;
using WH.ADMIN.Models.Entities;

namespace WH.ADMIN.DBManager
{
    public class UserManager: BaseManager
    {

        #region SELECT
        public User SelectUser(string col, string val)
        {
            string sql = @$"SELECT * FROM USERS u 
                            LEFT JOIN I_ROLES r ON r.role_id = u.role_id
                            WHERE u.{col} = @val AND status = @status";
            AddParameter("@val", val);
            AddParameter("@status", Status.ACTIVE);
            var user = SelectSingle<User>(sql);
            return user;
        }

        public List<User> SelectActiveUsers()
        {
            string sql = @$"SELECT * FROM USERS u 
                            LEFT JOIN I_ROLES r ON r.role_id = u.role_id
                            WHERE u.status = @status
                            ORDER BY user_id DESC";
            AddParameter("@status", Status.ACTIVE);
            var user = SelectList<User>(sql);
            return user;
        }
        #endregion

        #region INSERT

        public long InsertUser(User user)
        {

            user.BranchId = user.BranchId == 0 ? null : user.BranchId;
            user.RoleId = user.RoleId == 0 ? null : user.RoleId;

            string sql = @"INSERT INTO USERS (username, password, first_name, middle_name, last_name, email, phone_no, branch_id, role_id, status)
                                       VALUES(@username, @password, @first_name, @middle_name, @last_name, @email, @phone_no, @branch_id, @role_id, @status)";
            AddParameter("@username", user.Username);
            AddParameter("@password", user.Password);
            AddParameter("@first_name",user.FirstName);
            AddParameter("@middle_name", user.MiddleName);
            AddParameter("@last_name", user.LastName);
            AddParameter("@email", user.Email);
            AddParameter("@phone_no", user.PhoneNo);
            AddParameter("@branch_id", user.BranchId);
            AddParameter("@role_id", user.RoleId);
            AddParameter("@status", Status.ACTIVE);
            ExecuteQuery(sql);
            return LastInsertedId;
        }


        public long InsertSecurityLogs(UsersSercurityLogs log) {
            string sql = @"INSERT INTO USERS_SECURITY_LOGS (user_id, log_date, description, status) 
                                                     VALUES(@user_id, NOW(), @description, @status)";
            AddParameter("@user_id", log.UserId);
            AddParameter("@description", log.Description);
            AddParameter("@status", log.Status);
            ExecuteQuery(sql);
            return LastInsertedId;
        }



        #endregion

        #region UPDATE
        public void UpdateUser(User user)
        {
            user.BranchId = user.BranchId == 0 ? null : user.BranchId;
            user.RoleId = user.RoleId == 0 ? null : user.RoleId;

            string sql = @"UPDATE USERS
                           SET
                           first_name = @first_name,
                           middle_name = @middle_name,
                           last_name = @last_name,
                           email = @email,
                           phone_no = @phone_no,
                           branch_id = @branch_id,
                           role_id = @role_id
                           WHERE 
                           username = @username";

            AddParameter("@username", user.Username);
            AddParameter("@first_name", user.FirstName);
            AddParameter("@middle_name", user.MiddleName);
            AddParameter("@last_name", user.LastName);
            AddParameter("@email", user.Email);
            AddParameter("@phone_no", user.PhoneNo);
            AddParameter("@branch_id", user.BranchId);
            AddParameter("@role_id", user.RoleId);
            ExecuteQuery(sql);
            return;
        }


        public void UpdateUserStatus(string username, string status)
        {
            string sql = @"UPDATE USERS SET status = @status
                           WHERE username = @username AND status = 'A'";

            AddParameter("@username", username);
            AddParameter("@status", status);
            ExecuteQuery(sql);
            return;
        }


        #endregion

        #region DELETE
        #endregion
    }
}
