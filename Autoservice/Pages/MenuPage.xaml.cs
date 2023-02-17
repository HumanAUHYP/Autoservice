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

namespace Autoservice.Pages
{
    /// <summary>
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MainWindow globalMainWindow;
        public MenuPage(MainWindow mainWindow)
        {
            InitializeComponent();
            globalMainWindow = mainWindow;
            globalMainWindow.tbTitle.Text = "Меню";
        }

        private void btnClients_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ClientsTablePage(globalMainWindow));
        }

        private void btnServices_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ServicesTablePage());
        }

        private void btnPerformServices_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PerformServicesTablePage());
        }

    }
}
