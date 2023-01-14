using InteractiveSpaces.Model.Activity2UserDTOs;
namespace InteractiveSpaces.Model.Activity2UserDTOs
{
    public class Activity2UserDetailsDTO
    {
        public Activity2UserDetailsDTO(string user, Activity activity)
        {
            User = user;
            ActivityId=activity.Id;
            Name = activity.Name;
            Description = activity.Description;
            CreationDate = activity.CreationDate;
            FinalMessageOK = activity.FinalMessageOK;
            FinalMessageError = activity.FinalMessageError;
            MaxTime = activity.MaxTime;
            Owner = activity.Owner;
        }
        public int ActivityId{ get; set; }

        [EmailAddress]
        public string User { get; set; }
        [Required]
        [StringLength(100, ErrorMessageResourceName = "MessageErrorForName", MinimumLength = 5)]
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public string FinalMessageOK { get; set; }

        public string FinalMessageError { get; set; }

        [Precision(4, 2)]
        public decimal MaxTime { get; set; }

        [EmailAddress]
        public string Owner { get; set; }
    }
}
