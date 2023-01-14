namespace InteractiveSpaces.Model.ActivityDTO
{
    public class ActivityBriefDTO
    {
        public ActivityBriefDTO(Activity activity)
        {
            Id = activity.Id;
            Name = activity.Name;
            Description = activity.Description;
            FinalMessageOK = activity.FinalMessageOK;
            FinalMessageError = activity.FinalMessageError;
            MaxTime = activity.MaxTime;
            Owner = activity.Owner;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }



        public string FinalMessageOK { get; set; }

        public string FinalMessageError { get; set; }

        [Precision(5, 2)]
        public decimal MaxTime { get; set; }

        [EmailAddress]
        public string Owner { get; set; }
    }
}
