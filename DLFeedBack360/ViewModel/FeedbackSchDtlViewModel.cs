using DLFeedBack360.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DLFeedBack360.ViewModel
{
    public class FeedbackSchDtlViewModel
    {
        public int FBSID { get; set; }
        public string FBSUserID { get; set; }
        public int FBSCategoryID { get; set; }
        public string FBSProviderUSerID { get; set; }
        public DateTime FBSLastDate { get; set; }
        public bool FBSIsActive { get; set; }
        public List<UserDetailViewModel> getUserIDDetail { get; set; }
        public List<FeedbackCatagoryViewModel> getFeedbackCatagoryDetail { get; set; }
        public List<SelectListItem> getProviderUserIDDetail { get; set; }

    }
}
