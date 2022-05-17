using PTBusinessLogic.Model;
using PTDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTBusinessLogic.Service
{
    internal class DtoConverter
    {
        public User CreateUserEntity(RegisterUserDto registerUserDto)
        {
            User user = new User();
            
            user.Login = registerUserDto.Login;
            user.Password = registerUserDto.Password;
            user.IsLogged = false;

            user.CreatedTime = DateTime.Now;
            user.UpdatedTime = DateTime.Now;

            user.Ip = registerUserDto.Ip;
            return user;
        }

    }
}
