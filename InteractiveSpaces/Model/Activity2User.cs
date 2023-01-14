
namespace InteractiveSpaces.Models;
public class Activity2User
{

        [EmailAddress]
        public string User { get; set; }
        public Activity Activity{get;set;}

        public int ActivityId { get; set; }

}

