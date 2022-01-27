using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("employees")]
    public class Employee : Human
    {
        #region Fields

        [Required]
        [Column("password", TypeName = "character varying(100)")]
        private string password;

        [Column("day_of_birth", TypeName = "timestamp")]
        private DateTime? dayOfBirth;

        [Required]
        [Column("date_of_employment", TypeName = "timestamp")]
        private DateTime? dateOfEmployment = DateTime.Now;

        [Column("date_of_dismissal", TypeName = "timestamp")]
        private DateTime? dateOfDismissal;

        [Required]
        [Column("roleName", TypeName = "varchar(100)")]
        private string roleName;

        private List<Order> orders;

        #endregion

        #region Getters/Setters

        public string Password
        {
            get => password;
            set
            {
                if (value != null && value.Length > 0)
                {
                    password = value;
                }
            }
        }

        public DateTime? DayOfBirth
        {
            get => dayOfBirth;
            set => dayOfBirth = value;
        }

        public DateTime? DateOfEmployment
        {
            get => dateOfEmployment;
            set
            {
                if (value != null)
                {
                    dateOfEmployment = value;
                }
            }
        }

        public DateTime? DateOfDismissal
        {
            get => dateOfDismissal;
            set => dateOfDismissal = value;
        }

        public string RoleName
        {
            get => roleName;
            set
            {
                if (value != null && value.Length > 0)
                {
                    this.roleName = value;
                }
            }
        }

        public List<Order> Orders
        {
            get => orders;
            set => orders = value;
        }

        #endregion

        #region Constructors

        public Employee(string lastName, string firstName, string? middleName, string password, DateTime? dayOfBirth, string phone, DateTime? dateOfEmployment, string roleName)
        {
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
            Password = password;
            DayOfBirth = dayOfBirth;
            Phone = phone;
            DateOfEmployment = dateOfEmployment;
            RoleName = roleName;
        }

        public Employee(string lastName, string firstName, string? middleName, string password, DateTime dayOfBirth, string phone, DateTime dateOfEmployment, DateTime? dateOfDismissal, string roleName)
        {
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
            Password = password;
            DayOfBirth = dayOfBirth;
            Phone = phone;
            DateOfEmployment = dateOfEmployment;
            DateOfDismissal = dateOfDismissal;
            RoleName = roleName;
        }

        #endregion

        #region Methods

        public override string? ToString()
        {
            return $"{Id} {LastName} {FirstName} {MiddleName} {RoleName} {Phone}";
        }

        public override bool Equals(object? obj)
        {
            return obj is Employee employee &&
                   Id == employee.Id &&
                   LastName == employee.LastName &&
                   FirstName == employee.FirstName &&
                   MiddleName == employee.MiddleName &&
                   Password == employee.Password &&
                   DayOfBirth == employee.DayOfBirth &&
                   Phone == employee.Phone &&
                   DateOfEmployment == employee.DateOfEmployment &&
                   DateOfDismissal == employee.DateOfDismissal &&
                   RoleName == employee.RoleName;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(LastName);
            hash.Add(FirstName);
            hash.Add(MiddleName);
            hash.Add(Password);
            hash.Add(DayOfBirth);
            hash.Add(Phone);
            hash.Add(DateOfEmployment);
            hash.Add(DateOfDismissal);
            hash.Add(RoleName);
            return hash.ToHashCode();
        }

        #endregion 
    }
}
