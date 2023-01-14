namespace InteractiveSpaces.Model.Activity2UserDTOs
{
    public class Activity2UserDTO
    {
        [EmailAddress]
        public string User { get; set; }

        public int ActivityId { get; set; }
    }
}
