using AutoMapper;
using Dto;
using Model;
using Model;

namespace Api
{
    public class AutoMapper:Profile
    {
        public AutoMapper() {
            CreateMap<User, DtoUser_Gmail_Password>().ReverseMap();
            CreateMap<User, DtoUser_Id_Name>().ReverseMap();
            CreateMap<PassWord, DtoPassword_Password_Strength>().ReverseMap();
            CreateMap<User, DtoUser_Name_Password_Gmail>().ReverseMap();
            CreateMap<User, DtoUser_Name_Gmail>().ReverseMap();
            CreateMap<Category,DtoCategory_Name_Id>().ReverseMap();
        }
    }
}
