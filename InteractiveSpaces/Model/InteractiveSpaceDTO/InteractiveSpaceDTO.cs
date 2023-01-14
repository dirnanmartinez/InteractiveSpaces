using InteractiveSpaces.Models;

namespace InteractiveSpaces.Model.InteractiveSpaceDTO
{
    public class InteractiveSpace3DBriefDTO
    {
        public InteractiveSpace3DBriefDTO()
        {
        }

        public InteractiveSpace3DBriefDTO(InteractiveSpace3D interactive)
        {
            Name = interactive.Name;
            Description = interactive.Description;
            Visibility = interactive.Visibility;
            AnchorId = interactive.AnchorId;
            Owner = interactive.Owner;
        }

        [Required]
        [StringLength(100, ErrorMessageResourceName = "MessageErrorForName", MinimumLength = 5)]
        public string Name { get; set; }

        public string? Description { get; set; }

        public TypeOfVisibility Visibility { get; set; }
        public string? AnchorId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage ="You must provide an owner to register an Interactive Space", MinimumLength = 5)]
        public string Owner { get; set; }
    }

    public class InteractiveSpaceDetail3DDTO: InteractiveSpace3DBriefDTO
    {
        public InteractiveSpaceDetail3DDTO(InteractiveSpace3D interactive) : base(interactive)
        {
            Id = interactive.Id;
        }

        public int Id { get; set; }
    }
    }
