using PTBusinessLogic;
using PTBusinessLogic.Model;
using System;
using System.Windows;
using System.Windows.Input;

namespace PTApplication.ViewModel
{
    public class UserViewModel : ViewModelBase
    {
        public ICommand OnSaveNewUser { get; private set; } 
        public ICommand OnExitClicked { get; private set; }
        public ICommand OnLoginUser { get; private set; }

        private string password;
        private UserDto dto;

        public UserViewModel()
        {
            dto = new RegisterUserDto();
            OnSaveNewUser = new RelayCommand(OnRegisterNewUser);
            OnExitClicked = new RelayCommand(ClickCancel);
            OnLoginUser = new RelayCommand(LoginUser);
        }

        private void LoginUser(object param)
        {
            if (param == null) return;

            if (param is not Window)
            {
                throw new ArgumentException("Not valid parameter passed into exit command");
            };

            Window window = (Window)param;

            bool loginResult = false;
            using (var fileManager = new FileManager())
            {
                bool isCredentialsCorrect = fileManager.IsUserCredentialsCorrect(Login, Password);
                if (isCredentialsCorrect == false)
                {
                    MessageBox.Show("Credentials not correct! Change password");
                    window.DialogResult = false;
                    return;
                }

                loginResult = fileManager.LoginUser(Login);
            }
          
            window.DialogResult = loginResult;

            CloseWindow(param);
        }

        private void ClickCancel(object parameter)
        {
            CloseWindow(parameter);
        }

        private void CloseWindow(object parameter)
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
            Window window = GetWindowFromParam(obj);

            using (var fileManager = new FileManager())
            {
                RegisterUserDto registerUserDto = new RegisterUserDto();
                registerUserDto.Login = Login;
                registerUserDto.Password = password;

                if (fileManager.IsUserExisting(Login))
                {
                    fileManager.UpdateUser(registerUserDto);
                    window.DialogResult = true;
                    return;
                }
               
               
                registerUserDto.Login = Login;
                registerUserDto.Ip = "127.0.0.1";
                fileManager.CreateUser(registerUserDto);
                window.DialogResult = true;
            }

            CloseWindow(obj);
        }

        private Window GetWindowFromParam(object param)
        {
            if (param == null) return null;

            if (param is not Window)
            {
                throw new ArgumentException("Not valid parameter passed into exit command");
            };

            Window window = (Window)param;

            return window;
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
            get { return password; }
            set
            {
                if(value != null && password != value)
                {
                    password = value.ToString();
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
