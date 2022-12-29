using DBHelper;
using WH.ADMIN.Models;
using WH.ADMIN.Models.Entities;

namespace WH.ADMIN.DBManager
{
    public class MovieManager: BaseManager
    {
        #region SELECT
        public Movies SelectMovie(string col, string val)
        {
            string sql = @$"SELECT * FROM MOVIES WHERE {col} = @val AND status = @status";
            AddParameter("@val", val);
            AddParameter("@status", Status.ACTIVE);
            return SelectSingle<Movies>(sql);
        }

        public List<Movies> SelectMovieList()
        {
            string sql = @$"SELECT * FROM MOVIES WHERE status = @status";
            AddParameter("@status", Status.ACTIVE);
            return SelectList<Movies>(sql);
        }

        public List<MoviesGenre> SelectMovieGenre(long movieId) {
            string sql = @"SELECT 
                           mg.mg_id,
                           mg.movie_id,
                           mg.genre_id,
                           ig.name
                           FROM MOVIES_GENRE mg 
                           LEFT JOIN I_GENRES ig ON ig.genre_id = mg.genre_id
                           WHERE mg.movie_id = @movie_id";
            AddParameter("@movie_id", movieId);
            return SelectList<MoviesGenre>(sql);
        }

        public List<MoviesImages> SelectMovieImages(long movieId) {
            string sql = @"SELECT * FROM MOVIES_IMAGES WHERE movie_id = @movie_id";
            AddParameter("@movie_id", movieId);
            return SelectList<MoviesImages>(sql);
        }


        public List<MoviesSchedule> SelectMovieSchedule(long movieId) {
            string sql = @"SELECT 
                           schedule_id,
                           movie_id,
                           DATE_FORMAT(date_start, '%Y-%m-%d') as date_start,
                           DATE_FORMAT(date_end, '%Y-%m-%d') as date_end
                           FROM MOVIES_SCHEDULE WHERE movie_id = @movie_id";
            AddParameter("@movie_id", movieId);
            return SelectList<MoviesSchedule>(sql);
        }




        #endregion

        #region INSERT
        public long InsertMovie(Movies movie) {
            string sql = @"INSERT INTO MOVIES (title, description, duration, status) 
                                       VALUES (@title, @description, @duration, @status)";
            AddParameter("@title", movie.Title);
            AddParameter("@description", movie.Description);
            AddParameter("@duration", movie.Duration);
            AddParameter("@status", Status.ACTIVE);
            ExecuteQuery(sql);
            return LastInsertedId;
        }

        public long InsertMovieGenre(MoviesGenre genre) {
            string sql = @"INSERT INTO MOVIES_GENRE (movie_id, genre_id)
                                             VALUES (@movie_id, @genre_id)";
            AddParameter("@movie_id", genre.MovieId);
            AddParameter("@genre_id", genre.GenreId);
            ExecuteQuery(sql);
            return LastInsertedId;
        }

        public long InsertMovieImages(MoviesImages image)
        {
            string sql = @"INSERT INTO MOVIES_IMAGES (movie_id, path, is_movie_poster) 
                                              VALUES (@movie_id, @path, @is_movie_poster)";
            AddParameter("@movie_id", image.MovieId);
            AddParameter("@path", image.Path);
            AddParameter("@is_movie_poster", image.IsMoviePoster);
            ExecuteQuery(sql);
            return LastInsertedId;
        }

        public long InsertMovieSchedule(MoviesSchedule schedule) {
            string sql = @"INSERT INTO MOVIES_SCHEDULE (movie_id, date_start, date_end) 
                                                VALUES (@movie_id, @date_start, @date_end)";
            AddParameter("@movie_id", schedule.MovieId);
            AddParameter("@date_start", schedule.DateStart);
            AddParameter("@date_end", schedule.DateEnd);
            ExecuteQuery(sql);
            return LastInsertedId;
           
        }
        #endregion

        #region UPDATE
        public void UpdateMovieStatus(long movieId, string status) {
            string sql = @"UPDATE MOVIES SET status = @status WHERE movie_id = @movie_id";
            AddParameter("@movie_id", movieId);
            AddParameter("@status", status);
            ExecuteQuery(sql);
            return;
        }

        public void UpdateMovieDetails(Movies movie) {
            string sql = @"UPDATE MOVIES SET 
                           title = @title,
                           description = @description,
                           duration = @duration
                           WHERE movie_id = @movie_id";
            AddParameter("@movie_id", movie.MovieId);
            AddParameter("@title", movie.Title);
            AddParameter("@description", movie.Description);
            AddParameter("@duration", movie.Duration);
            ExecuteQuery(sql);
            return;
        }

        #endregion

        #region DELETE
        public void DeleteMovieGenres(long movieId) {
            string sql = @"DELETE FROM MOVIES_GENRE WHERE movie_id = @movie_id";
            AddParameter("@movie_id", movieId);
            ExecuteQuery(sql);
            return;
        }
        #endregion

    }
}
