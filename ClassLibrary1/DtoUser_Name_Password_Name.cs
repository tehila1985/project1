using System.ComponentModel.DataAnnotations;

namespace Dto
{
    public class DtoUser_Name_Password_Gmail
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Gmail is required")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Gmail { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 ")]
        public string UserFirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50")]
        public string UserLastname { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6  and cannot exceed 100 characters.")]
        public string Password { get; set; }
    }
}
