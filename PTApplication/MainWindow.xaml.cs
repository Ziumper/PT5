using PTApplication.DialogWindow;
using PTApplication.ViewModel;
using PTBusinessLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PTApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileExplorer fileExplorer;

        public MainWindow()
        {
            InitializeComponent();
            fileExplorer = new FileExplorer();
            DataContext = fileExplorer;
            fileExplorer.PropertyChanged += OnFileExplorerPropertyChanged;
            fileExplorer.OnOpenFileRequest += OnOpenFileRequest;

            if (fileExplorer.LoggedUser == null)
            {
                fileExplorer.LoggedUser = new UserViewModel();
                fileExplorer.LoggedUser.Login = FileManager.HostUserName;

                bool isPresent = false;
                bool isLogedIn = false;

                using (FileManager fileManager = new FileManager())
                {
                    isPresent = fileManager.FindIfCurrentUserPresent();
                    if (isPresent)
                    {
                        isLogedIn = fileManager.CheckIsUserLogedInCorrect();
                    }
                }

                if (isPresent && isLogedIn == false)
                {
                    
                    if(isLogedIn == false)
                    {
                        LoginDialog loginDialog = new LoginDialog(fileExplorer.LoggedUser);
                        loginDialog.ShowDialog();

                        if (loginDialog.DialogResult == true)
                        {
                            isLogedIn = true;
                        }
                    }
                }

                if(isLogedIn)
                {
                    return;
                }

                //register and login again
                RegistrationDialog dialog = new RegistrationDialog(fileExplorer.LoggedUser);
                var registrationDialogResult = dialog.ShowDialog();
                if(dialog.DialogResult == false)
                {
                    System.Windows.MessageBox.Show("Failed to update a user, closing application!");
                    this.Close();
                    return;
                }

                //login again
                LoginDialog loginDialogAfterRegistration = new LoginDialog(fileExplorer.LoggedUser);
                loginDialogAfterRegistration.ShowDialog();

                if (loginDialogAfterRegistration.DialogResult == true)
                {
                    isLogedIn = true;
                    return;
                }
                    
                this.Close();
                System.Windows.MessageBox.Show("Failed to login closed application");
            }
        }

        private void OnOpenFileRequest(object? sender, FileInfoViewModel e)
        {
            var content = fileExplorer.GetFileContent(e);
            if (content is string text)
            {
                FileTextBox.Text = text;
            }

        }

        private void OnExitItemMenuClick(object sender, RoutedEventArgs args)
        {
            Close();
        }

        private void OnFileExplorerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(FileExplorer.Lang))
                CultureResources.ChangeCulture(CultureInfo.CurrentUICulture);
        }
    }
}
