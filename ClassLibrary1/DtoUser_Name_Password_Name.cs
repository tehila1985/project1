using System.ComponentModel.DataAnnotations;

namespace Dto
{
    public class DtoUser_Name_Password_Gmail
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Gmail is required")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Gmail { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastname { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
