using DLFeedBack360.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLFeedBack360.Infrastructure.Abstract
{
    public interface IProvideFeedbackRepo
    {
        List<CandidateListViewModel> GetCandidateList(int ProviderID);
        FeedbackProvidingViewModel getFeedbackQuestion();
        string getIDtoUserID(string id);
        int getUserIDtoID(string userID);
        List<FeedbackResultViewModel> GetFeedbackResult();
        string SetFeedbackRatings(FeedbackProvidingViewModel model, int ToID, int ByID);

    }
}
