using DLFeedBack360.Infrastructure.Abstract;
using DLFeedBack360.Infrastructure.Repository;
using DLFeedBack360.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace FeedBack360Project.Controllers
{
    [Authorize]
    public class ProvideFeedbackController : Controller
    {
        IProvideFeedbackRepo _Repofb = new ProvideFeedbackRepo();

        [AllowAnonymous]
        [HttpGet]
        public IActionResult CandidateList()
        {
            ViewBag.MyUserID = HttpContext.Session.GetString("CurrentUser");
            ViewBag.MyRole = HttpContext.Session.GetString("CurrentUserRole");
            List<CandidateListViewModel> candidateList = _Repofb.GetCandidateList(_Repofb.getUserIDtoID(ViewBag.MyUserID));
            return View(candidateList);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult ProvidingFeedback(string userId)
        {
            ViewBag.MyUserID = HttpContext.Session.GetString("CurrentUser");
            ViewBag.MyRole = HttpContext.Session.GetString("CurrentUserRole");
            HttpContext.Session.SetString("CandidateUserName", userId);
            FeedbackProvidingViewModel Fbpvm = _Repofb.getFeedbackQuestion();
            return View(Fbpvm);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ProvidingFeedback(FeedbackProvidingViewModel model)
        {
            ViewBag.MyUserID = HttpContext.Session.GetString("CurrentUser");
            ViewBag.MyRole = HttpContext.Session.GetString("CurrentUserRole");
            ViewBag.ToUserID = HttpContext.Session.GetString("CandidateUserName");
            string result = _Repofb.SetFeedbackRatings(model, _Repofb.getUserIDtoID(ViewBag.ToUserID), _Repofb.getUserIDtoID(ViewBag.MyUserID));
            FeedbackProvidingViewModel Fbpvm = _Repofb.getFeedbackQuestion();
            if (result == "Success")
            {
                ViewBag.Message = "Rating submitted successfully";
                return View(Fbpvm);
            }
            else
            {
                ViewBag.Message = result;
                return View(Fbpvm);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult FeedbackResult()
        {
            ViewBag.MyUserID = HttpContext.Session.GetString("CurrentUser");
            ViewBag.MyRole = HttpContext.Session.GetString("CurrentUserRole");
            List<FeedbackResultViewModel> lst = _Repofb.GetFeedbackResult();
            return View(lst);
        }
    }
}
