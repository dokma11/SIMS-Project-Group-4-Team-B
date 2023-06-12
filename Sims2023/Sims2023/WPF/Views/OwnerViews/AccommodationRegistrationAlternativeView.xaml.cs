using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.OwnerViewModel;
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

namespace Sims2023.WPF.Views.OwnerViews
{
    /// <summary>
    /// Interaction logic for AccommodationRegistrationAlternativeView.xaml
    /// </summary>
    public partial class AccommodationRegistrationAlternativeView : Page
    {
        public AccommodationRegistrationAlternativeViewModel AccommodationRegistrationViewModel;
        public AccommodationRegistrationAlternativeView(User owner, string city, string country)
        {
            InitializeComponent();
            DataContext = new AccommodationRegistrationAlternativeViewModel(this, owner, city, country);
        }
    }
}