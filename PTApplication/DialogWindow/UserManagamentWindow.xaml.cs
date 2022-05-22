using PTApplication.ViewModel;
using PTBusinessLogic;
using PTBusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PTApplication.DialogWindow
{
    /// <summary>
    /// Logika interakcji dla klasy UserManagamentWindow.xaml
    /// </summary>
    public partial class UserManagamentWindow : Window
    {
        public UserManagamentWindow()
        {
            InitializeComponent();
            UsersViewModel model = new UsersViewModel();
            DataContext = model; 
        }

        private void DG1_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            UserDto user = new UserDto();
            int idOfNewUser = 1;

            using (FileManager manager = new FileManager())
            {
                idOfNewUser = manager.GetLastId() + 1;
            }

            user.Id = idOfNewUser;
            user.CreatedTime = DateTime.Now;
            user.UpdatedTime = DateTime.Now;
            user.IsActive = true;
            user.Ip = "127.0.0.1";

            GridUserViewModel model = new GridUserViewModel(user);

            e.NewItem = model;
        }

     

        private void DG1_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if ((e.EditAction == DataGridEditAction.Commit) == false) return;

            UserDto model = e.Row.DataContext as UserDto;

            using (FileManager manager = new FileManager())
            {
                if (manager.IsUserCanBeFound(model.Login))
                {
                    manager.UpdateUser(model);
                }
                else
                {
                    manager.CreateUser(model);
                }
            }
        }
    }
}
