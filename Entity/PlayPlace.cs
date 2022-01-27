using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("play_places")]
    public class PlayPlace : BaseEntity
    {
        [Required]
        [Column("name", TypeName = "varchar(100)")]
        public string Name { get; set; }

        [Required]
        [Column("type", TypeName = "varchar(100)")]
        public string Type { get; set; }

        [Required]
        [Column("price", TypeName = "numeric(10, 2)")]
        public double Price { get; set; }

        [Required]
        [Column("date_of_price_setting", TypeName = "timestamp")]
        public DateTime DateOfPriceSetting { get; set; } = DateTime.Now;

        [Column("date_of_writeoff", TypeName = "timestamp")]
        public DateTime? DateOfWriteOff { get; set; } = null;

        [Required]
        [Column("working_capacity", TypeName = "boolean")]
        public bool WorkingСapacity { get; set; } = true;

        public List<Order> Orders { get; set; }

        public string TypeItem { get; set; }

        public PlayPlace(string name, string type, double price)
        {
            Name = name;
            Type = type;
            Price = price;
        }

        public PlayPlace(string name, string type, double price, bool workingСapacity) : this(name, type, price)
        {
            WorkingСapacity = workingСapacity;
        }


        public override bool Equals(object? obj)
        {
            return obj is PlayPlace place &&
                   Name == place.Name &&
                   Type == place.Type;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Type, Price, DateOfPriceSetting, DateOfWriteOff, WorkingСapacity);
        }
    }
}
