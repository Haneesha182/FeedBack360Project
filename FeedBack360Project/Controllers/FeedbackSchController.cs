using DLFeedBack360.Infrastructure.Abstract;
using DLFeedBack360.Infrastructure.Repository;
using DLFeedBack360.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace FeedBack360Project.Controllers
{
    [Authorize]
    public class FeedbackSchController : Controller
    {
        IUserManagementRepo _umRepo = new UserManagementRepo();

        [Authorize(Roles = "HR,Admin")]
        [HttpGet]
        public IActionResult ScheduleFB()
        {
            ViewBag.MyUserID = HttpContext.Session.GetString("CurrentUser");
            ViewBag.MyRole = HttpContext.Session.GetString("CurrentUserRole");
            FeedbackSchDtlViewModel FeedbackSchDtl = new FeedbackSchDtlViewModel();
            FeedbackSchDtl.getUserIDDetail = _umRepo.GetUserID();
            FeedbackSchDtl.getFeedbackCatagoryDetail = _umRepo.GetFeedbackcatagory();
            FeedbackSchDtl.getProviderUserIDDetail = _umRepo.GetProviderUserID();

            return View(FeedbackSchDtl);
        }

        [Authorize(Roles = "HR,Admin")]
        [HttpPost]
        public IActionResult ScheduleFB(FeedbackSchDtlViewModel fbsdvm)
        {
            ViewBag.MyUserID = HttpContext.Session.GetString("CurrentUser");
            ViewBag.MyRole = HttpContext.Session.GetString("CurrentUserRole");
            string msg = _umRepo.SetFeedbackProviderList(fbsdvm);
            ViewBag.Result = msg;
            if (msg == "Success")
            {
                FeedbackSchDtlViewModel FeedbackSchDtl = new FeedbackSchDtlViewModel();
                FeedbackSchDtl.getUserIDDetail = _umRepo.GetUserID();
                FeedbackSchDtl.getFeedbackCatagoryDetail = _umRepo.GetFeedbackcatagory();
                FeedbackSchDtl.getProviderUserIDDetail = _umRepo.GetProviderUserID();
                return View(FeedbackSchDtl);
            }
            return View(fbsdvm);
        }

        [Authorize(Roles = "HR,Admin")]
        [HttpPost]
        public JsonResult GetFBProvider(string userId, string categoryid)
        {
            ViewBag.MyUserID = HttpContext.Session.GetString("CurrentUser");
            ViewBag.MyRole = HttpContext.Session.GetString("CurrentUserRole");
            List<System.Web.Mvc.SelectListItem> FBProviderList = new List<System.Web.Mvc.SelectListItem>();
            FBProviderList = _umRepo.GetFeedbackProviderList(categoryid, userId);

            return Json(FBProviderList);
        }
    }
}
