using System.Windows.Controls;

namespace Sims2023.WPF.ViewModels.Guest2ViewModels
{
    public class FrameManagerGuest2
    {
        private static FrameManagerGuest2 instance = null;
        private Frame mainFrame = null;

        private FrameManagerGuest2()
        {
        }

        public static FrameManagerGuest2 Instance
        {
            get
            {
                instance ??= new FrameManagerGuest2();
                return instance;
            }
        }

        public Frame MainFrame
        {
            get
            {
                return mainFrame;
            }

            set
            {
                mainFrame = value;
            }
        }
    }
}

