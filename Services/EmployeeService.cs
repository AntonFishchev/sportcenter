using Data;
using Entities;
using System;
using System.Collections.Generic;
using static Data.Enums;

namespace Services
{
    public class EmployeeService
    {
        private readonly GenericRepository<Employee> _employeeRepository;

        public EmployeeService()
        {
            _employeeRepository = new GenericRepository<Employee>();
        }

        // Получение всех должностей
        public List<string> GetRoles()
        {
            List<string> list = new List<string>();
            foreach(string role in Enum.GetNames(typeof(Role)))
            {
                list.Add(role);
            }
            return list;
        }

        // Получение всех не уволенных сотрудников
        public List<Employee> GetCurrentEmployees()
        {
            List<Employee> list = new List<Employee>();
            foreach(Employee employee in _employeeRepository.GetAll())
            {
                if (employee.DateOfDismissal == null)
                {
                    list.Add(employee);
                }
            }
            return list;
        }

        // Регистрации нового сотрудника
        // Если все поля введены возвращает true, иначе false
        public bool RegisterEmployee(
            string lastName,
            string firstName,
            string middleName,
            string password,
            DateTime? dayOfBirth,
            string phone,
            DateTime? dateOfEmployment,
            string role
        )
        {
            if (lastName != "" &&
                firstName != "" &&
                middleName != "" &&
                password != "" &&
                dayOfBirth != null &&
                phone != "" && 
                dateOfEmployment != null &&
                role != ""
                )
            {
                Employee employee = new Employee(
                    lastName,
                    firstName,
                    middleName,
                    password,
                    dayOfBirth,
                    phone,
                    dateOfEmployment,
                    role
                );

                _employeeRepository.Create(employee);
                return true;
            }
            return false;
        }
        
        // Увольнение сотрудника
        public bool DismissalEmployee(Employee employee, DateTime? dataOfDismissal)
        {
            if (employee != null && dataOfDismissal != null)
            {
                employee.DateOfDismissal = dataOfDismissal;
                _employeeRepository.Update(employee);
                return true;
            }
            return false;
        }

        // Генерация случайного пароля
        public string PasswordGeneration(int length = 8)
        {
            string password = "";
            string[] arr = { 
                "1", "2", "3", "4", "5", "6", "7", "8", "9", 
                "B", "C", "D", "F", "G", "H", "J", "K", "L", 
                "M", "N", "P", "Q", "R", "S", "T", "V", "W", 
                "X", "Z", "b", "c", "d", "f", "g", "h", "j", 
                "k", "m", "n", "p", "q", "r", "s", "t", "v", 
                "w", "x", "z", "A", "E", "U", "Y", "a", "e", 
                "i", "o", "u", "y" 
            };
            Random random = new Random();
            for (int i = 0; i < length; i = i + 1)
            {
                password = password + arr[random.Next(0, arr.Length - 1)];
            }
            return password;
        }

        // Проверка равенства пароля пользователя и полученного пароля
        public bool CheckPassword(Employee employee, string password)
        {
            if (employee == null)
            {
                return false;
            }
            return employee.Password == password;
        }

        public void ChangePassword(Employee employee, string password)
        {
            
        }

    }
}
