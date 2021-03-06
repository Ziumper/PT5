using PTApplication.ViewModel;
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
    /// Logika interakcji dla klasy RegistrationDialog.xaml
    /// </summary>
    public partial class RegistrationDialog : Window
    {
        public RegistrationDialog()
        {
            InitializeComponent();
            UserViewModel userViewModel = new UserViewModel();
            DataContext = userViewModel;
        }

        public RegistrationDialog(UserViewModel userViewModel)
        {
            InitializeComponent();
            DataContext = userViewModel;
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            UserViewModel userModel = (UserViewModel)DataContext;
            userModel.Password = passwordBox.Password;
        }

        
    }
}
