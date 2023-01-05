using WH.ADMIN.Models.Entities;

namespace WH.ADMIN.Models.RequestResponse
{
    public class GetBranchListResponse
    {
        public long BranchId { get; set; }
        public string Name { get; set; }

        public GetBranchListResponse(Branch branch)
        {
            this.BranchId = branch.BranchId;
            this.Name = branch.Name;
        }
    }
}
