using InteractiveSpaces.Model.ResourceDTO;
using InteractiveSpaces.Model.StepDTO;

namespace InteractiveSpaces.Model.ActivityDTO
{
    public class ActivityDetailsDTO : ActivityBriefDTO
    {
        public int? FirstStepId { get; set; }
        public ResourceDetailsDTO? ActivityImage { get; set; }
        public ResourceDetailsDTO? InitialHelp { get; set; }
        public ResourceDetailsDTO? FinalMessage { get; set; }
        public ActivityDetailsDTO(Activity activity) : base(activity)
        {
            FirstStepId = activity.FirstStep == null ? null : activity.FirstStep.Id;
            ActivityImage= activity.ActivityImage==null?null:new ResourceDetailsDTO(activity.ActivityImage);
            InitialHelp= activity.InitialHelp == null ? null : new ResourceDetailsDTO(activity.InitialHelp);
            FinalMessage= activity.FinalMessage == null ? null : new ResourceDetailsDTO(activity.FinalMessage);
        }
    }
}
