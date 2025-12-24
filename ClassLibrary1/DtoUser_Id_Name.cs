using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Dto
{
    //public record DtoUser_Id_Name(string? UserId,string UserFirstName);

    public class DtoUser_Id_Name
    {
        public string? UserId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 ")]
        public string UserFirstName { get; set; }
    }


}
