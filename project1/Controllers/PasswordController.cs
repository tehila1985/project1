using Dto;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        IPasswordService _p;
        public PasswordController(IPasswordService ip)
        {
            _p= ip;
        }
        
        //// GET api/<PasswordController>/5
        //[HttpGet("{id}")]
        //public DtoPassword_Password_Strength Get(PassWord p)
        //{
        //    return _p.getStrengthByPassword(p);
        //}

        [HttpPost]
        
        public DtoPassword_Password_Strength Post([FromBody] PassWord p)
        {
            return _p.getStrengthByPassword(p);
        }


     
     
    }
}
