using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.OwnerViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Sims2023.WPF.Views.OwnerViews
{
    /// <summary>
    /// Interaction logic for StartView.xaml
    /// </summary>
    public partial class StartView : Page, INotifyPropertyChanged
    {
        public User user { get; set; }
        public string welcomeString { get; set; }
        public StartView(User owner)
        {
            this.DataContext = this;
          
            InitializeComponent();

            this.user = owner;

            welcomeString = welcome_string(user);
        }

        public string welcome_string(User user)
        {
            var message = "Dobrodošli " + user.Name + " " + user.Surname;
            return message;
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
