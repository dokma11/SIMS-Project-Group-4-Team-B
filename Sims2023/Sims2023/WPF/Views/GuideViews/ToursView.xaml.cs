//using Nevron.Nov.UI;
using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.GuideViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Sims2023.WPF.Views.GuideViews
{
    /// <summary>
    /// Interaction logic for ToursView.xaml
    /// </summary>
    public partial class ToursView : Page
    {
        private TourService _tourService;
        private LocationService _locationService;
        private KeyPointService _keyPointService;
        private UserService _userService;
        private TourReservationService _tourReservationService;
        private TourReviewService _tourReviewService;
        private VoucherService _voucherService;
        private RequestService _requestService;
        private CountriesAndCitiesService _countriesAndCitiesService;
        private TourNotificationService _tourNotificationService;
        private ComplexTourRequestService _complexTourRequestService;
        private SubTourRequestService _subTourRequestService;

        public ToursViewModel ToursViewModel;
        public User LoggedInGuide { get; set; }

        public ToursView(TourService tourService, TourReviewService tourReviewService, TourReservationService tourReservationService, KeyPointService keyPointService, LocationService locationService, VoucherService voucherService, UserService userService, User loggedInGuide, CountriesAndCitiesService countriesAndCitiesService, RequestService requestService, TourNotificationService tourNotificationService, ComplexTourRequestService complexTourRequestService, SubTourRequestService subTourRequestService)
        {
            InitializeComponent();

            _tourService = tourService;
            _locationService = locationService;
            _keyPointService = keyPointService;
            _tourReservationService = tourReservationService;
            _tourReviewService = tourReviewService;
            _voucherService = voucherService;
            _userService = userService;
            _countriesAndCitiesService = countriesAndCitiesService;
            _requestService = requestService;
            _complexTourRequestService = complexTourRequestService;
            _subTourRequestService = subTourRequestService;

            LoggedInGuide = loggedInGuide;

            ToursViewModel = new ToursViewModel(tourService, locationService, keyPointService, tourReviewService, requestService, tourReservationService, voucherService, userService, countriesAndCitiesService, loggedInGuide, tourNotificationService, complexTourRequestService, subTourRequestService);
            DataContext = ToursViewModel;

            countryComboBox.ItemsSource = ToursViewModel.GetCitiesAndCountries();
            countryComboBox.DisplayMemberPath = "CountryName";
            countryComboBox.SelectedValuePath = "CountryName";

            _tourNotificationService = tourNotificationService;
        }

        private void IntegerUpDown_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out _))
            {
                e.Handled = true;
            }
        }

        private void TabControl_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {
                toolTipButton.ToolTip = "Dobrodošli na stranicu ,,Ture\"!\n\n"
                + "U tabeli u sredini možete videti listu svih predstojećih tura i njihove informacije. \n"
                + "Da biste videli ključne tačke ture, morate da odaberetu željenu turu, nakon čega će Vam ispod nje biti izlistane sve ključne tačke\n\n"
                + "Dole, sa Vaše desne strane, Vam se nalaze dva dugmeta: \n"
                + "• ,,Započni turu\": kada odaberete turu koju želite da započnete(mora biti današnja), potom pritisnete na ovo dugme,\n"
                + "bićete preusmereni na prozor ,,Praćenje ture uživo\" preko kojeg ćete pratiti Vašu turu i vršiti željene evidencije vezane za nju\n"
                + "• ,,Otkaži turu\": kada odaberete turu koju želite da otkažete(mora biti 48 sati unapred), potom pritisnete na ovo dugme,\n"
                + "izabrana tura će biti otkazana i samim time uklonjena iz liste. Svi gosti koji su rezervisali mesto na toj novootkazanoj turi\n"
                + "dobiće po jedan vaučer koji će kasnije moći da iskoriste za neku drugu turu.\n";
            }
            else if (tabControl.SelectedIndex == 1)
            {
                toolTipButton.ToolTip = "Dobrodošli na stranicu ,,Kreiranje ture!\"\n\n"
                  + "Treba da popunite sva sledeća polja:\n"
                      + "• Naziv ture: unesite željeni naziv nove ture \n"
                       + " • Lokacija održavanja ture: iz padajućih menija odaberite prvo državu(levo), zatim grad u toj državi(desno)\n"
                       + "  • • Jezik na kom se tura održava: iz padajućeg menija odaberite jezik na kojem ćete održati Vašu turu\n"
                      + "  • Maksimalan broj gostiju: unesite maksimalan broj gostiju koji može da prisustvuje Vašoj turi\n"
                      + "  • Trajanje ture(u satima): unesite koliko želite da nova tura traje\n"
                      + "  • Jedna ili više slika: unesite url putanje slika koje će korisnici moći da vide prilikom odabira ture\n"
                      + "  • Ključne tačke: unesite makar dve ključne tačke iz kojih će Vaša nova tura da se sastoji. U polju ispod možete videti koje ste uneli\n"
                       + " • Datum i vreme početka ture: klikom na kalendar imaćete priliku da odaberete jedan od datuma koji se nalazi u već unapred zadatom opseg,\n"
                       + " odaberite onaj datum za koji ste sigurni da Vam je slobodan.\n"
                       + " • Opis događaja na turi: ukratko opišite šta će se sve dešavati na Vašoj turi, potrudite se da bude interesantno Vašim budućim gostima \n\n"
                       + " Ako ste zadovoljni Vašim unosom i želite da konačno potvrdite tj kreirate turu pritisnite na dugme ,,Potvrdi\" \n"
                       + " Ako niste zadovoljni i želite da otkažete kreiranje ture pritisnite na dugme,, Odustani\" \n";


                toursNameTextBox.Text = string.Empty;
                maximumNumberOfGuests.Value = 1;
                duration.Value = 1;
                picturesTextBox.Text = string.Empty;
                keyPointTextBox.Text = string.Empty;
                keyPointsOutput.Items.Clear();
                descriptionTextBox.Text = string.Empty;
                if (ToursViewModel.DateTimeList.Count > 0)
                {
                    ToursViewModel.DateTimeList.Clear();
                }
                if (ToursViewModel.KeyPointsList.Count > 0)
                {
                    ToursViewModel.KeyPointsList.Clear();
                }

            }
            else
            {
                toolTipButton.ToolTip = "Dobrodošli na stranicu ,,Statistika tura\"!\n\n"
                + "Sa Vaše desne strane nalazi se prikaz najposećenije ture. Pored samog naslova ,,Najposećenija tura\" nalazi se padajući meni, \n"
                + "pomoću kojeg možete da odaberete godinu za koju želite da vidite najposećeniju turu. Isto tako ima i opcija ,,Svih vremena\", \n"
                + "koja će Vam izlistati najposećeniju turu svih vremena! \n"
                + "Ispod izlistane ture nalazi se statistika prikazane najposećenije ture: \n"
                + "• Broj gostiju po starosnoj grupi: prikazuje se broj gostiju, koji su bili prisutni na turi, za svaku starosnu grupu \n"
                + "• Procenat gostiju sa/bez vaučera: prikazuje se procenat gostiju koji su iskoristili vaučer da bi rezervisali svoje mesto na turi, \n"
                + "a isto tako i procenat gostiju koji nisu koristili vaučer da bi rezervisali svoje mesto na turi\n\n"
                + "Sa Vaše leve strane ispod naslova ,,Završene ture\" izlistane su sve završene, otkazane i prekinute ture\n"
                + "Ako želite da vidite statistiku određene ture morate da je odaberete (levim klikom na nju) i onda da pritisnete na dugme ,,Prikaži statistiku\",\n"
                + "koje se nalazi ispod same tabele \n"
                + "Nakon toga, sa Vaše desne strane biće prikazana statistika izabrane ture u kojoj čete moči da vidite ponovo broj gostiju po starosnoj grupi \n"
                + "i procenat gostiju sa/bez vaučera \n"
                + "Klikom na dugme, koje se nalazi ispod, ,,Prikaži najposećeniju turu\", ažuriraće se prikaz tako da prikaže najposećeniju turu, a skloniće se \n"
                + "prikaz statistike Vaše prethodno izabrane ture \n";
            }
        }

        //PREDSTOJECE TURE

        private void StartTourButton_Click(object sender, RoutedEventArgs e)
        {
            if (ToursViewModel.IsCreatedTourSelected())
            {
                startTourButton.IsEnabled = false;
                LiveTourTrackingView liveTourTrackingView = new(ToursViewModel.SelectedCreatedTour, _keyPointService, _tourReservationService, _userService, _tourService, _tourReviewService, _requestService, LoggedInGuide, _locationService, _voucherService, _countriesAndCitiesService, _tourNotificationService, _complexTourRequestService, _subTourRequestService);
                FrameManagerGuide.Instance.MainFrame.Navigate(liveTourTrackingView);
                ToursViewModel.Update();
            }
        }

        private void CancelTourButton_Click(object sender, RoutedEventArgs e)
        {
            if (ToursViewModel.IsCreatedTourSelected() && ToursViewModel.IsTourEligibleForCancellation())
            {
                ToursViewModel.CancelTour("Otkazujem");
                SuccessfulCancellationLabelEvent();
            }
        }

        private void SuccessfulCancellationLabelEvent()
        {
            successfulEventLabel.Content = "Uspešno ste otkazali turu";
            successfulEventLabel.Visibility = Visibility.Visible;
            startTourButton.Margin = new Thickness(20, 20, 0, 0);
            DispatcherTimer timer = new()
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            successfulEventLabel.Visibility = Visibility.Hidden;
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
        }

        public void returnToTourButton_Click(object sender, RoutedEventArgs e)
        {
            startTourButton.IsEnabled = false;
            LiveTourTrackingView liveTourTrackingView = new(ToursViewModel.StartedTour, _keyPointService, _tourReservationService, _userService, _tourService, _tourReviewService, _requestService, LoggedInGuide, _locationService, _voucherService, _countriesAndCitiesService, _tourNotificationService, _complexTourRequestService, _subTourRequestService);
            FrameManagerGuide.Instance.MainFrame.Navigate(liveTourTrackingView);
            ToursViewModel.Update();
        }

        //KREIRANJE TURE

        public void CountryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Retrieve the list of cities for the selected country
            var selectedCountry = (CountriesAndCities)countryComboBox.SelectedItem;
            var cities = new List<string> { selectedCountry.City1, selectedCountry.City2, selectedCountry.City3, selectedCountry.City4, selectedCountry.City5 };

            // Bind the city ComboBox to the list of cities
            cityComboBox.ItemsSource = cities;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (keyPointsOutput.Items.Count > 1 && toursNameTextBox.Text != "" && picturesTextBox.Text != ""
                && descriptionTextBox.Text != "" && maximumNumberOfGuests.Text != "" && duration.Text != "")
            {
                ToursViewModel.ConfirmCreation(countryComboBox.Text, cityComboBox.Text);
                ToursViewModel.Update();
                tabControl.SelectedIndex = 0;
                SuccessfulCreationLabelEvent();
            }
            else
            {
                ValidationErrorLabelEvent();
            }
        }

        public void ValidationErrorLabelEvent()
        {
            validationLabel.Visibility = Visibility.Visible;

            if (toursNameTextBox.Text == "")
            {
                toursNameTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }

            if (cityComboBox.SelectedItem == null)
            {
                cityComboBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }

            if (countryComboBox.SelectedItem == null)
            {
                countryComboBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }

            if (picturesTextBox.Text == "")
            {
                picturesTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }

            if (descriptionTextBox.Text == "")
            {
                descriptionTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
            }

            if (maximumNumberOfGuests.Text == "" || Int32.Parse(maximumNumberOfGuests.Text) <= 0)
            {
                maximumNumberOfGuests.BorderBrush = System.Windows.Media.Brushes.Red;
            }

            if (duration.Text == "" || Int32.Parse(duration.Text) <= 0)
            {
                duration.BorderBrush = System.Windows.Media.Brushes.Red;
            }

            if (keyPointsOutput.Items.Count <= 1)
            {
                keyPointTextBox.BorderBrush = System.Windows.Media.Brushes.Red;
                keyPointsOutput.BorderBrush = System.Windows.Media.Brushes.Red;
            }

            if (tourDatePicker.Text.ToString() == "")
            {
                tourDatePicker.BorderBrush = System.Windows.Media.Brushes.Red;
            }

            DispatcherTimer timer = new()
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            timer.Tick += Timer_Tick_Validation;
            timer.Start();
        }

        private void Timer_Tick_Validation(object sender, EventArgs e)
        {
            validationLabel.Visibility = Visibility.Hidden;

            toursNameTextBox.BorderBrush = System.Windows.Media.Brushes.Gray;
            picturesTextBox.BorderBrush = System.Windows.Media.Brushes.Gray;
            descriptionTextBox.BorderBrush = System.Windows.Media.Brushes.Gray;
            maximumNumberOfGuests.BorderBrush = System.Windows.Media.Brushes.Gray;
            duration.BorderBrush = System.Windows.Media.Brushes.Gray;
            tourDatePicker.BorderBrush = System.Windows.Media.Brushes.Gray;
            keyPointTextBox.BorderBrush = System.Windows.Media.Brushes.Gray;
            keyPointsOutput.BorderBrush = System.Windows.Media.Brushes.Gray;

            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
        }

        private void SuccessfulCreationLabelEvent()
        {
            successfulEventLabel.Content = "Uspešno ste kreirali turu!";
            successfulEventLabel.Visibility = Visibility.Visible;
            startTourButton.Margin = new Thickness(20, 20, 0, 0);
            DispatcherTimer timer = new()
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 0;
        }

        private void AddKeyPointsButton_Click(object sender, RoutedEventArgs e)
        {
            string inputText = keyPointTextBox.Text;
            if (!string.IsNullOrWhiteSpace(keyPointTextBox.Text) && !keyPointsOutput.Items.Contains(keyPointTextBox.Text))
            {
                keyPointsOutput.Items.Add(inputText);
                ToursViewModel.AddKeyPointsToList(inputText);
                keyPointTextBox.Clear();
            }
        }

        private void KeyPointTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            addKeyPointsButton.IsEnabled = !string.IsNullOrEmpty(keyPointTextBox.Text);
        }

        private void TourDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ToursViewModel.AddDatesToList(tourDatePicker.Text);
        }

        //STATISTIKA TURE

        private void DisplayStatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            if (ToursViewModel.IsFinishedTourSelected())
            {
                ChangeDisplay();
                ToursViewModel.DisplayStatistics();
            }
        }

        private void ChangeDisplay()
        {
            theMostVisitedTourLabel.Content = "Statistika izabrane ture";
            yearComboBox.Visibility = Visibility.Hidden;
            theMostVisitedTourDataGrid.Visibility = Visibility.Hidden;
            theMostVisitedTourStatisticsLabel.Visibility = Visibility.Hidden;
            displayTheMostVisitedTourButton.Visibility = Visibility.Visible;
            displayTheMostVisitedTourButton.Margin = new Thickness(270, -100, 0, 0);
            cartesianChart.Height = 400;
            cartesianChart.Margin = new Thickness(0, -60, 0, 100);
            cartesianChartLabel.Margin = new Thickness(40, -100, 0, 0);
            pieChart.Height = 400;
            pieChart.Margin = new Thickness(0, -60, 80, 100);
            pieChartLabel.Margin = new Thickness(40, -100, 0, 0);
        }

        private void YearComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ComboBox cBox = (ComboBox)sender;
            string year = (cBox.SelectedItem.ToString());
            ToursViewModel.UpdateTheMostVisitedTour(LoggedInGuide, year);
        }

        private void DisplayTheMostVisitedTourButton(object sender, RoutedEventArgs e)
        {
            //vrati nazad sve
            ToursView toursView = new(_tourService, _tourReviewService, _tourReservationService, _keyPointService, _locationService, _voucherService, _userService, LoggedInGuide, _countriesAndCitiesService, _requestService, _tourNotificationService, _complexTourRequestService, _subTourRequestService);
            FrameManagerGuide.Instance.MainFrame.Navigate(toursView);
            toursView.tabControl.SelectedIndex = 2;
        }
    }
}
