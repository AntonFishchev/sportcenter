using Data;
using Entities;
using Services;
using System;
using System.Linq;
using System.Windows;

namespace Views
{
    public partial class LoginWindow : Window
    {
        EmployeeService employeeService = new EmployeeService();

        public LoginWindow()
        {
            InitializeComponent();

            Loaded += LoginWindow_Loaded;
        }

        private void LoginWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Получаем в ComboBox всех не уволенных сотрудников отсортированных по ФИО
            LoginField.ItemsSource = employeeService.GetCurrentEmployees()
                .OrderBy(employee => employee.LastName)
                .ThenBy(employee => employee.FirstName);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Проверка правильности введенного пароля
            if (employeeService.CheckPassword((Employee)LoginField.SelectedItem, PasswordField.Text))
            {
                Window mainWindow = new MainWindow((Employee)LoginField.SelectedItem);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Выберете сотрудника и введите правильный пароль");         
            }
        }

        private void CreateNewDB()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                Employee employee = new Employee(
                    "Фищев",
                    "Антон",
                    "Владиславович",
                    "123",
                    new DateTime(2002, 01, 06),
                    "89007006050",
                    new DateTime(2022, 01, 01),
                    "Директор"
                );
                context.employees.Add(employee);
                context.SaveChanges();
            }
        }
    }
}
