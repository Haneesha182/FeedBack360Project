using DLFeedBack360.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DLFeedBack360.Infrastructure.Abstract
{
        public interface IUserManagementRepo
        {
            List<CountryViewModel> GetCountries();
            List<LanguageViewModel> GetLanguages();
            List<RoleDetailViewModel> GetRoles();
            string GetRole(string userID);
            string SetRegistration(UserDetailViewModel uservm);
            string UpdatePassword(ChangePasswordViewModel cpv);
            string UserLogin(LoginViewModel lvm);
            string SendOPT(string emailid);
            string UserForgotPassword(ForgotPasswordViewModel fpvm);
            List<UserDetailViewModel> GetUserID();
            List<SelectListItem> GetProviderUserID();
            List<FeedbackCatagoryViewModel> GetFeedbackcatagory();
            List<SelectListItem> GetFeedbackProviderList(string catagory, string userID);
            string SetFeedbackProviderList(FeedbackSchDtlViewModel fbsdvm);
        }
    }
