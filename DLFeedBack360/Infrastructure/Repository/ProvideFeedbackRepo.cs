using DLFeedBack360.Infrastructure.Abstract;
using DLFeedBack360.Models;
using DLFeedBack360.ViewModel;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLFeedBack360.Infrastructure.Repository
{
    public class ProvideFeedbackRepo : IProvideFeedbackRepo
    {
        ProjectFB360Context _dbContext = new ProjectFB360Context();

        public List<CandidateListViewModel> GetCandidateList(int ProviderID)
        {
            List<CandidateListViewModel> candidateListViewModels = new List<CandidateListViewModel>();
            try
            {
                // Get list of scheduled feedback for current user
                List<FeedbackScheduleDetail> feedbackSchDtlModels = _dbContext.FeedbackScheduleDetails.Where(c => Convert.ToInt32(c.FbsproviderUserId) == Convert.ToInt32(ProviderID)).ToList();
                int i = 1;
                foreach (FeedbackScheduleDetail feedbackSch in feedbackSchDtlModels)
                {
                    //IsRatingProvided :- Check is rating already provided for ToID.
                    List<FeedbackRating> IsRatingProvided = _dbContext.FeedbackRatings.Where(s => Convert.ToInt32(s.ToId) == Convert.ToInt32(feedbackSch.FbsuserId) && Convert.ToInt32(s.ById) == Convert.ToInt32(feedbackSch.FbsproviderUserId)).ToList();

                    if (IsRatingProvided.Count == 0)
                    {
                        CandidateListViewModel clvm = new CandidateListViewModel();

                        var selectResult = from aa in _dbContext.UserDetails.Where(s => Convert.ToInt32(s.Id) == Convert.ToInt32(feedbackSch.FbsuserId)) select new { UserID = aa.UserId, Name = aa.Name };
                        foreach (var item in selectResult)
                        {
                            clvm.CandidateUserID = item.UserID;
                            clvm.CandidateName = item.Name;
                        }
                        clvm.SLNo = i;
                        clvm.Action = "Provide Feedback";
                        i++;
                        candidateListViewModels.Add(clvm);
                    }
                }

            }
            catch (Exception)
            {

            }
            return candidateListViewModels;
        }

        public FeedbackProvidingViewModel getFeedbackQuestion()
        {
            FeedbackProvidingViewModel lstModel = new FeedbackProvidingViewModel();

            lstModel.getFeedbackQuestions = GetQuestions();
            lstModel.getFbRatings = GetRating();

            return lstModel;
        }

        private List<FeedbackQuesViewModel> GetQuestions()
        {
            List<FeedbackQuesViewModel> lstQues = new List<FeedbackQuesViewModel>();

            List<FeedbackQuestion> feedbackQues = _dbContext.FeedbackQuestions.ToList();

            foreach (FeedbackQuestion FBQues in feedbackQues)
            {
                FeedbackQuesViewModel QuesVM = new FeedbackQuesViewModel();

                QuesVM.QID = FBQues.Qid;
                QuesVM.QDescription = FBQues.Qdescription;
                lstQues.Add(QuesVM);
            }

            return lstQues;
        }
        private List<FeedbackRatingsViewModel> GetRating()
        {
            List<FeedbackRatingsViewModel> lst = new List<FeedbackRatingsViewModel>();
            for (int i = 1; i <= 5; i++)
            {
                FeedbackRatingsViewModel obj = new FeedbackRatingsViewModel();
                obj.RatingID = i;
                obj.Rating = i;
                lst.Add(obj);
            }
            return lst;
        }

        public string SetFeedbackRatings(FeedbackProvidingViewModel model, int ToID, int ByID)
        {
            string msg = string.Empty;

            try
            {
                for (int i = 0; i < model.getFeedbackQuestions.Count; i++)
                {
                    FeedbackRating FBRatingModel = new FeedbackRating();

                    FBRatingModel.Frid = ToID;
                    FBRatingModel.ById = ByID;
                    FBRatingModel.Qid = model.getFeedbackQuestions[i].QID;
                    FBRatingModel.Rating = model.getFbRatings[i].Rating;
                    FBRatingModel.CreatedDate = System.DateTime.Now;

                    _dbContext.FeedbackRatings.Add(FBRatingModel);
                    _dbContext.SaveChanges();
                }
                msg = "Success";
            }
            catch (Exception)
            {
                msg = "Failed";
            }

            return msg;
        }

        public List<FeedbackResultViewModel> GetFeedbackResult()
        {
            List<FeedbackResultViewModel> FRVM = new List<FeedbackResultViewModel>();
            int count = 1;
            var query = from u in _dbContext.UserDetails
                        join r in _dbContext.FeedbackRatings on u.Id equals r.ToId
                        group r by new { r.ToId, u.UserId, u.Name, u.EmpId } into g
                        orderby g.Average(x => x.Rating) descending
                        select new
                        {
                            AverageRating = g.Average(x => x.Rating),
                            g.Key.UserId,
                            g.Key.Name,
                            g.Key.EmpId,
                            g.Key.ToId
                        };

            var result = query.ToList();
            foreach (var r in result)
            {
                FeedbackResultViewModel VM = new FeedbackResultViewModel();
                VM.SLNo = count++;
                VM.CandidateUserID = r.UserId;
                VM.CandidateName = r.Name;
                VM.CandidateEmpID = r.EmpId;
                VM.AvgRating = r.AverageRating;
                FRVM.Add(VM);
            }
            return FRVM;
        }
        public string getIDtoUserID(string id)
        {
            UserDetail ud = _dbContext.UserDetails.FirstOrDefault(x => Convert.ToInt32(x.Id) == Convert.ToInt32(id));  //Get UserID

            return ud.UserId.ToString();
        }

        public int getUserIDtoID(string userID)
        {
            UserDetail ud = _dbContext.UserDetails.FirstOrDefault(x => x.UserId == userID);  //Get ID

            return ud.Id;
        }
    }
}
