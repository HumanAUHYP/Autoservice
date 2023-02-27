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
    /// Логика взаимодействия для ServicesTablePage.xaml
    /// </summary>
    public partial class ServicesTablePage : Page
    {
        public MainWindow globalMainWindow;
        public List<Service> Services { get; set; }
        public List<Service> AllServices { get; set; }
        const int ITEMONPAGE = 20;
        int pageIndex = 0;
        public ServicesTablePage(MainWindow mainWindow)
        {
            InitializeComponent();
            globalMainWindow = mainWindow;
            globalMainWindow.tbTitle.Text = "Услуги";

            Services = DataAccess.GetServices().FindAll(a => a.IsD);
            AllServices = Services;
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if (lvTable.SelectedItem != null)
            {
                var client = lvTable.SelectedItem as Client;
                NavigationService.Navigate(new AddClientPage(client));
            }
            else
                MessageBox.Show("Выберите клиента для изменения");

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddServicePage(new Service()));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var service = lvTable.SelectedItem as Service;
                service.IsDeleted = true;
                db.connection.SaveChanges();

                GeneratePages();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void Paginator(object sender, MouseButtonEventArgs e)
        {
            var content = (sender as TextBlock).Text;

            var pagesCount = (int)Math.Ceiling((double)Services.Count / ITEMONPAGE);

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

            var pagesCount = (int)Math.Ceiling((double)Services.Count / ITEMONPAGE);

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
            var servicesInPage = new List<Service>();
            for (int i = pageIndex * ITEMONPAGE; i < (pageIndex + 1) * ITEMONPAGE; i++)
            {
                try
                {
                    servicesInPage.Add(Services[i]);
                }
                catch (Exception)
                {
                    break;
                }
            }
            lvTable.ItemsSource = servicesInPage;
        }

        private void AllFilters()
        {
            Services = AllServices;

            var search = tbSearch.Text.ToLower().Trim();

            if (search != "")
            {
                Services = Services.FindAll(a => a.Title.ToLower().Contains(search));
            }

            var selectedSort = cbSort.SelectedItem as TextBlock;
            if (selectedSort == tbTitleSort)
                Services = Services.OrderBy(a => a.Title).ToList();
            else if (selectedSort == tbTitleDescSort)
                Services = Services.OrderByDescending(a => a.Title).ToList();

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
