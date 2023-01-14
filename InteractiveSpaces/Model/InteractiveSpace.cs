using InteractiveSpaces.Model.InteractiveSpaceDTO;

namespace InteractiveSpaces.Models;

public enum TypeOfVisibility
{ 
    Public,
    Private,
}


public class InteractiveSpace
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100,ErrorMessageResourceName = "MessageErrorForName", MinimumLength =5)]
    public string Name { get; set; }

    public string?  Description { get; set; }

    public TypeOfVisibility  Visibility { get; set; }

    public string Owner { get; set; }
}

public class InteractiveSpace3D : InteractiveSpace
{
    public InteractiveSpace3D()
    {
    }

    public InteractiveSpace3D(InteractiveSpace3DBriefDTO interactiveSpace)
    {
        Name = interactiveSpace.Name;
        Description = interactiveSpace.Description;
        Visibility = interactiveSpace.Visibility;
        AnchorId = interactiveSpace.AnchorId;
        Owner = interactiveSpace.Owner;
    }

    public string?  AnchorId { get; set; }

}