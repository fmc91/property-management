using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace PropertyManagementUi
{
    public class MainWindow : NavigationWindow
    {
        public MainWindow()
        {
            ShowsNavigationUI = false;
            Navigated += (s, e) => NavigationService.RemoveBackEntry();

            Width = 1300;
            Height = 800;
            MinWidth = 650;
            MinHeight = 400;
        }
    }
}
