using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Sims2023.WPF.Commands
{
    public static class RoutedCommands
    {
        public static readonly RoutedUICommand OpenMenu = new RoutedUICommand(
            "Open Menu",
            "OpenMenu",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.M, ModifierKeys.Control),
                new KeyGesture(Key.M, ModifierKeys.Alt | ModifierKeys.Control)
            }
            );
        public static readonly RoutedUICommand OpenHelp = new RoutedUICommand(
            "Open Help",
            "OpenHelp",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.H, ModifierKeys.Control),
                new KeyGesture(Key.H, ModifierKeys.Alt | ModifierKeys.Control)
            }
            );
    }
}
