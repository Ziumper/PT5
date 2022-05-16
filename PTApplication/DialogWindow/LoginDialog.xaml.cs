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
    /// Logika interakcji dla klasy LoginDialog.xaml
    /// </summary>
    public partial class LoginDialog : Window
    {
        public LoginDialog()
        {
            InitializeComponent();
            DataContext = new UserViewModel();
        }

        public LoginDialog(UserViewModel userViewModel)
        {
            InitializeComponent();
            DataContext = userViewModel;
        }
    }
}
