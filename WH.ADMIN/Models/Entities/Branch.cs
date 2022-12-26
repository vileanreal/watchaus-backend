using WH.ADMIN.Models.RequestResponse;

namespace WH.ADMIN.Models.Entities
{
    public class Branch
    {
        public long BranchId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }

        public Branch() { }

        public Branch(AddBranchRequest request)
        {
            this.Name = request.Name;
        }

        public Branch(UpdateBranchRequest request)
        {
            this.BranchId = request.BranchId ?? 0;
            this.Name = request.Name;
        }

        public Branch(DeleteBranchRequest request)
        {
            this.BranchId = request.BranchId ?? 0;
        }
    }


}
