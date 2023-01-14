using System.ComponentModel.DataAnnotations.Schema;

namespace InteractiveSpaces.Models;

public class EntityStep {
    public EntityStep()
    {
    }

    public EntityStep(StepDescription stepDescription, Entity3D entity, Location3D locatedIn, IList<ActionEntityStep>? hasActions)
    {
        StepDescription = stepDescription;
        Entity = entity;
        LocatedIn = locatedIn;
        HasActions = hasActions;
    }

    [Key]
    public int Id { get; set; }

    public int StepDescriptionId { get; set; }
    [ForeignKey("StepDescriptionId")]
    public StepDescription StepDescription { get; set; }

    public int EntityId { get; set; }
    [ForeignKey("EntityId")]
    public Entity3D Entity { get; set; }


    public Location LocatedIn  { get; set; }
    public IList<ActionEntityStep>? HasActions{ get; set; }

}