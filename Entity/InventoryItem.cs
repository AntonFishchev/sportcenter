using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("inventory_items")]
    public class InventoryItem : BaseEntity, IPaymentItem
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
        [Column("price_of_penalty", TypeName = "numeric(10, 2)")]
        public double PriceOfPenalty { get; set; }

        [Required]
        [Column("date_of_price_setting", TypeName = "timestamp")]
        public DateTime DateOfPriceSetting { get; set; } = DateTime.Now;

        [Column("date_of_writeoff", TypeName = "timestamp")]
        public DateTime? DateOfWriteOff { get; set; } = null;

        [Required]
        [Column("working_capacity", TypeName = "boolean")]
        public bool WorkingСapacity { get; set; } = true;

        
        public List<Order> Orders { get; set; }
        public List<Penalty> Penaltyes { get; set; }

        public string TypeItem { get; set; }

        public InventoryItem(string name, string type, double price, double priceOfPenalty)
        {
            Name = name;
            Type = type;
            Price = price;
            PriceOfPenalty = priceOfPenalty;
        }

        public InventoryItem(string name, string type, double price, double priceOfPenalty, bool workingСapacity) : this(name, type, price, priceOfPenalty)
        {
            WorkingСapacity = workingСapacity;
        }
    }
}
