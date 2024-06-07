using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLFeedBack360.ViewModel
{
    public class ForgotPasswordViewModel
    {
        public string UserID { get; set; }
        public string PassCode { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
