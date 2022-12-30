using WH.ADMIN.Models.Entities;

namespace WH.ADMIN.Models.RequestResponse
{
    public class GetScreenListResponse
    {
        public long ScreenId { get; set; }
        public long BranchId { get; set; }
        public string ScreenName { get; set; }
        public int NoOfSeats { get; set; }
        public decimal Charge { get; set; }

        public GetScreenListResponse(Screens screen)
        {
            ScreenId = screen.ScreenId;
            BranchId = screen.BranchId;
            ScreenName = screen.ScreenName;
            NoOfSeats = screen.NoOfSeats;
            Charge = screen.Charge;
        }
    }
}
