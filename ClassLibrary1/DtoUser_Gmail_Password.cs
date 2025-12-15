using System.ComponentModel.DataAnnotations;


namespace Dto
{
    //public record DtoUser_Gmail_Password(string Gmail,string Password);
    public class DtoUser_Gmail_Password
    {
        public string Gmail { get; set; }
         public string Password { get; set; }
    }
}
