using System.Windows.Controls;

namespace Sims2023.WPF.ViewModels.GuideViewModels
{
    public class FrameManagerGuide
    {
        private static FrameManagerGuide instance = null;
        private Frame mainFrame = null;

        private FrameManagerGuide()
        {
        }

        public static FrameManagerGuide Instance
        {
            get
            {
                instance ??= new FrameManagerGuide();
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
