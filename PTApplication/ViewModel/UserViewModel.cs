using PTBusinessLogic;
using PTBusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PTApplication.ViewModel
{
    public class UserViewModel : ViewModelBase
    {
        public ICommand OnSaveNewUser { get; private set; } 
        public ICommand OnExitClicked { get; private set; }

        private RegisterUserDto dto;
        private FileManager fileManager;

        public UserViewModel()
        {
            dto = new RegisterUserDto();
            OnSaveNewUser = new RelayCommand(OnRegisterNewUser);
            OnExitClicked = new RelayCommand(ClickCancel);
            fileManager = new FileManager();
        }

        private void ClickCancel(object parameter)
        {
            if (parameter == null) return;

            if (parameter is not Window)
            {
                throw new ArgumentException("Not valid parameter passed into exit command");
            };

            Window window = (Window)parameter;
            window.Close();
        }

        private void OnRegisterNewUser(object obj)
        {
            fileManager.CreateUser(dto);
        }

        

       

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
