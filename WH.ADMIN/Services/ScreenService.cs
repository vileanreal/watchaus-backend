using Utilities;
using WH.ADMIN.DBManager;
using WH.ADMIN.Models;
using WH.ADMIN.Models.Entities;
using WH.PORTAL.Services;

namespace WH.ADMIN.Services
{
    public class ScreenService
    {

        public bool IsScreenExist(long screenId) {
            using var manager = new ScreenManager();
            var screen = manager.SelectScreen("screen_id", screenId.ToString());
            return screen != null;
        }

        public Screens GetScreenDetails(long screenId) {
            using var manager = new ScreenManager();
            var screen = manager.SelectScreen("screen_id", screenId.ToString());
            if (screen == null) {
                return null;
            }
            screen.ShowTimesList = manager.SelectShowTimes(screenId);
            return screen;
        }

        public List<Screens> GetScreenList(long branchId) {
            using var manager = new ScreenManager();
            var screenList = manager.SelectScreenList(branchId);
            return screenList;
        }

        public OperationResult AddScreen(Screens screen, Session session) {

            BranchService branchService = new BranchService();
            
            if (!branchService.IsBranchExist(screen.BranchId)) {
                return OperationResult.Failed("Branch doesn't exist.");
            }

            foreach (var item in screen.ShowTimesList) {
                if (!CommonHelper.IsMilitaryTime(item.TimeStart))
                {
                    return OperationResult.Failed("Time should be in military time format.");
                }
            }

            using ScreenManager screenManager = new ScreenManager();
            screenManager.BeginTransaction();
            var screenId = screenManager.InsertScreen(screen);
            foreach (var item in screen.ShowTimesList) { 
                item.ScreenId = screenId;
                screenManager.InsertShowTimes(item);
            }

            CommonService commonService = new CommonService();
            commonService.InsertAuditTrailLogs(new AuditTrails() { 
                UserId = session.Id,
                Description = $"Added new screen: {screen.ScreenName}, to branch: {screen.BranchId}"
            });

            screenManager.Commit();

            return OperationResult.Success("Added screen successfully.");
        }


        public OperationResult UpdateScreen(Screens screen, Session session) {
            BranchService branchService = new BranchService();

            if (!IsScreenExist(screen.ScreenId)) {
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

    }
}
