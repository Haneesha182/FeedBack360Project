using DLFeedBack360.Infrastructure.Abstract;
using DLFeedBack360.Infrastructure.Repository;
using DLFeedBack360.ViewModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ULFeedBack360;
using Microsoft.AspNetCore.Authorization;


namespace FeedBack360Project.Controllers
{
    [Authorize]
    public class UserManagementController : Controller
    {
        IUserManagementRepo _umRepo = new UserManagementRepo();

        public IActionResult Home()
        {
            ViewBag.MyUserID = HttpContext.Session.GetString("CurrentUser");
            ViewBag.MyRole = HttpContext.Session.GetString("CurrentUserRole");
            FeedbackUtility feedbackUtility = FeedbackUtility.getInstant();
            feedbackUtility.SendEmail("haneesha0809@gmail.com", "pinjarihaneesha200@gmail.com", "Test Mail", "Hello All, This is test mail");
            return View();
        }

        //Login Code

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginViewModel loginView)
        {        
            if (ModelState.IsValid)
            {
                string result = _umRepo.UserLogin(loginView);
                if (result == "Success")
                {
                    HttpContext.Session.SetString("CurrentUser", loginView.UserID.ToString());
                    HttpContext.Session.SetString("CurrentUserRole", _umRepo.GetRole(loginView.UserID.ToString()));
                    ViewBag.MyUserID = HttpContext.Session.GetString("CurrentUser");
                    ViewBag.MyRole = HttpContext.Session.GetString("CurrentUserRole");

                    //Authorization Code Start...............
                    string[] userRoles = new string[] { ViewBag.MyRole };

                    var claims = new List<Claim>();
                    claims.Add(new Claim("username", ViewBag.MyUserID));   // u001
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, ViewBag.MyUserID));      // 1111
                    foreach (var eachRole in userRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, eachRole));
                    }

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    HttpContext.SignInAsync(claimsPrincipal);

                    //Authorization Code End...............

                   // return RedirectToAction("Registration");
                }
                else
                {
                    ViewBag.LoginStatus = "Login failed, please check User ID and Password";
                    return View();
                }
            }
            return Registration();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }


        //Registration Code
        [Authorize(Roles = "HR")]
        [HttpGet]
        public IActionResult Registration()
        {
            ViewBag.MyUserID = HttpContext.Session.GetString("CurrentUser");
            ViewBag.MyRole = HttpContext.Session.GetString("CurrentUserRole");
            UserDetailViewModel userDetail = new UserDetailViewModel();
            userDetail.CountryDetail = _umRepo.GetCountries();
            userDetail.LanguageDetail = _umRepo.GetLanguages();
            userDetail.RoleDetail = _umRepo.GetRoles();
            return View("Registration", userDetail);
        }

        //[Authorize(Roles = "HR")]
        [HttpPost] 
        public IActionResult Registration(UserDetailViewModel userDetailView)
        {
            ViewBag.MyUserID = HttpContext.Session.GetString("CurrentUser");
            ViewBag.MyRole = HttpContext.Session.GetString("CurrentUserRole");
            string result = _umRepo.SetRegistration(userDetailView);
            UserDetailViewModel userDetail = new UserDetailViewModel();
            userDetail.CountryDetail = _umRepo.GetCountries();
            userDetail.LanguageDetail = _umRepo.GetLanguages();
            userDetail.RoleDetail = _umRepo.GetRoles();

            if (result == "Success")
            {
                //return RedirectToAction("Home");
                ViewBag.RegistrationStatus = "User registered successfully";
                return View(userDetail);
            }
            else
            {
                ViewBag.RegistrationStatus = "User registration failed!";
                return View(userDetail);
            }
        }
           



        //Change Password Code
        [AllowAnonymous]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            ViewBag.MyUserID = HttpContext.Session.GetString("CurrentUser");
            ViewBag.MyRole = HttpContext.Session.GetString("CurrentUserRole");
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel changePasswordView)
        {
            ViewBag.MyUserID = HttpContext.Session.GetString("CurrentUser");
            ViewBag.MyRole = HttpContext.Session.GetString("CurrentUserRole");
            string result = _umRepo.UpdatePassword(changePasswordView);
            if (result == "Success")
            {
                ViewBag.ChangePasswordStatus = "Password  changed successfully";
                //return RedirectToAction("Home");
                return View();
            }
            else
            {
                ViewBag.ChangePasswordStatus = "Password not changed, Please check!";
                return View();
            }

        }


        //Forget Password Code
        [AllowAnonymous]
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            ViewBag.MyUserID = HttpContext.Session.GetString("CurrentUser");
            ViewBag.MyRole = HttpContext.Session.GetString("CurrentUserRole");
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordViewModel forgotPasswordView, string btn)
        {
            ViewBag.MyUserID = HttpContext.Session.GetString("CurrentUser");
            ViewBag.MyRole = HttpContext.Session.GetString("CurrentUserRole");
            if (btn == "Send OTP")
            {
                string otp = _umRepo.SendOPT(forgotPasswordView.UserID.ToString());
                HttpContext.Session.SetString("MyOTP", otp.ToString());
                string str2 = HttpContext.Session.GetString("MyOTP");
            }
            else if (btn == "submit")
            {
                string str1 = HttpContext.Session.GetString("MyOTP");
                if (forgotPasswordView.PassCode == str1)
                {
                    string result = _umRepo.UserForgotPassword(forgotPasswordView);
                    ViewBag.Result = "Your password reset successfully";
                }
            }

            return View();
        }

    }
}
