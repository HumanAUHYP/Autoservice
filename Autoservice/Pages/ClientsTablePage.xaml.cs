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
using Autoservice.DB;

namespace Autoservice.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientsTablePage.xaml
    /// </summary>
    public partial class ClientsTablePage : Page
    {
        public MainWindow globalMainWindow;
        public List<Client> Clients { get; set; }
        const int ITEMONPAGE = 20;
        public ClientsTablePage(MainWindow mainWindow)
        {
            InitializeComponent();
            globalMainWindow = mainWindow;
            globalMainWindow.tbTitle.Text = "Клиенты";
            
            Clients = DataAccess.GetClients();
            lvTable.ItemsSource = Clients;
        }

        private void btnLastPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
