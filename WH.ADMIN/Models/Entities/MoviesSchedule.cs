namespace WH.ADMIN.Models.Entities
{
    public class MoviesSchedule
    {
        public long ScheduleId { get; set; }
        public long MovieId { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
    }
}
