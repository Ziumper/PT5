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
        public User CreateUserEntity(UserDto registerUserDto)
        {
            User user = new User();
            
            user.Login = registerUserDto.Login;
            user.Password = registerUserDto.Password;
            user.IsLogged = false;

            user.CreatedTime = DateTime.Now;
            user.UpdatedTime = DateTime.Now;
            user.IsActive = true;
            user.Ip = registerUserDto.Ip;
            return user;
        }

        internal List<UserDto> CreateUserDto(List<User> users)
        {
            List<UserDto> result = new List<UserDto>();
            foreach(User user in users)
            {
                result.Add(FromUser(user));
            }

            return result;
        }

        private UserDto FromUser(User user)
        {
            UserDto userDto = new UserDto();
            userDto.Login = user.Login;
            userDto.Password = user.Password;
            userDto.Ip = user.Ip;
            userDto.IsLogged = user.IsLogged;
            userDto.CreatedTime = user.CreatedTime;
            userDto.UpdatedTime = user.UpdatedTime;
            userDto.Id = user.Id;
            userDto.IsActive = user.IsActive;
            return userDto;
           
        }
    }
}
