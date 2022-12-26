using WH.ADMIN.Models.RequestResponse;

namespace WH.ADMIN.Models.Entities
{
    public class Movies
    {
        public long MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long Duration { get; set; }
        public string Status { get; set; }

        public List<MoviesGenre> GenreList { get; set; }
        public List<MoviesImages> ImageList { get; set; }
        public List<MoviesSchedule> ScheduleList { get; set; }

        public Movies() { }

        public Movies(AddMovieRequest request) {
            this.Title = request.Title;
            this.Description = request.Description;
            this.Duration = request.Duration;
            
            this.GenreList = request.Genres.Select(item => new MoviesGenre() { 
                GenreId = item
            }).ToList();
            
            this.ImageList = request.Screenshots.Select(item => new MoviesImages() { 
                Base64Img = item,
                IsMoviePoster = false
              
            }).ToList();
            
            this.ImageList.Add(new MoviesImages() { 
                Base64Img = request.MoviePosterImg,
                IsMoviePoster = true
            });

            this.ScheduleList = new List<MoviesSchedule>
            {
                new MoviesSchedule()
                {
                    DateStart = request.showingDateStart,
                    DateEnd = request.showingDateEnd,
                }
            };

        }
    }
}
