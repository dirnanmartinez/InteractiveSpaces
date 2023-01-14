namespace InteractiveSpaces.Model.EntityDTO
{
    public class Entity3DBriefDTO
    {
        public Entity3DBriefDTO()
        {
        }

        public Entity3DBriefDTO(string name, string? description, string? path)
        {
            Name = name;
            Description = description;
            Path = path;
        }

        public Entity3DBriefDTO(string name, string? description, string? path, IList<AnimationBriefDTO> animations)
        {
            Name = name;
            Description = description;
            Path = path;
            Animations = animations;
        }

        [Required]
        [StringLength(100, ErrorMessage ="You must provide a name for the Entity", MinimumLength = 5)]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "You must provide a description for the Entity", MinimumLength = 5)]
        public string? Path { get; set; }

        public IList<AnimationBriefDTO> Animations { get; set; }

    }

    public class AnimationBriefDTO
    {

        public AnimationBriefDTO()
        {
        }

        public AnimationBriefDTO(Animation animation):this(animation.Name, animation.AnimationId)
        {
            
        }

        public AnimationBriefDTO(string name, string animationId)
        {
            Name = name;
            AnimationId = animationId;
        }

        [Required]
        [StringLength(100, ErrorMessage ="You must provide a name for the animation", MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "You must provide the id of the animation", MinimumLength = 5)]
        //id of the animation of the prefab
        public string AnimationId { get; set; }
    }

}

