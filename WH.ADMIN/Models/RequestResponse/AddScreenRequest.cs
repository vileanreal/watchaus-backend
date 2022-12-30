namespace WH.ADMIN.Models.RequestResponse
{
    public class AddScreenRequest
    {
        public long? BranchId { get; set; }
        public string ScreenName { get; set; }
        public int? NoOfSeats { get; set; }
        public Decimal? Charge { get; set; }
        public List<string> ShowTimes { get; set; }
    }
}
