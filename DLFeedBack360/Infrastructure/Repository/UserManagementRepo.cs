using DLFeedBack360.Models;
using DLFeedBack360.Infrastructure.Abstract;
using DLFeedBack360.ViewModel;
using System;
using System.Data;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULFeedBack360;
using System.Web.Mvc;


namespace DLFeedBack360.Infrastructure.Repository
{
    public class UserManagementRepo : IUserManagementRepo
    {

        ProjectFB360Context _dbContext = new ProjectFB360Context();
        public UserManagementRepo()
        {
            _dbContext = new ProjectFB360Context();
        }

        public List<CountryViewModel> GetCountries()
        {
            List<CountryDetail> countries = _dbContext.CountryDetails.ToList();
            List<CountryViewModel> countryDetails = new List<CountryViewModel>();

            foreach (CountryDetail _country in countries)
            {
                CountryViewModel cvm = new CountryViewModel();
                cvm.CountryID = _country.CountryId;
                cvm.CountryName = _country.CountryName;
                countryDetails.Add(cvm);
            }
            return countryDetails;
        }

        public List<LanguageViewModel> GetLanguages()
        {
            List<LanguageDetail> Lang = _dbContext.LanguageDetails.ToList();
            List<LanguageViewModel> languageDetails = new List<LanguageViewModel>();

            foreach (LanguageDetail _lang in Lang)
            {
                LanguageViewModel lvm = new LanguageViewModel();
                lvm.LangID = _lang.LanguageId;
                lvm.LangName = _lang.LanguageName;
                languageDetails.Add(lvm);
            }
            return languageDetails;
        }

        public List<RoleDetailViewModel> GetRoles()
        {
            List<RoleDetail> roles = _dbContext.RoleDetails.ToList();
            List<RoleDetailViewModel> roleDetails = new List<RoleDetailViewModel>();

            foreach (RoleDetail _role in roles)
            {
                RoleDetailViewModel rdvm = new RoleDetailViewModel();
                rdvm.RoleID = _role.RoleId;
                rdvm.RoleName = _role.RoleName;
                rdvm.RoleSequenceID = _role.RoleSequenceId;
                roleDetails.Add(rdvm);
            }
            return roleDetails;
        }

        public string GetRole(string userID)            //Get role for current user
        {
            string Role = string.Empty;

            try
            {
                var query = from u in _dbContext.UserDetails
                            join r in _dbContext.RoleDetails on u.Role equals r.RoleId.ToString()
                            where u.UserId == userID
                            select new
                            {
                                r.RoleName
                            };
                var result = query.ToList();
                foreach (var r in result)
                {
                    Role = r.RoleName;
                }

            }
            catch (Exception ex)
            {

            }
            return Role;

        }


        public string SetRegistration(UserDetailViewModel uservm)
        {
            string msg = string.Empty;
            string lang = string.Empty;
            UserDetail userDetail = new UserDetail();

            try
            {
                userDetail.UserId = uservm.UserID;
                userDetail.Password = uservm.Password;
                userDetail.EmpId = uservm.EmpId;
                userDetail.Mobile = uservm.Mobile;
                userDetail.Name = uservm.Name;
                userDetail.IsActive = uservm.IsActive;
                userDetail.Country = Convert.ToInt32(uservm.Country);


                foreach (LanguageViewModel lvm in uservm.LanguageDetail)
                {
                    if (lvm.Selected == true)
                    {
                        if (lang == "")
                        {
                            lang = lvm.LangID.ToString();
                        }
                        else
                        {
                            lang = string.Concat(lang, ",", lvm.LangID.ToString());
                        }
                    }
                }

                userDetail.Language = lang;
                userDetail.Role = uservm.Role;

                _dbContext.UserDetails.Add(userDetail);
                _dbContext.SaveChanges();
                msg = "Success";
            }
            catch (Exception ex)
            {
                msg = "Failed";
            }
            return msg;
        }

        public string UpdatePassword(ChangePasswordViewModel cpv)
        {
            string msg = string.Empty;
            try
            {
                UserDetail userDetail = new UserDetail();
                userDetail = _dbContext.UserDetails.FirstOrDefault(x => x.UserId == cpv.UserID && x.Password == cpv.CurrentPassword);

                if (userDetail != null && userDetail.Password == cpv.CurrentPassword)
                {
                    userDetail.Password = cpv.NewPassword;
                    _dbContext.SaveChanges();

                    msg = "Success";
                }
                else
                {
                    msg = "Failed";
                }

            }
            catch (Exception ex)
            {
                msg = "Failed";
            }

            return msg;

        }

        public string UserLogin(LoginViewModel lvm)
        {
            string result = string.Empty;
            try
            {
                UserDetail userobj = _dbContext.UserDetails.FirstOrDefault(x => x.UserId == lvm.UserID);
                if (userobj == null)
                {
                    result = "data is null";
                }
                else
                {
                    result = "Success";
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public string SendOPT(string emailid)
        {
            string msg = string.Empty;
            try
            {
                Random obj = new Random();
                int otp = obj.Next(11111, 99999);
                FeedbackUtility feedbackUtility = FeedbackUtility.getInstant();
                string otpText = "Hello your OTP is :" + otp.ToString();
                feedbackUtility.SendEmail("haneesha0809@gmail.com", emailid, "OTP Test Mail", otpText);     
                //feedbackUtility.SendEmail("pinjarihaneesha2000@gmail.com", "haneesha0809@gmail.com", "OTP Test Mail", otpText);
                msg = otp.ToString();
            }
            catch (Exception ex)
            {

            }

            return msg;
        }

        public string UserForgotPassword(ForgotPasswordViewModel fpvm)
        {
            string msg = string.Empty;
            try
            {
                UserDetail userDetail = _dbContext.UserDetails.FirstOrDefault(x => x.UserId == fpvm.UserID);
                userDetail.Password = fpvm.NewPassword;
                _dbContext.SaveChanges();
                msg = "Success";
            }
            catch (Exception ex)
            {
                msg = "Failed";
            }

            return msg;
        }

        public List<UserDetailViewModel> GetUserID()
        {
            List<UserDetailViewModel> UserIdlst = new List<UserDetailViewModel>();
            try
            {
                List<UserDetail> userDetail = _dbContext.UserDetails.ToList();

                foreach (UserDetail ud in userDetail)
                {
                    FeedbackScheduleDetail fbSch = _dbContext.FeedbackScheduleDetails.FirstOrDefault(y => Convert.ToInt32(y.FbsuserId) == ud.Id);
                    if (fbSch == null)
                    {
                        UserDetailViewModel udvm = new UserDetailViewModel();

                        udvm.ID = ud.Id;
                        udvm.UserID = ud.UserId;
                        UserIdlst.Add(udvm);
                    }
                }
            }

            catch (Exception ex)
            {
                //msg = "Failed";
            }
            return UserIdlst;
        }
        public List<SelectListItem> GetProviderUserID()
        {
            List<SelectListItem> FBProviderList = new List<SelectListItem>();
            try
            {
                List<UserDetail> userDetail = _dbContext.UserDetails.ToList();

                foreach (UserDetail ud in userDetail)
                {
                    SelectListItem obj = new SelectListItem();

                    obj.Value = ud.Id.ToString();
                    obj.Text = ud.UserId;
                    FBProviderList.Add(obj);
                }

            }
            catch (Exception ex)
            {
                //msg = "Failed";
            }
            return FBProviderList;
        }

        public List<FeedbackCatagoryViewModel> GetFeedbackcatagory()
        {
            List<FeedbackCatagoryViewModel> lst = new List<FeedbackCatagoryViewModel>();
            try
            {
                List<FeedbackCategoryDetail> fbCatagoryDetail = _dbContext.FeedbackCategoryDetails.ToList();

                foreach (FeedbackCategoryDetail fbcm in fbCatagoryDetail)
                {
                    FeedbackCatagoryViewModel fbCatagoryDetailvm = new FeedbackCatagoryViewModel();
                    fbCatagoryDetailvm.FBCDCategoryID = fbcm.FbcdcategoryId;
                    fbCatagoryDetailvm.FBCDDescription = fbcm.Fbcddescription;

                    lst.Add(fbCatagoryDetailvm);
                }
                //msg = "Success";

            }
            catch (Exception ex)
            {
                //msg = "Failed";
            }
            return lst;
        }

        public List<SelectListItem> GetFeedbackProviderList(string catagory, string userID)
        {
            List<SelectListItem> lst = new List<SelectListItem>();

            UserDetail lst3 = _dbContext.UserDetails.FirstOrDefault(x => Convert.ToInt32(x.Id) == Convert.ToInt32(userID));  //Get Role for user id
            RoleDetail rd1 = _dbContext.RoleDetails.FirstOrDefault(y => Convert.ToInt32(y.RoleId) == Convert.ToInt32(lst3.Role)); //Get RoleID and RoleSequenceID

            if (catagory == "2")        //Senior Level
            {
                List<RoleDetail> roles = _dbContext.RoleDetails.Where(y => Convert.ToInt32(y.RoleSequenceId) < Convert.ToInt32(rd1.RoleSequenceId)).ToList();
                lst = GetUserIdData(roles, userID);

            }
            else if (catagory == "4")   //Peer Level
            {
                List<RoleDetail> roles = _dbContext.RoleDetails.Where(y => Convert.ToInt32(y.RoleSequenceId) == Convert.ToInt32(rd1.RoleSequenceId)).ToList();
                lst = GetUserIdData(roles, userID);

            }
            else if (catagory == "5")   //Junior Level
            {
                List<RoleDetail> roles = _dbContext.RoleDetails.Where(y => Convert.ToInt32(y.RoleSequenceId) > Convert.ToInt32(rd1.RoleSequenceId)).ToList();
                lst = GetUserIdData(roles, userID);

            }
            else if (catagory == "6")   //External Level
            {
                // How to get External user??
            }

            return lst;
        }


        private List<SelectListItem> GetUserIdData(List<RoleDetail> Roles, string CurrentuserID)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            foreach (RoleDetail rd3 in Roles)
            {
                List<UserDetail> lst4 = _dbContext.UserDetails.Where(x => Convert.ToInt32(x.Role) == Convert.ToInt32(rd3.RoleId)).ToList();  //Role
                foreach (UserDetail ud in lst4)
                {
                    if (CurrentuserID != ud.Id.ToString())
                    {
                        SelectListItem obj = new SelectListItem();
                        obj.Value = ud.Id.ToString();
                        obj.Text = ud.UserId;
                        lst.Add(obj);
                    }
                }

            }
            return lst;
        }

        public string SetFeedbackProviderList(FeedbackSchDtlViewModel fbsdvm)
        {
            string msg = string.Empty;

            try
            {
                var isScheduleViseVarsa = _dbContext.FeedbackScheduleDetails.Any(x => x.FbsuserId == fbsdvm.FBSProviderUSerID && x.FbsproviderUserId == fbsdvm.FBSUserID);

                if (isScheduleViseVarsa == false)
                {
                    FeedbackScheduleDetail feedbackSchDtlModel = new FeedbackScheduleDetail();

                    feedbackSchDtlModel.FbsuserId = fbsdvm.FBSUserID;
                    feedbackSchDtlModel.FbscategoryId = fbsdvm.FBSCategoryID;
                    feedbackSchDtlModel.FbsproviderUserId = fbsdvm.FBSProviderUSerID;
                    feedbackSchDtlModel.FbslastDate = fbsdvm.FBSLastDate;
                    feedbackSchDtlModel.FbsisActive = true;

                    Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<FeedbackScheduleDetail> entityEntry = _dbContext.FeedbackScheduleDetails.Add(feedbackSchDtlModel);
                    _dbContext.SaveChanges();

                    msg = "Success";
                }
                else
                {
                    msg = "Failed";
                }
            }
            catch (Exception ex)
            {
                msg = "Failed";
            }
            return msg;
        }
    }
}
