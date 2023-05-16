using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Sims2023.Domain.Models;

namespace Sims2023.WPF.Templates
{
    public class StatusTemplateSelector : DataTemplateSelector
    {
        public DataTemplate CreatedTemplate { get; set; }
        public DataTemplate StartedTemplate { get; set; }
        public DataTemplate FinishedTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is TourReservation dataItem)
            {
                switch (dataItem.Tour.CurrentState)
                {
                    case ToursState.Created:
                        return CreatedTemplate;
                    case ToursState.Started:
                        return StartedTemplate;
                    case ToursState.Finished:
                        return FinishedTemplate;
                    default:
                        return null;
                }
            }

            return null;
        }
    }
}
