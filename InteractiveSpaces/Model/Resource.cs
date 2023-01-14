using InteractiveSpaces.Model.ResourceDTO;

namespace InteractiveSpaces.Models;

public class Resource
{
    public Resource()
    {
    }

    public Resource(ResourceDetailsDTO resource)
    {

        Name = resource.Name;
        Type = resource.Type;
        Description = resource.Description;
        Size = resource.Size;
        Path = resource.Path;
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Name of the resource is required", MinimumLength = 5)]
    public string Name { get; set; }

    public TypeOfResource Type { get; set; } 

    public string? Description { get; set; } 

    public int? Size { get; set; }

    public string? Path { get; set; }

}

public enum TypeOfResource
{ 
    Text,
    Image,
    Audio,
    Video,
}