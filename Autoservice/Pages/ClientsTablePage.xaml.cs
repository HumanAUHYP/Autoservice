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
        int pageIndex = 0;
        public ClientsTablePage(MainWindow mainWindow)
        {
            InitializeComponent();
            globalMainWindow = mainWindow;
            globalMainWindow.tbTitle.Text = "Клиенты";

            Clients = DataAccess.GetClients();
            lvTable.ItemsSource = Clients;

            GeneratePages();
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
                (spPages.Children[pageIndex] as TextBlock).TextDecorations = TextDecorations.Underline;

        }
    }
}
