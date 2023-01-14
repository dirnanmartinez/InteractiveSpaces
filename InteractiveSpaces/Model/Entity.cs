using InteractiveSpaces.Model.EntityDTO;
using System.ComponentModel.DataAnnotations.Schema;

namespace InteractiveSpaces.Models;

public abstract class Entity 
{
    protected Entity()
    {
    }

    protected Entity(string name, string? description)
    {
        Name = name;
        Description = description;
    }

    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(100, ErrorMessageResourceName = "MessageErrorForName", MinimumLength = 5)]
    public string Name { get; set; }

    public string? Description { get; set; }

}

public class Entity3D : Entity
{
    public Entity3D(Entity3DBriefDTO entity):base(entity.Name, entity.Description)
    {
        Path = entity.Path;
        Animations = entity.Animations.Select(a=>new Animation(a.Name,a.AnimationId)).ToList();
    }

    public Entity3D()
    {
   
    }

    //unique id in the prefab
    public string Path { get; set; }

    [NotMapped]
    public int NumberOfAnimations{ get { return Animations == null ? 0 : Animations.Count; }  }

    public IList<Animation> Animations { get; set; }


}