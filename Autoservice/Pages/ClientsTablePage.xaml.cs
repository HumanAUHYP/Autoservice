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
    /// Логика взаимодействия для ClientsTablePage.xaml
    /// </summary>
    public partial class ClientsTablePage : Page
    {
        public MainWindow globalMainWindow;
        public ClientsTablePage(MainWindow mainWindow)
        {
            InitializeComponent();
            globalMainWindow = mainWindow;
            globalMainWindow.tbTitle.Text = "Клиенты";
            
        }
    }
}
