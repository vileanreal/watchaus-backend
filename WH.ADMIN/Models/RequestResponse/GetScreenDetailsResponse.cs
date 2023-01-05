using WH.ADMIN.Models.Entities;

namespace WH.ADMIN.Models.RequestResponse
{
    public class GetScreenDetailsResponse
    {
        public long ScreenId { get; set; }
        public string ScreenName { get; set; }
        public long BranchId { get; set; }
        public string BranchName { get; set; }
        public int NoOfSeats { get; set; }
        public decimal Charge { get; set; }
        public List<string> ShowTimes { get; set; }

        public GetScreenDetailsResponse(Screens screen)
        {
            ScreenId = screen.ScreenId;
            BranchId = screen.BranchId;
            BranchName = screen.BranchName;
            ScreenName = screen.ScreenName;
            NoOfSeats = screen.NoOfSeats;
            Charge = screen.Charge;
            ShowTimes = screen.ShowTimesList.Select(x => x.TimeStart).ToList();
        }
    }
}
