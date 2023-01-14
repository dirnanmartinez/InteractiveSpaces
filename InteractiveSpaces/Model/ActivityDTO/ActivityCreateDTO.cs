using InteractiveSpaces.Models.StepDTO;
using InteractiveSpaces.Models;
using Microsoft.EntityFrameworkCore;

namespace InteractiveSpaces.Model.ActivityDTO
{
    public class ActivityCreateDTO
    {

        [Required]
        [StringLength(100, ErrorMessageResourceName = "MessageErrorForName", MinimumLength = 5)]
        public string Name { get; set; }
        public string Description { get; set; }

        public string FinalMessageOK { get; set; }

        public string FinalMessageError { get; set; }

        [Precision(4, 2)]
        public decimal MaxTime { get; set; }


        public int? ActivityImagId { get; set; }
        public int? InitialHelpId { get; set; }
        public int? FinalMessageId { get; set; }

        [EmailAddress]
        public string Owner { get; set; }
    }
}
