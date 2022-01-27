using Services;
using System.Windows;

namespace Views
{
    public partial class ClientRegistrationWindow : Window
    {
        private readonly ClientService clientService;

        public ClientRegistrationWindow()
        {
            InitializeComponent();

            clientService = new ClientService();
        }


        private void btnDoneClientRegisterPage_Click(object sender, RoutedEventArgs e)
        {
            string message = clientService.RegisterClient(
                    lastNameClientRegisterPage.Text,
                    firstNameClientRegisterPage.Text,
                    middleNameClientRegisterPage.Text,
                    phoneClientRegisterPage.Text,
                    organizationClientRegisterPage.Text
                );

            MessageBox.Show(message);

            if (message == $"Клиент {lastNameClientRegisterPage.Text} {firstNameClientRegisterPage.Text} {middleNameClientRegisterPage.Text} добавлен(а)")
            {
                this.Close(); 
            }
        }
    }
}
