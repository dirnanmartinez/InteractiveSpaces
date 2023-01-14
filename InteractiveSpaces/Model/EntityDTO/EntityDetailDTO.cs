using InteractiveSpaces.Models;
using System.IO;
using System.Xml.Linq;

namespace InteractiveSpaces.Model.EntityDTO
{
    public class EntityDetail3DDTO
    {
        public EntityDetail3DDTO(Entity3D entity3D)
        {
            Name = entity3D.Name;
            Description = entity3D.Description;
            Path = entity3D.Path;
            Animations = entity3D.Animations.Select(a => new AnimationBriefDTO(a)).ToList();
            Id = entity3D.Id;
        }

        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "You must provide a name for the Entity", MinimumLength = 5)]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "You must provide a description for the Entity", MinimumLength = 5)]
        public string? Path { get; set; }

        public virtual IList<AnimationBriefDTO> Animations { get; set; }
    }



}
