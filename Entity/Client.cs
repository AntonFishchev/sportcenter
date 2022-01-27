using Entities.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("clients")]
    public class Client : Human
    {
        #region Fields

        [Required]
        [Column("organization", TypeName = "varchar(100)")]
        private string organization = "Частное лицо";

        [Required]
        [Column("black_list", TypeName = "boolean")]
        private bool blackList = false;

        private List<Order> orders;

        #endregion

        #region Getters/Setters
        
        public string Organization
        {
            get => organization;
            set
            {
                if (value != null && value.Length != 0)
                {
                    this.organization = value;
                }
            }
        }

        public bool BlackList
        {
            get => blackList;
            set => this.blackList = value;
        }

        public List<Order> Orders
        {
            get => orders;
            set => this.orders = value;
        }

        #endregion

        #region Constructors

        public Client(string? lastName, string? firstName, string phone)
        {
            LastName = lastName;
            FirstName = firstName;
            Phone = phone;
        }

        public Client(string? lastName, string? firstName, string middleName, string phone, string organization) : this(lastName, firstName, phone)
        {
            MiddleName = middleName;
            Organization = organization;
        }

        #endregion
    }
}
