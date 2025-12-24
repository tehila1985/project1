using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Dto
{
    //public record  DtoUser_Name_Gmail(int UserId,string Gmail,string UserFirstName,string UserLastname );
    public class DtoUser_Name_Gmail
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
    }

}

