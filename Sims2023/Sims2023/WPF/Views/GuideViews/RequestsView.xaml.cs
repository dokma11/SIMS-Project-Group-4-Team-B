using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.ViewModels.GuideViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Sims2023.WPF.Views.GuideViews
{
    /// <summary>
    /// Interaction logic for RequestStatisticsView.xaml
    /// </summary>
    public partial class RequestsView : Page
    {
        public RequestsViewModel RequestsViewModel;
        private TourService _tourService;
        private LocationService _locationService;
        private KeyPointService _keyPointService;
        private TourReviewService _tourReviewService;
        private RequestService _requestService;
        private TourReservationService _tourReservationService;
        private VoucherService _voucherService;
        private UserService _userService;
        private CountriesAndCitiesService _countriesAndCitiesService;
        private TourNotificationService _tourNotificationService;
        public User LoggedInGuide { get; set; }

        public RequestsView(RequestService requestService, TourService tourService, LocationService locationService, KeyPointService keyPointService, TourReviewService tourReviewService, User loggedInGuide, TourReservationService tourReservationService, VoucherService voucherService, UserService userService, CountriesAndCitiesService countriesAndCitiesService, TourNotificationService tourNotificationService)
        {
            InitializeComponent();

            _tourService = tourService;
            _locationService = locationService;
            _keyPointService = keyPointService;
            _tourReviewService = tourReviewService;
            _requestService = requestService;
            _tourReservationService = tourReservationService;
            _countriesAndCitiesService = countriesAndCitiesService;
            _voucherService = voucherService;
            _userService = userService;
            _requestService = requestService;
            _tourNotificationService = tourNotificationService;

            LoggedInGuide = loggedInGuide;

            RequestsViewModel = new(requestService);
            DataContext = RequestsViewModel;

            languageComboBox.ItemsSource = RequestsViewModel.GetLanguages();
            locationComboBox.ItemsSource = RequestsViewModel.GetLocations();
            languageYearComboBox.ItemsSource = RequestsViewModel.GetYears();
            locationYearComboBox.ItemsSource = RequestsViewModel.GetYears();

            TextBox[] textBoxes = { locationTextBox, guestNumberTextBox, languageTextBox };

            foreach (TextBox textBox in textBoxes)
            {
                textBox.GotFocus += TextBox_GotFocus;
                textBox.LostFocus += TextBox_LostFocus;
                textBox.Text = textBox.Tag.ToString();
            }
        }

        private void LanguageComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (languageComboBox.SelectedItem != null && languageYearComboBox.SelectedItem != null)
            {
                RequestsViewModel.DisplayLanguageStatistics(languageComboBox.SelectedItem.ToString(), languageYearComboBox.SelectedItem.ToString());
            }
        }

        private void LocationComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (locationComboBox.SelectedItem != null && locationYearComboBox.SelectedItem != null)
            {
                RequestsViewModel.DisplayLocationStatistics(locationComboBox.SelectedItem.ToString(), locationYearComboBox.SelectedItem.ToString());
            }
        }

        private void LanguageConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            CreateTourFromFrequentLanguageView createTourFromFrequentLanguageView = new(RequestsViewModel.TheMostRequestedLanguage, _tourService, _locationService, _keyPointService, _tourReviewService, _requestService, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, LoggedInGuide, _tourNotificationService);
            FrameManagerGuide.Instance.MainFrame.Navigate(createTourFromFrequentLanguageView);
        }

        private void LocationConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            CreateTourFromFrequentLocationView createTourFromFrequentLocationView = new(RequestsViewModel.TheMostRequestedLocation, _tourService, _locationService, _keyPointService, _tourReviewService, _requestService, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, LoggedInGuide, _tourNotificationService);
            FrameManagerGuide.Instance.MainFrame.Navigate(createTourFromFrequentLocationView);
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            string locationSearchTerm = locationTextBox.Text == "Unesite lokaciju" ? "" : locationTextBox.Text;
            string guestNumberSearchTerm = guestNumberTextBox.Text == "Unesite broj gostiju" ? "" : guestNumberTextBox.Text;
            string languageSearchTerm = languageTextBox.Text == "Unesite jezik" ? "" : languageTextBox.Text;
            string dateStartSearchTerm = dateStartTextBox.Text;
            string dateEndSearchTerm = dateEndTextBox.Text;

            requestDataGrid.ItemsSource = RequestsViewModel.FilterRequests(locationSearchTerm, guestNumberSearchTerm, languageSearchTerm, dateStartSearchTerm, dateEndSearchTerm);
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            CreateTourFromRequestView createTourFromRequestView = new(RequestsViewModel.SelectedRequest, _tourService, _locationService, _keyPointService, _tourReviewService, _requestService, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, LoggedInGuide, _tourNotificationService);
            FrameManagerGuide.Instance.MainFrame.Navigate(createTourFromRequestView);
            RequestsViewModel.HandleRequest(true);
        }

        private void DeclineButton_Click(object sender, RoutedEventArgs e)
        {
            RequestsViewModel.HandleRequest(false);
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string placeholderText = textBox.Tag.ToString();
            if (textBox.Text == placeholderText)
            {
                textBox.Text = "";
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string placeholderText = textBox.Tag.ToString();
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = placeholderText;
            }
        }

        private void TabControlSelectionChanged(object sender, RoutedEventArgs e)
        {
            if(TabControl.SelectedIndex == 0)
            {
                toolTipButton.ToolTip = "Sa Vaše leve strane prikazani su svi zahtevi za kreiranje ture.\n\n"
                + "U tabeli sa leve strane možete prvo videti šest informacija:\n"
                + "• Korisničko ime korisnika koji je kreirao zahtev\n"
                + "• Lokaciju na kojoj korisnik želi da se tura održi\n"
                + "• Broj gostiju koji bi bio prisutan na turi\n"
                + "• Početak i kraj opsega za datum održavanja date ture. \n"
                + "Treba da odaberete jedan datum kad ste slobodni u datom opsegu\n"
                + "• Da biste videli opis ture koji je korisnik zadao, morate da pritisnete na taj zahtev\n"
                + "Jednim levim klikom red će se proširiti i Vi ćete moći da pročitate opis datog zahteva za turu.\n"
                + "• U koloni prihvati postoje dva dugmeta, jedno je za prihvatanje drugo za odbijanje zahteva:\n"
                + "Ako kliknete da dugme ,,Prihvati\" preći ćete na prozor za kreiranje ture preko prihvaćenog zahteva\n"
                + "Ako kliknete na dugme ,,Odbij\" odabrani zahtev će biti odbijen i samim time uklonjne iz liste.\n\n"
                + "Sa desne strane, ispod naslova ,,Filtriraj prema\" videćete više polja pomoću kojih možete da filtrirate zahteve: \n"
                + "• Kada filtrirate prema lokaciji, biće Vam ispisani samo zahtevi koji se nalaze na unetoj lokaciji \n"
                + "• Kada filtrirate prema broju gostiju, biće Vam ispisani samo zahtevi koji su predviđeni za uneti broj gostiju \n"
                + "• Kada filtrirate prema jeziku, biće Vam ispisani samo zahtevi koji su predviđeni da se održe na unetom jeziku \n"
                + "• Kada filtrirate prema datumima, unosite početni i krajnji opseg u kojem želite da vidite zahteve.\n"
                + "Biće prikazani oni zahtevi koji su zadovoljili uneti opseg \n"
                + "• Na samom kraju postoji dugme ,,Filtriraj\" na koje kad kliknete primenićete željene promene.\n";
            }
            else if(TabControl.SelectedIndex == 1)
            {
                toolTipButton.ToolTip = "Sa Vaše leve strane prikazana je statistika o zahtevima za ture na lokaciji.\n\n"
                + "Postoje dva padajuća menija:\n"
                + "• Levi Vam služi da odaberete lokaciju za koju želite da vidite statistiku.\n"
                + "• Desni Vam služi da odaberete godinu u kojoj želite da vidite statistiku za odabranu lokaciju.\n\n"
                + "Sa Vaše desne strane prikazana je najtraženija lokacija u proteklih godinu dana.\n"
                + "Ispod nje prikazana je statistika date lokacije, takođe postoji dugme sa natpisom ,,Da\".\n"
                + "Pritiskom na to dugme, preći ćete na prozor za kreiranje ture na najtraženijoj lokaciji.";
            }
            else
            {
                toolTipButton.ToolTip = "Sa Vaše leve strane prikazana je statistika o zahtevima za ture na jeziku.\n\n"
                + "Postoje dva padajuća menija:\n"
                + "• Levi Vam služi da odaberete jezik za koji želite da vidite statistiku.\n"
                + "• Desni Vam služi da odaberete godinu u kojoj želite da vidite statistiku za odabrani jezik.\n\n"
                + "Sa Vaše desne strane prikazana je najtraženiji jezik u proteklih godinu dana.\n"
                + "Ispod nje prikazana je statistika datog jezika, takođe postoji dugme sa natpisom ,,Da\".\n"
                + "Pritiskom na to dugme, preći ćete na prozor za kreiranje ture na najtraženijem jeziku.";
            }
        }

        //TOOLBAR

        private void HomePageButton_Click(object sender, RoutedEventArgs e)
        {
            GuideHomePageView guideHomePageView = new(LoggedInGuide);
            FrameManagerGuide.Instance.MainFrame.Navigate(guideHomePageView);
        }

        private void ToursButton_Click(object sender, RoutedEventArgs e)
        {
            ToursView toursView = new(_tourService, _tourReviewService, _tourReservationService, _keyPointService, _locationService, _voucherService, _userService, LoggedInGuide, _countriesAndCitiesService, _requestService, _tourNotificationService);
            FrameManagerGuide.Instance.MainFrame.Navigate(toursView);
        }

        private void ReviewsButton_Click(object sender, RoutedEventArgs e)
        {
            GuestReviewsView guestReviewsView = new(_tourService, _tourReviewService, _locationService, _requestService, _keyPointService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, _tourNotificationService);
            FrameManagerGuide.Instance.MainFrame.Navigate(guestReviewsView);
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            GuideAccountView guideAccountView = new(_tourService, _tourReviewService, _locationService, _requestService, _keyPointService, LoggedInGuide, _tourReservationService, _voucherService, _userService, _countriesAndCitiesService, _tourNotificationService);
            FrameManagerGuide.Instance.MainFrame.Navigate(guideAccountView);
        }
    }
}
