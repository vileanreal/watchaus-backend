using Utilities;
using WH.ADMIN.DBManager;
using WH.ADMIN.Models;
using WH.ADMIN.Models.Entities;
using WH.PORTAL.Services;

namespace WH.ADMIN.Services
{
    public class ScreenService
    {


        public bool IsScreenExist(long screenId)
        {
            using var manager = new ScreenManager();
            var screen = manager.SelectScreen("screen_id", screenId.ToString());
            return screen != null;
        }


        public bool IsMovieAlreadyAssignedToScreen(long movieId, long screenId)
        {
            using var manager = new ScreenManager();
            var screen = manager.SelectAssignedMovie(screenId, movieId);
            return screen != null;
        }

        public Screens GetScreenDetails(long screenId)
        {
            using var manager = new ScreenManager();
            var screen = manager.SelectScreen("screen_id", screenId.ToString());
            if (screen == null)
            {
                return null;
            }
            screen.ShowTimesList = manager.SelectShowTimes(screenId);
            return screen;
        }

        public List<Screens> GetScreenList(long branchId)
        {
            using var manager = new ScreenManager();
            var screenList = manager.SelectScreenList(branchId);
            return screenList;
        }


        public List<ScreenAssignedMovies> GetAssignedMovieList(long screenId)
        {
            using var manager = new ScreenManager();
            var movieList = manager.SelectAssignedMovieList(screenId);
            return movieList;
        }

        public OperationResult AddScreen(Screens screen, Session session)
        {

            BranchService branchService = new BranchService();

            if (!branchService.IsBranchExist(screen.BranchId))
            {
                return OperationResult.Failed("Branch doesn't exist.");
            }

            foreach (var item in screen.ShowTimesList)
            {
                if (!CommonHelper.IsMilitaryTime(item.TimeStart))
                {
                    return OperationResult.Failed("Time should be in military time format.");
                }
            }

            using ScreenManager screenManager = new ScreenManager();
            screenManager.BeginTransaction();
            var screenId = screenManager.InsertScreen(screen);
            foreach (var item in screen.ShowTimesList)
            {
                item.ScreenId = screenId;
                screenManager.InsertShowTimes(item);
            }

            CommonService commonService = new CommonService();
            commonService.InsertAuditTrailLogs(new AuditTrails()
            {
                UserId = session.Id,
                Description = $"Added new screen: {screen.ScreenName}, to branch: {screen.BranchId}"
            });

            screenManager.Commit();

            return OperationResult.Success("Added screen successfully.");
        }


        public OperationResult UpdateScreen(Screens screen, Session session)
        {
            BranchService branchService = new BranchService();

            if (!IsScreenExist(screen.ScreenId))
            {
                return OperationResult.Failed("Screen doesn't exist");
            }

            if (!branchService.IsBranchExist(screen.BranchId))
            {
                return OperationResult.Failed("Branch doesn't exist.");
            }
            foreach (var item in screen.ShowTimesList)
            {
                if (!CommonHelper.IsMilitaryTime(item.TimeStart))
                {
                    return OperationResult.Failed("Time should be in military time format.");
                }
            }

            using ScreenManager screenManager = new ScreenManager();
            screenManager.BeginTransaction();
            screenManager.UpdateScreen(screen);
            screenManager.DeleteShowTimes(screen.ScreenId);
            foreach (var item in screen.ShowTimesList)
            {
                item.ScreenId = screen.ScreenId;
                screenManager.InsertShowTimes(item);
            }

            CommonService commonService = new CommonService();
            commonService.InsertAuditTrailLogs(new AuditTrails()
            {
                UserId = session.Id,
                Description = $"Updated screen: {screen.ScreenId}, to branch: {screen.BranchId}"
            });

            screenManager.Commit();


            return OperationResult.Success("Updated screen successfully");
        }



        public OperationResult DeleteScreen(long screenId, Session session)
        {
            BranchService branchService = new BranchService();

            if (!IsScreenExist(screenId))
            {
                return OperationResult.Failed("Screen doesn't exist");
            }

            using ScreenManager screenManager = new ScreenManager();
            screenManager.BeginTransaction();
            screenManager.UpdateScreenStatus(screenId, Status.DELETED);

            CommonService commonService = new CommonService();
            commonService.InsertAuditTrailLogs(new AuditTrails()
            {
                UserId = session.Id,
                Description = $"Deleted screen: {screenId}"
            });

            screenManager.Commit();

            return OperationResult.Success("Deleted screen successfully");
        }


        public OperationResult AssignMovieToScreen(ScreenAssignedMovies assignedMovie, Session session)
        {

            if (!IsScreenExist(assignedMovie.ScreenId))
            {
                return OperationResult.Failed("Screen doesn't exist.");
            }

            var movieService = new MovieService();

            if (!movieService.IsMovieExist(assignedMovie.MovieId))
            {
                return OperationResult.Failed("Movie doesn't exist.");
            }

            if (IsMovieAlreadyAssignedToScreen(assignedMovie.MovieId, assignedMovie.ScreenId))
            {
                return OperationResult.Failed("Movie is already assigned to screen.");
            }

            using var screenManager = new ScreenManager();
            screenManager.BeginTransaction();

            screenManager.InsertAssignedMovies(assignedMovie);

            CommonService commonService = new CommonService();
            commonService.InsertAuditTrailLogs(new AuditTrails()
            {
                UserId = session.Id,
                Description = $"Assigned movie: {assignedMovie.MovieId} to screen: {assignedMovie.ScreenId}"
            });

            screenManager.Commit();
            return OperationResult.Success("Assigned movie to screen successfully.");
        }



        public OperationResult DeleteAssignedMovie(ScreenAssignedMovies assignedMovie, Session session)
        {

            if (!IsScreenExist(assignedMovie.ScreenId))
            {
                return OperationResult.Failed("Screen doesn't exist.");
            }

            var movieService = new MovieService();

            if (!movieService.IsMovieExist(assignedMovie.MovieId))
            {
                return OperationResult.Failed("Movie doesn't exist.");
            }

            if (!IsMovieAlreadyAssignedToScreen(assignedMovie.MovieId, assignedMovie.ScreenId))
            {
                return OperationResult.Failed("Movie isn't assigned to screen.");
            }

            using var screenManager = new ScreenManager();
            screenManager.BeginTransaction();

            screenManager.UpdateAssignedMovieStatus(assignedMovie.MovieId, assignedMovie.ScreenId, Status.DELETED);

            CommonService commonService = new CommonService();
            commonService.InsertAuditTrailLogs(new AuditTrails()
            {
                UserId = session.Id,
                Description = $"Delete assigned movie: {assignedMovie.MovieId} to screen: {assignedMovie.ScreenId}"
            });

            screenManager.Commit();
            return OperationResult.Success("Delete assigned movie to screen successfully.");
        }



    }
}
