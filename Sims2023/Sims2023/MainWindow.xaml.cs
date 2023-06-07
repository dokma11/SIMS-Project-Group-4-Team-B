using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.View;
using Sims2023.WPF.Views;
using Sims2023.WPF.Views.Guest1Views;
using Sims2023.WPF.Views.GuideViews;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sims2023
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _username;
        private string _password;
        private UserService _userService;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            _userService = new UserService();

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
            var user = _userService.GetAllUsers().FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null && user.AbleToLogIn)
            {
                OpenUserView(user);
                Close();
            }
            else if (user != null && !user.AbleToLogIn)
            {
                MessageBox.Show("Nakon datog otkaza više ne možete da se prijavite na svoj nalog!");
            }
            else
            {
                MessageBox.Show("Pogrešna kombinacija korisničkog imena i lozinke");
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
                GuideView guideView = new(user);
                guideView.Show();
            }
        }

    }
}