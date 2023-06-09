using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.Observer;
using Sims2023.View;
using Sims2023.WPF.Views;
using Sims2023.WPF.Views.GuideViews;
using Sims2023.WPF.Views.Guest1Views;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using Sims2023.WPF.Views.Guest2Views;

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
            bool loggedIn = false;
            var users = _userService.GetAllUsers().ToList();
            foreach (var user in users)
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
                GuideView guideView = new(user);
                guideView.Show();
            }
        }

    }
}