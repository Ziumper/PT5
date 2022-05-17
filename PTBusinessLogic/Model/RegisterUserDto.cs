using PTDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTBusinessLogic.Model
{
    public class RegisterUserDto : UserDto
    {
        public string Password { get; set; }
    }
}
