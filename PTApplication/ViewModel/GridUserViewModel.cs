using PTBusinessLogic;
using PTBusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTApplication.ViewModel
{
    public class GridUserViewModel : ViewModelBase
    {
        public GridUserViewModel()
        {
           dto = new UserDto();
        }

        public GridUserViewModel(UserDto dto)
        {
            this.dto = dto;
        }

        private UserDto dto;

        private void UpdateOrCreate()
        {
            using (var fileManager = new FileManager())
            {
                fileManager.UpdateOrCreate(dto);
            }
        }

        public string Login
        {
            get
            {
                return dto.Login;
            }
            set
            {
                if (value != null && dto.Login != value)
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
                if (value != null && dto.Password != value)
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
                if (value != null && dto.Ip != value)
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
                if (value != null && dto.IsActive != value)
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
                if (value != null && dto.IsLogged != value)
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
                if (value != null && dto.CreatedTime != value)
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
                if (value != null && dto.UpdatedTime != value)
                {
                    dto.UpdatedTime = value; NotifyPropertyChanged();
                    UpdateOrCreate();
                }
            }

        }

        public int Id
        {
            get { return dto.Id; }
            set
            {
                dto.Id = value; NotifyPropertyChanged();
                UpdateOrCreate();
            }
        }
    }
}
