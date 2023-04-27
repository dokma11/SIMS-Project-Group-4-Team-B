using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sims2023.WPF.ViewModels.OwnerViewModel
{
    public class FrameManager
    {
        private static FrameManager instance = null;
        private Frame mainFrame = null;

        private FrameManager()
        {
        }

        public static FrameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FrameManager();
                }

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
