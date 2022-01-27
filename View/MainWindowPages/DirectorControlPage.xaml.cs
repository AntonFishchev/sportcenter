using Entities;
using System.Windows;
using System.Windows.Controls;

namespace Views.MainWindowPages
{
    public partial class DirectorControlPage : Page
    {
        private Employee currentUser;

        public DirectorControlPage(Employee employee)
        {
            InitializeComponent();

            currentUser = employee;
        }

        private void BtnRegisterEmployee_Click(object sender, RoutedEventArgs e)
        {
            Window employeeRegisterWindow = new EmployeeRegisterWindow();
            employeeRegisterWindow.ShowDialog();
        }

        private void btnDismissalEmployee_Click(object sender, RoutedEventArgs e)
        {
            Window employeeDismissalWindow = new EmployeeDismissalWindow(currentUser);
            employeeDismissalWindow.ShowDialog();
        }

        private void btnRegisterPlayPlace_Click(object sender, RoutedEventArgs e)
        {
            Window playPlaceRegister = new PlayPlaceRegisterWindow();
            playPlaceRegister.ShowDialog();
        }

        private void btnWorkingСapacityPlayPlace_Click(object sender, RoutedEventArgs e)
        {
            Window playPlaceWorkingСapacityWindow = new PlayPlaceWorkingСapacityWindow();
            playPlaceWorkingСapacityWindow.ShowDialog();
        }

        private void btnChangePricePlayPlace_Click(object sender, RoutedEventArgs e)
        {
            Window playPlaceChangePriceWindow = new PlayPlaceChangePriceWindow();
            playPlaceChangePriceWindow.ShowDialog();
        }

        private void btnRegisterInventoryItem_Click(object sender, RoutedEventArgs e)
        {
            Window inventoryItemRegisterWindow = new InventoryItemRegisterWindow();
            inventoryItemRegisterWindow.ShowDialog();
        }

        private void btnChangePriceInventoryItem_Click(object sender, RoutedEventArgs e)
        {
            Window inventoryItemChangePriceWindow = new InventoryItemChangePriceWindow();
            inventoryItemChangePriceWindow.ShowDialog();
        }

        private void btnWriteOffInventoryItem_Click(object sender, RoutedEventArgs e)
        {
            Window inventoryItemWriteOffWindow = new InventoryItemWriteOffWindow();
            inventoryItemWriteOffWindow.ShowDialog();
        }
    }
}
