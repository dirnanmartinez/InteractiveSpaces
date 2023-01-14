using System.ComponentModel.DataAnnotations.Schema;

namespace InteractiveSpaces.Models;

public class ActionEntityStep
{
    public ActionEntityStep()
    {
    }

    public ActionEntityStep(string? description, string? feedbackMessage, TypeOfActionEntityStep actionType, Animation animation)
    {
        Description = description;
        FeedbackMessage = feedbackMessage;
        ActionType = actionType;
        Animation = animation;
    }

    [Key]
    public int Id { get; set; }

    public string? Description { get; set; }
    public string? FeedbackMessage { get; set; }

    [EnumDataType(typeof(TypeOfActionEntityStep))]
    public TypeOfActionEntityStep ActionType { get; set; }

    public int EntityStepId { get; set; }

    [ForeignKey("EntityStepId")]
    public EntityStep EntityStep { get; set; }

    public int AnimationId { get; set; }
    [ForeignKey("AnimationId")]
    public Animation? Animation { get; set; }
    
}

public enum TypeOfActionEntityStep
{
    Required,
    Optional,
    Wrong,
}
