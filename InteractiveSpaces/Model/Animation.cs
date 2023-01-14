namespace InteractiveSpaces.Models;

public class Animation
{
    public Animation()
    {
    }

    public Animation(string name, string animationId)
    {
        Name = name;
        AnimationId = animationId;
    }

    [Key]
    public int Id { get; set; }


    [Required]
    [StringLength(100, ErrorMessageResourceName = "MessageErrorForName", MinimumLength = 5)]
    public string Name { get; set; }

    
    //id of the animation of the prefab
    public string AnimationId { get; set; }

    public Entity3D Entity { get; set; }
}
