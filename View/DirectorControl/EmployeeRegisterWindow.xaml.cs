using Services;
using System;
using System.Windows;

namespace Views
{
    public partial class EmployeeRegisterWindow : Window
    {
        private readonly EmployeeService employeeService = new EmployeeService();

        public EmployeeRegisterWindow()
        {
            InitializeComponent();

            Loaded += EmployeeRegisterWindow_Loaded;
        }

        private void EmployeeRegisterWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Выставляется текущая дата при загрузки окна
            dateOfEmploymentField.SelectedDate = DateTime.Now;
            // Получение всех существующих должностей в ComboBox
            roleField.ItemsSource = employeeService.GetRoles();
        }

        private void btnGeneratePassword_Click(object sender, RoutedEventArgs e)
        {
            // Генерация случайного пароля
            passwordField.Text = employeeService.PasswordGeneration();
        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            // Регистрация нового сотрудника
            if (employeeService.RegisterEmployee(
                    lastNameField.Text, 
                    firstNameField.Text, 
                    middleNameField.Text, 
                    passwordField.Text, 
                    dayOfBirthField.SelectedDate, 
                    phoneField.Text, 
                    dateOfEmploymentField.SelectedDate,
                    roleField.Text
                )
            )
            {
                this.Close();
                MessageBox.Show($"{lastNameField.Text} {firstNameField.Text} {middleNameField.Text} добавлен");
            }
            else
            {
                MessageBox.Show("Введите данные во все поля");
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
