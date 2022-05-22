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

        private UserDto dto;

        public UserViewModel()
        {
            dto = new UserDto();
            BindCommands();
          
        }

        public UserViewModel(UserDto userDto)
        {
            this.dto = userDto;
            BindCommands();
        }

        private void BindCommands()
        {
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
                UserDto registerUserDto = new UserDto();
                registerUserDto.Login = Login;
                registerUserDto.Password = dto.Password;

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

        private void UpdateOrCreate()
        {
            //using (var fileManager = new FileManager())
            //{
            //    fileManager.UpdateOrCreate(dto);
            //}
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
                    UpdateOrCreate();
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
                    dto.Password = value.ToString();
                    NotifyPropertyChanged();
                    UpdateOrCreate();
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
                UpdateOrCreate();
            }
        }
        
        public bool IsActive
        {
            get
            {
                return dto.IsActive;
            }
            set
            {
                if(value != null && dto.IsActive != value)
                {
                    dto.IsActive = value; NotifyPropertyChanged();
                    UpdateOrCreate();
                }
            }
        }

        public bool IsLogged
        {
            get
            {
                return dto.IsLogged;
            }
            set
            {
                if(value!=null && dto.IsLogged != value)
                {
                    dto.IsLogged = value; NotifyPropertyChanged();
                    UpdateOrCreate();
                }
            }
        }

        public DateTime CreatedTime
        {
            get
            {
                return dto.CreatedTime;
            }
            set
            {
                if(value != null && dto.CreatedTime != value)
                {
                    dto.CreatedTime = value; NotifyPropertyChanged();
                    UpdateOrCreate();
                }
            }
        }

        public DateTime UpdatedTime
        {
            get
            {
                return dto.UpdatedTime;
            }
            set
            {
                if(value != null && dto.UpdatedTime != value)
                {
                    dto.UpdatedTime = value; NotifyPropertyChanged();
                    UpdateOrCreate();
                }
             }

        }

        public int Id
        {
            get { return dto.Id; }
            set { dto.Id = value; NotifyPropertyChanged();
                UpdateOrCreate();
            }
        }
        
    }
}
