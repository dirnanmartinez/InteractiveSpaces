namespace InteractiveSpaces.Model.ResourceDTO
{
    public class ResourceDetailsDTO
    {
        public ResourceDetailsDTO(Resource resource)
        {
            Name = resource.Name;
            Type = resource.Type;
            Description = resource.Description;
            Size = resource.Size;
            Path = resource.Path;
        }

        [Required]
        [StringLength(100, ErrorMessage = "Name of the resource is required", MinimumLength = 5)]
        public string Name { get; set; }

        [EnumDataType(typeof(TypeOfResource))]
        public TypeOfResource Type { get; set; }

        public string? Description { get; set; }

        public int? Size { get; set; }

        public string? Path { get; set; }
    }
}
