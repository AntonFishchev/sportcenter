using Entities;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Views
{
    public partial class EmployeeDismissalWindow : Window
    {
        private readonly EmployeeService employeeService;
        private Employee currentUser;

        public EmployeeDismissalWindow(Employee employee)
        {
            InitializeComponent();

            employeeService = new EmployeeService();
            currentUser = employee;

            Loaded += EmployeeDismissalWindow_Loaded;
        }

        private void EmployeeDismissalWindow_Loaded(object sender, RoutedEventArgs e)
        {
            List<Employee> employees = new EmployeeService().GetCurrentEmployees();
            employees.Remove(currentUser);

            Employees.ItemsSource = employees
                .OrderBy(employee => employee.LastName)
                .ThenBy(employee => employee.FirstName)
                .ThenBy(employee => employee.MiddleName)
                .ToList(); 

            dateOfDismissalField.SelectedDate = DateTime.Now;
        }

        private void btnEmployeeDismissal_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = (Employee)Employees.SelectedItem;
            if (employee != null)
            {
                if (employee.DateOfEmployment < dateOfDismissalField.SelectedDate)
                {
                    if (employeeService.DismissalEmployee(employee, dateOfDismissalField.SelectedDate))
                    {
                        MessageBox.Show($"{employee.LastName} {employee.FirstName} {employee.MiddleName} был уволен");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show($"Введены некорректные данные");
                    }
                }
                else
                {
                    MessageBox.Show($"Дата увольнения не может быть меньше даты приема на работу. " +
                                    $"\nДата приема на работу - {employee.DateOfEmployment.ToString()}");
                }

            }
            else
            {
                MessageBox.Show($"Выберите сотрудника");
            }
        }
    }
}
