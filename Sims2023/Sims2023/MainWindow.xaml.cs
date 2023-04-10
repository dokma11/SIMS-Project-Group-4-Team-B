using Sims2023.Controller;
using Sims2023.Model;
using Sims2023.Observer;
using Sims2023.View;
using Sims2023.WPF.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sims2023
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IObserver
    {
        private string _username;
        private string _password;
        private UserController _userController;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            _userController = new UserController();
            _userController.Subscribe(this);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(UsernameTextBox.Text) && !String.IsNullOrEmpty(PasswordBox.Password))
            {
                _username = UsernameTextBox.Text;
                _password = PasswordBox.Password;
                GetUser(_username, _password);
            }
            else
            {
                MessageBox.Show("Molimo Vas da popunite sva polja");
            }
        }

        private void GetUser(String username, String password)
        {
            bool loggedIn = false;
            foreach (var user in _userController.GetAllUsers())
            {
                if (user.Username == username && user.Password == password)
                {
                    OpenUserView(user);
                    loggedIn = true;
                    Close();
                }
            }
            if (!loggedIn)
            {
                MessageBox.Show("Data kombinacija ne postoji");
                UsernameTextBox.Text = "";
                PasswordBox.Password = "";
            }
        }

        private void OpenUserView(User user)
        {
            if (user.UserType == User.Type.Guest1)
            {
                Guest1MainView guest1MainView = new(user);
                guest1MainView.Show();
            }
            else if (user.UserType == User.Type.Guest2)
            {
                Guest2View guest2View = new(user);
                guest2View.Show();
            }
            else if (user.UserType == User.Type.Owner)
            {
                OwnerView ownerView = new(user);
                ownerView.Show();
            }
            else if (user.UserType == User.Type.Guide)
            {
                GuideViewModel guideView = new(user);
                guideView.Show();
            }
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}