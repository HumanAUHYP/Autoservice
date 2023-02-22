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
        public List<Client> AllClients { get; set; }
        public List<Gender> Genders { get; set; }
        const int ITEMONPAGE = 20;
        int pageIndex = 0;
        public ClientsTablePage(MainWindow mainWindow)
        {
            InitializeComponent();
            globalMainWindow = mainWindow;
            globalMainWindow.tbTitle.Text = "Клиенты";

            Genders = DataAccess.GetGenders();
            Genders.Insert(0, new Gender { Name = "Любой гендер" });
            cbGender.ItemsSource = Genders;

            Clients = DataAccess.GetClients();
            AllClients = Clients;
            lvTable.ItemsSource = Clients;

            GeneratePages();

            cbSort.SelectedIndex = 0;
            cbGender.SelectedIndex = 0;
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

        private void Paginator(object sender, MouseButtonEventArgs e)
        {
            var content = (sender as TextBlock).Text;

            var pagesCount = (int)Math.Ceiling((double)Clients.Count / ITEMONPAGE);

            if (content.Contains("<") && pageIndex > 0)
                pageIndex--;
            else if (content.Contains(">") && pageIndex < pagesCount - 1)
                pageIndex++;
            else if (int.TryParse(content, out int pageNum))
                pageIndex = pageNum - 1;

            
            GeneratePages();
        }

        private void GeneratePages()
        {
            spPages.Children.Clear();

            var pagesCount = (int)Math.Ceiling((double)Clients.Count / ITEMONPAGE);

            for (int i = 0; i < pagesCount; i++)
            {
                spPages.Children.Add(new TextBlock
                {
                    Text = (i + 1).ToString()
                });

                spPages.Children[i].PreviewMouseDown += Paginator;
            }
            if (spPages.Children.Count != 0)
            {
                if (pageIndex > spPages.Children.Count)
                    pageIndex = 0;
                (spPages.Children[pageIndex] as TextBlock).TextDecorations = TextDecorations.Underline;
            }
                

            DisplayClientsInPage();
        }

        private void DisplayClientsInPage()
        {
            var clientsInPage = new List<Client>();
            for (int i = pageIndex * ITEMONPAGE; i < (pageIndex + 1) * ITEMONPAGE; i++)
            {
                try
                {
                    clientsInPage.Add(Clients[i]);
                }
                catch (Exception)
                {
                    break;
                }

            }
            lvTable.ItemsSource = clientsInPage;
        }

        private void AllFilters()
        {
            Clients = AllClients;

            var search = tbSearch.Text.ToLower().Trim();

            if (search != "")
            {
                Clients = Clients.FindAll(a => 
                    a.FirstName.ToLower().Contains(search) ||
                    a.LastName.ToLower().Contains(search) ||
                    a.Patronymic.ToLower().Contains(search));
            }

            var selectedSort = cbSort.SelectedItem as TextBlock;
            if (selectedSort == tbFNameSort)
                Clients = Clients.OrderBy(a => a.FirstName).ToList();
            else if (selectedSort == tbFNameDescSort)
                Clients = Clients.OrderByDescending(a => a.FirstName).ToList();
            else if (selectedSort == tbLNameSort)
                Clients = Clients.OrderBy(a => a.LastName).ToList();
            else if (selectedSort == tbLNameDescSort)
                Clients = Clients.OrderByDescending(a => a.LastName).ToList();

            var selectedGender = cbGender.SelectedItem as Gender;
            if (cbGender.SelectedIndex != 0)
                Clients = Clients.FindAll(a => a.Gender == selectedGender);
            GeneratePages();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            AllFilters();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AllFilters();
        }

        private void cbGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AllFilters();
        }
    }
}
