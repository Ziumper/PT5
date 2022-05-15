using PTBusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTApplication.ViewModel
{
    public class RegisterUserVievModel : ViewModelBase
    {
        private RegisterUserDto dto;

        public string Login
        { 
            get
            {
                return dto.Login;
            }
            set 
            { 
                if(value != null && dto.Login != value)
                {
                    dto.Login = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Password
        {
            get { return dto.Password; }
            set
            {
                if(value != null && dto.Password != value)
                {
                    dto.Login = value.ToString();
                    NotifyPropertyChanged();
                }
            }
        }

        public string Ip
        {
            get 
            { 
                return dto.Ip; 
            }
            set 
            { 
                if(value != null && dto.Ip != value )
                dto.Ip = value; NotifyPropertyChanged(); 
            }
        }        
    }
}
