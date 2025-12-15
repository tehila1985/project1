
using Dto;
using Model;

namespace Services
{
    public interface IPasswordService
    {
        DtoPassword_Password_Strength getStrengthByPassword(PassWord p);
    }
}