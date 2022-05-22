using PTBusinessLogic;
using PTBusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTApplication.ViewModel
{
    public class UsersViewModel : ViewModelBase
    {
        private DispatchedObservableCollection<UserViewModel> users;

        public DispatchedObservableCollection<UserViewModel> Users
        {
            get
            {
                return users;
            }
            set 
            {
                if(users != value || users != null)
                {
                    users = value;
                    NotifyPropertyChanged();
                }
            }
        }


        public UsersViewModel()
        {
            using (FileManager manager = new FileManager())
            {
                DispatchedObservableCollection<UserViewModel> usersViewModel = new DispatchedObservableCollection<UserViewModel>();
                var users = manager.GetUsers();
                foreach (var user in users)
                {
                    UserViewModel viewModel = new UserViewModel(user);
                    usersViewModel.Add(viewModel);
                }

                Users = usersViewModel;
            }

            Users.CollectionChanged += OnCollectionChanged;
        }

        private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            Debug.WriteLine("Hello ther!");
        }
    }
}
