using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLFeedBack360.ViewModel
{
    public class FeedbackResultViewModel
    {
        public int SLNo { get; set; }
        public string CandidateUserID { get; set; }
        public string CandidateName { get; set; }
        public int CandidateEmpID { get; set; }
        public Double AvgRating { get; set; }
    }
}
