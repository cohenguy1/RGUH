using RGUH.Enums;
using RGUH.Logic;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RGUH
{
    public partial class Game : System.Web.UI.Page
    {
        private void UpdateImages(CandidateState candidateState)
        {
            ImageInterview.Visible = (candidateState == CandidateState.Interview || candidateState == CandidateState.PostInterview);
            LabelInterviewing.Visible = (candidateState == CandidateState.Interview || candidateState == CandidateState.PostInterview); ;
            MovingToNextPositionLabel.Visible = false;
            MovingJobTitleLabel.Visible = false;
        }

        private void ClearInterviewImages()
        {
            ImageInterview.Visible = false;
            LabelInterviewing.Visible = false;
        }

        private void ShowInterviewImages()
        {
            ImageInterview.Visible = true;
            LabelInterviewing.Visible = true;
        }
    }
}