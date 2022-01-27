using Entities.Abstract;
using System;
using System.Collections.Generic;

namespace Entities
{
    public class Penalty : BaseEntity
    {
        public Client client { get; set; }
        public Employee employee { get; set; }

        public List<InventoryItem> inventoryList { get; set; }

        public DateTime date { get; set; }

        public double totalPrice { get; set; } = 0;
        public bool paymentStatus { get; set; } = false;
    }
}
