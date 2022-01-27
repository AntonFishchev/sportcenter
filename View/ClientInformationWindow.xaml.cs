using Entities;
using Services;
using System.Windows;

namespace Views
{
    public partial class ClientInformationWindow : Window
    {
        private readonly ClientService clientService;
        private readonly OrderService orderService;
        private Client client;

        public ClientInformationWindow(Client client)
        {
            InitializeComponent();

            this.clientService = new ClientService();
            this.orderService = new OrderService();
            this.client = client;

            Loaded += ClientInformationWindow_Loaded;
        }

        private void ClientInformationWindow_Loaded(object sender, RoutedEventArgs e)
        {
            clientIntoFields.DataContext = client;
            orderList.ItemsSource = orderService.GetOrdersByClient(client);
        }

        private void btnLastNameChange_Click(object sender, RoutedEventArgs e)
        {
            if (clientService.ChangeLastName(client, lastNameField.Text))
            {
                MessageBox.Show($"Фамилия изменена на {lastNameField.Text}");
                btnLastNameChange.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("Некорректное значение");
            }
        }

        private void btnFirstNameChange_Click(object sender, RoutedEventArgs e)
        {
            if (clientService.ChangeFirstName(client, firstNameField.Text))
            {
                MessageBox.Show($"Имя изменено на {firstNameField.Text}");
                btnFirstNameChange.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("Некорректное значение");
            }
        }

        private void btnMiddleNameChange_Click(object sender, RoutedEventArgs e)
        {
            if (clientService.ChangeMiddleName(client, middleNameField.Text))
            {
                MessageBox.Show($"Отчество изменено на {middleNameField.Text}");
                btnMiddleNameChange.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("Некорректное значение");
            }
        }

        private void btnPhoneChange_Click(object sender, RoutedEventArgs e)
        {
            if (clientService.ChangePhone(client, phoneField.Text))
            {
                MessageBox.Show($"Телефон изменен на {phoneField.Text}");
                btnPhoneChange.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("Некорректное значение");
            }
        }

        private void btnOrganizationChange_Click(object sender, RoutedEventArgs e)
        {
            if (clientService.ChangeOrganization(client, organizationField.Text))
            {
                MessageBox.Show($"Организация изменена на {organizationField.Text}");
                btnOrganizationChange.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("Некорректное значение");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            orderService.PaymentOrder((Order)orderList.SelectedItem);
            orderList.ItemsSource = orderService.GetOrdersByClient(client);
        }
    }
}
