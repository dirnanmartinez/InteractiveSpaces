using InteractiveSpaces.Model;
using InteractiveSpaces.Models.StepDTO;
using Microsoft.OpenApi.Extensions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using static InteractiveSpaces.Models.Step;

namespace InteractiveSpaces.Models;

public class Step
{
    public Step()
    {
        StepDescriptions = new List<StepDescription>();
    }

    public Step(StepCreateDTO stepDTO)
    {
        Groupal = stepDTO.Groupal;
        IsSupervised = stepDTO.IsSupervised;
        Type = stepDTO.TypeOfStep;
        StepDescriptions = stepDTO.StepDescriptions.Select(s=>new StepDescription(s, this)).ToList();
    }

    public Step(StepCreateDTO stepDTO, InteractiveSpace3D? interactiveSpace, 
            Step previousStep, Activity activity)
        :this(stepDTO)
    {
        InteractiveSpace = interactiveSpace;
        //EntityStep = entityStep;
        PreviousStep = previousStep;
        Activity = activity;
    }

    public Step(StepCreateDTO stepDTO, InteractiveSpace3D? interactiveSpace)
    : this(stepDTO)
    {
        InteractiveSpace = interactiveSpace;

    }


    [Key]
    public int Id { get; set; }

    [EnumDataType(typeof(TypeOfStep))]
    public TypeOfStep Type { get; set; }
   
    public IList<StepDescription> StepDescriptions { get; set; }
    
    public bool? Groupal { get; set; }

    public bool? IsSupervised { get; set; }

    public InteractiveSpace3D? InteractiveSpace { get; set; }

    public Step? PreviousStep { get; set; }

    public Step? NextStep { get; set; }

    public Activity Activity { get; set; }


    [NotMapped]
    public bool LastStep {

        get {
            return NextStep == null;
        }
    }


    [NotMapped]
    public bool FirstStep
    {

        get
        {
            return PreviousStep == null;
        }
    }

}

public enum TypeOfStep
{
   
    Mandatory,
    Optional,
    Alternative
}

