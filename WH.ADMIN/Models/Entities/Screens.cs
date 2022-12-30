using WH.ADMIN.Models.RequestResponse;

namespace WH.ADMIN.Models.Entities
{
    public class Screens
    {
        public long ScreenId { get; set; }
        public string ScreenName { get; set; }
        public long BranchId { get; set; }
        public string BranchName { get; set; }
        public int NoOfSeats { get; set; }
        public decimal Charge { get; set; }
        public string Status { get; set; }
        public List<ScreensShowTimes> ShowTimesList { get; set; }
        public List<ScreenAssignedMovies> AssignedMoviesList { get; set; }

        public Screens() { }

        public Screens(AddScreenRequest request)
        {
            BranchId = request.BranchId ?? 0;
            ScreenName = request.ScreenName;
            NoOfSeats = request.NoOfSeats ?? 0;
            Charge = request.Charge ?? 0;
            ShowTimesList = request.ShowTimes.Select(x => new ScreensShowTimes()
            {
                TimeStart = x
            }).ToList();
        }


        public Screens(UpdateScreenRequest request)
        {
            ScreenId = request.ScreenId ?? 0;
            BranchId = request.BranchId ?? 0;
            ScreenName = request.ScreenName;
            NoOfSeats = request.NoOfSeats ?? 0;
            Charge = request.Charge ?? 0;
            ShowTimesList = request.ShowTimes.Select(x => new ScreensShowTimes()
            {
                TimeStart = x
            }).ToList();
        }
    }
}
