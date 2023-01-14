using InteractiveSpaces.Models;

namespace InteractiveSpaces.Models.StepDTO
{
    public class StepCreateDTO
    {
        public StepCreateDTO()
        {
            StepDescriptions = new List<string>();
            Groupal = false;
            IsSupervised= false;
            TypeOfStep = TypeOfStep.Mandatory;
        }

        public bool? Groupal { get; set; }

        public bool? IsSupervised { get; set; }

        [Range(1, int.MaxValue)]
        public int? InteractiveSpace3DId { get; set; }

        [Range(1, int.MaxValue)]
        public int? ActivityId { get; set; }

        [Range(1, int.MaxValue)]
        public int? NextStep { get; set; }

        //the type of step
        [EnumDataType(typeof(TypeOfStep))]
        public TypeOfStep TypeOfStep { get; set; }

        public IList<string> StepDescriptions { get; set; }
 
    }

}
