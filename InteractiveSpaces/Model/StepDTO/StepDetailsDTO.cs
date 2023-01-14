using InteractiveSpaces.Model.EntityStepDTO;
using InteractiveSpaces.Models;

namespace InteractiveSpaces.Model.StepDTO
{
    public class StepDetailsDTO
    {
        public StepDetailsDTO(int id, bool? groupal, bool? isSupervised,
            string interactiveSpaceName, IList<EntityStep> entitiesStep,
            int? previousStep,
            TypeOfStep typeNextStepRelation)
        {
            Id= id;
            Groupal = groupal;
            IsSupervised = isSupervised;
            InteractiveSpaceName = interactiveSpaceName;
            //StepDescriptions = entitiesStep.Select(e=> 
            //    new StepDescriptionDetailsDTO(e.Entity.Path, e.Feedback.Path, e.LocatedIn)).ToList();
            PreviousStep = previousStep;
            Type = typeNextStepRelation;
        }

        public StepDetailsDTO(Step step)
        {
            Id = step.Id;
            Groupal = step.Groupal;
            IsSupervised = step.IsSupervised;
            InteractiveSpaceName = step.InteractiveSpace.Name;
            PreviousStep = step.PreviousStep==null?null:step.PreviousStep.Id;
            Type = step.Type;
            StepDescriptions = step.StepDescriptions==null?new List<StepDescriptionDetailsDTO>()
                : step.StepDescriptions.Select(sd=>new StepDescriptionDetailsDTO(sd)).ToList();
            
        }

        public StepDetailsDTO()
        {
        }

        public int Id { get; set; }
        public bool? Groupal { get; set; }

        public bool? IsSupervised { get; set; }

        public string InteractiveSpaceName { get; set; }

        public IList<StepDescriptionDetailsDTO> StepDescriptions { get; set; }

        public string? FeedbackPath { get; set; }

        public int? PreviousStep { get; set; }

        public TypeOfStep Type { get; set; }

    }

    public class StepDescriptionDetailsDTO {
        public StepDescriptionDetailsDTO(StepDescription stepDescription)
        {
            Id = stepDescription.Id;
            Description = stepDescription.Description;
            Entities = stepDescription.EntityStep == null ? new List<EntityStepDetail3DLocationDTO>()
                : stepDescription.EntityStep.Select(es => new EntityStepDetail3DLocationDTO(es)).ToList();

        }

        public int Id { get; set; }
        public string Description { get; set; }

        public IList<EntityStepDetail3DLocationDTO> Entities { get; set; }

    }


}
