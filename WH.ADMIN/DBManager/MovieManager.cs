using DBHelper;
using WH.ADMIN.Models;
using WH.ADMIN.Models.Entities;

namespace WH.ADMIN.DBManager
{
    public class MovieManager: BaseManager
    {
        #region SELECT
        public Movies GetMovie(string col, string val) {
            string sql = @$"SELECT * FROM MOVIES WHERE {col} = @val";
            AddParameter("@val", val);
            return SelectSingle<Movies>(sql);
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
        #endregion

        #region DELETE
        #endregion

    }
}
