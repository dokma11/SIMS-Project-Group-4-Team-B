using Sims2023.Application.Services;
using Sims2023.Domain.Models;
using Sims2023.WPF.Views.Guest1Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ToastNotifications.Lifetime;

namespace Sims2023.WPF.ViewModels.Guest1ViewModel
{
    public class ForumCommentViewModel
    {
        private User User;
        private ForumCommentView ForumCommentView;
        private Forum SelectedForum;
        private ForumCommentService _forumCommentService;
        private ForumService _forumService;
        List<TourReservation> tourReservations = new();
        List<AccommodationReservation> accommodationReservations = new();

        public ForumCommentViewModel(User user, ForumCommentView forumCommentView, ForumCommentService commentService,Forum selectedForum,ForumService forumService)
        {
            User = user;
            SelectedForum=selectedForum;
            ForumCommentView = forumCommentView;
            tourReservations = new();
            accommodationReservations = new();
            _forumCommentService = commentService;
            _forumService = forumService;
        }

        internal void AddComment()
        {
            String comment=ForumCommentView.CommentBox.Text;
            if (string.IsNullOrEmpty(comment))
            {
                MessageBox.Show("Molimo Vas unesite komentar prvo.");
                return;
            }
            ForumComment newComment=new ForumComment();
            newComment.Forum = SelectedForum;
            newComment.User = User;
            newComment.Comment = comment;
            newComment.NumberOfReports = 0;
            if (MarkCommentsAsSpecial())
            {
                newComment.Special = true;
                SelectedForum.CountGuestComments++;
                _forumService.Update(SelectedForum);
            }
            else
            {
                newComment.Special = false;
            }
            _forumCommentService.Create(newComment);
            ForumCommentView.Close();
        }
        public bool MarkCommentsAsSpecial()
        {
            AccommodationReservationService _accommodationReservationService = new AccommodationReservationService();

            accommodationReservations = FindAccommodationReservationsOnThisLocation(_accommodationReservationService.FindSuitablePastReservations(User));
            TourReservationService _tourReservationService = new TourReservationService();
            tourReservations = FindTourReservationsOnThisLocation(_tourReservationService.GetUsersTours(User));
            _tourReservationService.GetUsersTours(User);

            MessageBox.Show($"smestaja: {accommodationReservations.Count} ,  tura: {tourReservations.Count()}");
            if (accommodationReservations.Count > 0 || tourReservations.Count() > 0)
            {
                return true;
            }
            return false;
        }

        private List<AccommodationReservation> FindAccommodationReservationsOnThisLocation(List<AccommodationReservation> accommodationReservations)
        {
            List<AccommodationReservation> reservations = new();
            foreach(AccommodationReservation reservation in accommodationReservations)
            {
                if(reservation.Accommodation.Location.Country==SelectedForum.Location.Country && reservation.Accommodation.Location.City==SelectedForum.Location.City)
                {
                    reservations.Add(reservation);
                }
            }
            return reservations;
        }
        private List<TourReservation> FindTourReservationsOnThisLocation(List<TourReservation> tourReservations)
        {
            List<TourReservation> reservations = new List<TourReservation>();

            foreach (TourReservation reservation in tourReservations)
            {
                if (reservation.Tour.Location.Country == SelectedForum.Location.Country && reservation.Tour.Location.City == SelectedForum.Location.City)
                {
                    reservations.Add(reservation);
                }
            }

            return reservations;
        }


        public void GoBack()
        {
            ForumCommentView.Close();
        }
    }
}
