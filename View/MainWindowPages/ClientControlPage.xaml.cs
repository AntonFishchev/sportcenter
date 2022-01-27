using Entities;
using Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Views.MainWindowPages
{
    public partial class ClientControlPage : Page
    {
        private Employee currentUser;
        private readonly ClientService clientService;
        private List<Client> clients;
        private List<Client> clientsSearchByPhone;
        private List<Client> clientsSearchByName;

        public ClientControlPage(Employee employee)
        {
            InitializeComponent();

            Loaded += ClientControlPage_Loaded;

            clientService = new ClientService();
            currentUser = employee;
        }

        private void ClientControlPage_Loaded(object sender, RoutedEventArgs e)
        {
            clients = clientService.GetAllClients();
            clientsListView.ItemsSource = clients.OrderBy(c => c.LastName)
                                                 .ThenBy(c => c.FirstName)
                                                 .ThenBy(c => c.MiddleName);
            clientsSearchByPhone = clients;
            clientsSearchByName = clients;
        }

        private void SearchByPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            clientsSearchByPhone = clients.Where(client => client.Phone.Contains(searchByPhoneField.Text)).ToList();
            clientsListView.ItemsSource = clientsSearchByPhone.Intersect(clientsSearchByName).ToList();
        }

        private void SearchByName_TextChanged(object sender, TextChangedEventArgs e)
        {
            clientsSearchByName = clients
                .Where(client => client.FirstName.ToLower().Contains(searchByNameField.Text.ToLower())
                                || client.LastName.ToLower().Contains(searchByNameField.Text.ToLower()))
                .ToList();
            clientsListView.ItemsSource = clientsSearchByName.Intersect(clientsSearchByPhone).ToList();
        }

        private void btnRefreshClients_Click(object sender, RoutedEventArgs e)
        {
            clients = clientService.GetAllClients();
        }

        private void btnInfoClient_Click(object sender, RoutedEventArgs e)
        {
            if (clientsListView.SelectedItem != null)
            {
                Window clientInformationWindow = new ClientInformationWindow((Client)clientsListView.SelectedItem);
                clientInformationWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Клиент не выбран");
            }
        }

        private void btnCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            if (clientsListView.SelectedItem != null)
            {
                Window orderCreateWindow = new OrderCreateWindow((Client)clientsListView.SelectedItem, currentUser);
                orderCreateWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Клиент не выбран");
            }
            
        }

        private void btnRegisterClient_Click(object sender, RoutedEventArgs e)
        {
            Window clientRegistrationWindow = new ClientRegistrationWindow();
            clientRegistrationWindow.ShowDialog();
        }
    }
}
