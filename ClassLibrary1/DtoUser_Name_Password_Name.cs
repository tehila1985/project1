using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Dto
{
    //public record DtoUser_Name_Password_Gmail(int UserId,string Gmail, string UserFirstName,string UserLastname,string Password);

    public class DtoUser_Name_Password_Gmail 
    {
        public int UserId { get; set; }
        public string Gmail { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastname { get; set; }
        public string Password { get; set; }
    }
}