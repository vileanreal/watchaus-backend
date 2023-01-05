using DBHelper;
using WH.ADMIN.Models;
using WH.ADMIN.Models.Entities;

namespace WH.ADMIN.DBManager
{
    public class ScreenManager : BaseManager
    {
        #region SELECT
        public Screens SelectScreen(string col, string val)
        {
            string sql = @$"SELECT 
                           s.*,
                           b.name as branch_name
                           FROM SCREENS s
                           LEFT JOIN BRANCHES b ON b.branch_id = s.branch_id
                           WHERE {col} = @val AND 
                                 s.status = @status AND
                                 b.status = @status";
            AddParameter("@val", val);
            AddParameter("@status", Status.ACTIVE);
            return SelectSingle<Screens>(sql);
        }

        public List<Screens> SelectScreenList(long branchId)
        {
            string sql = @$"SELECT * FROM SCREENS WHERE branch_id = @branch_id AND status = @status";
            AddParameter("@branch_id", branchId);
            AddParameter("@status", Status.ACTIVE);
            return SelectList<Screens>(sql);
        }

        public List<ScreensShowTimes> SelectShowTimes(long screenId)
        {
            string sql = @"SELECT 
                           showtime_id,
                           screen_id,
                           TIME_FORMAT(time_start, '%H:%i') time_start
                           FROM SCREENS_SHOW_TIMES WHERE screen_id = @screen_id";
            AddParameter("@screen_id", screenId);
            return SelectList<ScreensShowTimes>(sql);
        }

        public ScreenAssignedMovies SelectAssignedMovie(long screenId, long movieId)
        {
            string sql = @"SELECT * FROM SCREENS_ASSIGNED_MOVIES
                           WHERE screen_id = @screen_id AND movie_id = @movie_id AND status = @status";
            AddParameter("@screen_id", screenId);
            AddParameter("@movie_id", movieId);
            AddParameter("@status", Status.ACTIVE);
            return SelectSingle<ScreenAssignedMovies>(sql);
        }

        public List<ScreenAssignedMovies> SelectAssignedMovieList(long screenId)
        {
            string sql = @"SELECT * FROM SCREENS_ASSIGNED_MOVIES
                           WHERE screen_id = @screen_id AND status = @status";
            AddParameter("@screen_id", screenId);
            AddParameter("@status", Status.ACTIVE);
            return SelectList<ScreenAssignedMovies>(sql);
        }

        #endregion


        #region INSERT
        public long InsertScreen(Screens screen)
        {
            string sql = @"INSERT INTO SCREENS (branch_id, screen_name, no_of_seats, charge, status)
                                        VALUES (@branch_id, @screen_name, @no_of_seats, @charge, @status)";
            AddParameter("@branch_id", screen.BranchId);
            AddParameter("@screen_name", screen.ScreenName);
            AddParameter("@no_of_seats", screen.NoOfSeats);
            AddParameter("@charge", screen.Charge);
            AddParameter("@status", Status.ACTIVE);
            ExecuteQuery(sql);
            return LastInsertedId;

        }

        public long InsertShowTimes(ScreensShowTimes showTime)
        {
            string sql = @"INSERT INTO SCREENS_SHOW_TIMES (screen_id, time_start)
                                                   VALUES (@screen_id, @time_start)";
            AddParameter("@screen_id", showTime.ScreenId);
            AddParameter("@time_start", showTime.TimeStart);
            ExecuteQuery(sql);
            return LastInsertedId;
        }

        public long InsertAssignedMovies(ScreenAssignedMovies assignedMovies)
        {
            string sql = @"INSERT INTO SCREENS_ASSIGNED_MOVIES (movie_id, screen_id, status)
                                                        VALUES (@movie_id, @screen_id, @status)";
            AddParameter("@movie_id", assignedMovies.MovieId);
            AddParameter("@screen_id", assignedMovies.ScreenId);
            AddParameter("@status", Status.ACTIVE);
            ExecuteQuery(sql);
            return LastInsertedId;
        }
        #endregion


        #region UPDATE
        public void UpdateScreen(Screens screen)
        {
            string sql = @"UPDATE SCREENS SET
                           branch_id = @branch_id,
                           screen_name = @screen_name,
                           no_of_seats = @no_of_seats,
                           charge = @charge
                           WHERE screen_id = @screen_id";
            AddParameter("@screen_id", screen.ScreenId);
            AddParameter("@branch_id", screen.BranchId);
            AddParameter("@screen_name", screen.ScreenName);
            AddParameter("@no_of_seats", screen.NoOfSeats);
            AddParameter("@charge", screen.Charge);
            ExecuteQuery(sql);
        }

        public void UpdateScreenStatus(long screenId, string status)
        {
            string sql = @"UPDATE SCREENS SET status = @status WHERE screen_id = @screen_id";
            AddParameter("@screen_id", screenId);
            AddParameter("@status", status);
            ExecuteQuery(sql);
        }

        public void UpdateAssignedMovieStatus(long movieId, long screenId, string status)
        {
            string sql = @"UPDATE SCREENS_ASSIGNED_MOVIES SET status = @status WHERE movie_id = @movie_id AND screen_id = @screen_id";
            AddParameter("@movie_id", movieId);
            AddParameter("@screen_id", screenId);
            AddParameter("@status", status);
            ExecuteQuery(sql);
        }

        #endregion


        #region DELETE
        public void DeleteShowTimes(long screenId)
        {
            string sql = @"DELETE FROM SCREENS_SHOW_TIMES WHERE screen_id = @screen_id";
            AddParameter("@screen_id", screenId);
            ExecuteQuery(sql);
        }

        #endregion
    }
}
