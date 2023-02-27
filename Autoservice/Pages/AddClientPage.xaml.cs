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
    /// Логика взаимодействия для AddClientPage.xaml
    /// </summary>
    public partial class AddClientPage : Page
    {
        List<Gender> Genders;

        public Client client { get; }
        public Client oldClient { get; set; }

        public AddClientPage(Client _client)
        {
            InitializeComponent();
            Genders = DataAccess.GetGenders();
            cbGender.ItemsSource = Genders;

            if (_client.FirstName == null)
            {
                client = new Client();
                oldClient = new Client();
            }
            else
                client = _client;

            tbxFirstName.Text = client.FirstName;
            tbxLastName.Text = client.LastName;
            tbxPatronymic.Text = client.Patronymic;
            tbxPhone.Text = client.Phone;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                client.FirstName = tbxFirstName.Text;
                client.LastName = tbxLastName.Text;
                client.Patronymic = tbxPatronymic.Text;
                client.RegistrationDate = DateTime.Now;
                client.Phone = tbxPhone.Text;
                client.Gender = cbGender.SelectedItem as Gender;
                client.IsDeleted = false;

                if (oldClient != null)
                    DataAccess.AddClient(client);
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
