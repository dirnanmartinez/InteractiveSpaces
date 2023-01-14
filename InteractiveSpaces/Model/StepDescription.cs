namespace InteractiveSpaces.Models
{

    public class StepDescription
    {
        public StepDescription()
        {
        }

        public StepDescription(string description, Step step)
        {

            Description = description;
            Step = step;
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage ="You must provide a description for this step", MinimumLength = 5)]
        public string Description { get; set; }
        public IList<EntityStep>? EntityStep { get; set; }
        public Step Step { get; set; }  

    }
}
