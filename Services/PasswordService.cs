using AutoMapper;
using Model;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PasswordService : IPasswordService
    {
        IMapper mapper;
        public PasswordService(IMapper mapperr)
        {
            mapper = mapperr;
        }

        public DtoPassword_Password_Strength getStrengthByPassword(PassWord p)
        {

            var result = Zxcvbn.Core.EvaluatePassword(p.Password);
            p.Strength = result.Score;
            var DtoPassword = mapper.Map<PassWord, DtoPassword_Password_Strength>(p);
            return DtoPassword;
        }
    }
}