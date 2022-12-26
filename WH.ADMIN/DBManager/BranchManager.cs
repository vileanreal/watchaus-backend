using DBHelper;
using WH.ADMIN.Models;
using WH.ADMIN.Models.Entities;

namespace WH.ADMIN.DBManager
{
    public class BranchManager: BaseManager
    {
        #region SELECT
        public Branch SelectBranch(string col, string val)
        {
            string sql = @$"SELECT * FROM BRANCHES WHERE {col} = @val AND status = @status";
            AddParameter("@val", val);
            AddParameter("@status", Status.ACTIVE);
            return SelectSingle<Branch>(sql);
        }
        public List<Branch> SelectBranchList()
        {
            string sql = @$"SELECT * FROM BRANCHES WHERE status = @status";
            AddParameter("@status", Status.ACTIVE);
            return SelectList<Branch>(sql);

        }
        #endregion

        #region INSERT
        public void InsertBranch(Branch branch) {
            string sql = @"INSERT INTO BRANCHES (name,status) 
                                         VALUES (@name,@status)";
            AddParameter("@name", branch.Name);
            AddParameter("@status", "A");
            ExecuteQuery(sql);
            return;
        }
        #endregion

        #region UPDATE

        public void UpdateBranch(Branch branch)
        {
            string sql = @"UPDATE BRANCHES SET
                           name = @name
                           WHERE
                           branch_id = @branch_id";
            AddParameter("@branch_id", branch.BranchId);
            AddParameter("@name", branch.Name);
            ExecuteQuery(sql);
            return;
        }

        public void UpdateBranchStatus(long branchId, string status)
        {
            string sql = @"UPDATE BRANCHES SET
                           status = @status
                           WHERE
                           branch_id = @branch_id";
            AddParameter("@branch_id", branchId);
            AddParameter("@status", status);
            ExecuteQuery(sql);
            return;
        }
        #endregion

        #region DELETE
        #endregion
    }
}
