using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLFeedBack360.ViewModel
{
    public class FeedbackProvidingViewModel
    {
        public int QID { get; set; }
        public string QDescription { get; set; }
        public string FbRating { get; set; }
        public List<FeedbackQuesViewModel> getFeedbackQuestions { get; set; }
        public List<FeedbackRatingsViewModel> getFbRatings { get; set; }
    }
}
