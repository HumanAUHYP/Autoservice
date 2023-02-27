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
    /// Логика взаимодействия для AddServicePage.xaml
    /// </summary>
    public partial class AddServicePage : Page
    {
        public Service service { get; }
        public Service oldService { get; set; }
        public AddServicePage(Service _service)
        {
            InitializeComponent();

            if (_service.Title == null)
            {
                service = new Service();
                oldService = new Service();
            }
            else
                service = _service;

            tbxName.Text = service.Title;
            tbxDurationInSec.Text = service.DurationInSeconds.ToString();
            tbxCost.Text = service.Cost.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                service.Title = tbxName.Text;
                service.DurationInSeconds = Convert.ToInt32(tbxDurationInSec.Text);
                service.Cost = Convert.ToDecimal(tbxCost.Text);
                service.IsDeleted = false;

                if (oldService != null)
                    DataAccess.AddService(service);
                else
                    db.connection.SaveChanges();

                NavigationService.GoBack();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка");
            }
        }
    }
}
