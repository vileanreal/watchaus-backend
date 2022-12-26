using Utilities;
using WH.ADMIN.DBManager;
using WH.ADMIN.Models;
using WH.ADMIN.Models.Entities;
using WH.PORTAL.Services;

namespace WH.ADMIN.Services
{
    public class BranchService
    {

        public List<Branch> GetBranchList()
        {
            using var manager = new BranchManager();
            return manager.SelectBranchList();
        }

        public Branch GetBranchDetailsById(long branchId)
        {
            using var manager = new BranchManager();
            return manager.SelectBranch("branch_id", branchId.ToString());
        }

        public Branch GetBranchDetailsByName(string name)
        {
            using var manager = new BranchManager();
            return manager.SelectBranch("name", name);
        }

        public bool IsBranchExist(long branchId)
        {
            var result = GetBranchDetailsById(branchId);
            return result != null;
        }

        public bool IsBranchExist(string name)
        {
            var result = GetBranchDetailsByName(name);
            return result != null;
        }


        public OperationResult AddBranch(Branch branch, Session session)
        {

            var userService = new UserService();
            var loggonedUser = userService.GetUserDetails(session.Username);

            if (loggonedUser.RoleId != Roles.SUPERADMIN &&
                loggonedUser.RoleId != Roles.ADMIN)
            {
                return OperationResult.Failed("Only admin can add new branch.");
            }

            if (IsBranchExist(branch.Name))
            {
                return OperationResult.Failed("Branch already exist.");
            }

            using var manager = new BranchManager();
            manager.InsertBranch(branch);

            var commonService = new CommonService();
            commonService.InsertAuditTrailLogs(new AuditTrails() { 
                UserId = loggonedUser.UserId,
                Description = $"Added new branch: {branch.Name}"
            });

            return OperationResult.Success("Branch added successfully.");
        }



        public OperationResult UpdateBranch(Branch branch, Session session)
        {

            var userService = new UserService();
            var loggonedUser = userService.GetUserDetails(session.Username);

            if (loggonedUser.RoleId != Roles.SUPERADMIN &&
                loggonedUser.RoleId != Roles.ADMIN)
            {
                return OperationResult.Failed("Only admin can update branch details.");
            }

            if (!IsBranchExist(branch.BranchId))
            {
                return OperationResult.Failed("Branch doesn't exist.");
            }

            using var manager = new BranchManager();
            manager.UpdateBranch(branch);

            var commonService = new CommonService();
            commonService.InsertAuditTrailLogs(new AuditTrails() 
            {
                UserId = loggonedUser.UserId,
                Description = $"Updated branch id: {branch.BranchId}, new branch name {branch.Name}"
            });

            return OperationResult.Success("Branch updated successfully.");
        }


        public OperationResult DeleteBranch(Branch branch, Session session)
        {

            var userService = new UserService();
            var loggonedUser = userService.GetUserDetails(session.Username);

            if (loggonedUser.RoleId != Roles.SUPERADMIN &&
                loggonedUser.RoleId != Roles.ADMIN)
            {
                return OperationResult.Failed("Only admin can delete branch.");
            }

            if (!IsBranchExist(branch.BranchId))
            {
                return OperationResult.Failed("Branch doesn't exist.");
            }

            using var manager = new BranchManager();
            manager.UpdateBranchStatus(branch.BranchId, Status.DELETED);

            var commonService = new CommonService();
            commonService.InsertAuditTrailLogs(new AuditTrails()
            {
                UserId = loggonedUser.UserId,
                Description = $"Deleted branch id: {branch.BranchId}, branch name: {branch.Name}"
            });

            return OperationResult.Success("Branch deleted successfully.");
        }
    }
}
