using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Entities.Abstract
{
    public abstract class Human : BaseEntity
    {
        #region Fields

        [Required]
        [Column("lastName", TypeName = "varchar(100)")]
        protected string lastName;

        [Required]
        [Column("firstName", TypeName = "varchar(100)")]
        protected string firstName;

        [Column("middleName", TypeName = "varchar(100)")]
        protected string? middleName;

        [Required]
        [Column("phone", TypeName = "varchar(25)")]
        protected string phone;

        #endregion

        #region Getters/Setters

        public string LastName
        {
            get => lastName;
            set
            {
                if (value != null && value.Length != 0 && value.All(char.IsLetter))
                {
                    if (char.IsUpper(value, 0))
                    {
                        this.lastName = value;
                        return;
                    }
                    else
                    {
                        this.lastName = value.Substring(0, 1).ToUpper() + value.Substring(1);
                    }

                }
            }
        }

        public string FirstName
        {
            get => firstName;
            set
            {
                if (value != null && value.Length != 0 && value.All(char.IsLetter))
                {
                    if (char.IsUpper(value, 0))
                    {
                        this.firstName = value;
                        return;
                    }
                    else
                    {
                        this.firstName = value.Substring(0, 1).ToUpper() + value.Substring(1);
                    }

                }
            }
        }

        public string? MiddleName
        {
            get => middleName;
            set
            {
                if (value != null && value.Length != 0 && value.All(char.IsLetter))
                {
                    if (char.IsUpper(value, 0))
                    {
                        this.middleName = value;
                        return;
                    }
                    else
                    {
                        this.middleName = value.Substring(0, 1).ToUpper() + value.Substring(1);
                    }

                }
            }
        }

        public string Phone
        {
            get => phone;
            set
            {
                if (value != null && value.Length != 0 && value.All(char.IsDigit))
                {
                    this.phone = value;
                }
            }
        }

        #endregion
    }
}
