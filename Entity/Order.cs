using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    [Table("orders")]
    public class Order : BaseEntity
    {
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [ForeignKey("PlayPlace")]
        public int PlayPlaceId { get; set; }
        public PlayPlace PlayPlace { get; set; }

        //[NotMapped]
        //public ICollection<IPaymentItem> Items { get; set; }

        public List<InventoryItem> InventoryList { get; set; } = null;

        [Required]
        [Column("time_start", TypeName = "timestamp")]
        public DateTime TimeStart { get; set; }

        [Required]
        [Column("time_end", TypeName = "timestamp")]
        public DateTime TimeEnd { get; set; }

        [Required]
        [Column("total_price", TypeName = "numeric(10,2)")]
        public double TotalPrice { get; set; } = 0;

        [Required]
        [Column("payment_status", TypeName = "boolean")]
        public bool PaymentStatus { get; set; } = false;

        #region Constructors

        public Order()
        {
        }
        
        public Order(Client client, Employee employee, PlayPlace playPlace, List<InventoryItem> inventoryList, DateTime timeStart, DateTime timeEnd)
        {
            Client = client;
            Employee = employee;
            PlayPlace = playPlace;
            InventoryList = inventoryList;
            TimeStart = timeStart;
            TimeEnd = timeEnd;

            TotalPrice = CalculationTotalPrice((timeEnd - timeStart).TotalMinutes, playPlace, inventoryList);
        }

        public Order(Client client, Employee employee, PlayPlace playPlace, List<InventoryItem> inventoryList, DateTime timeStart, DateTime timeEnd, double totalPrice, bool paymentStatus) : this( client,  employee,  playPlace,  inventoryList,  timeStart,  timeEnd)
        {
            TotalPrice = totalPrice;
            PaymentStatus = paymentStatus;
        }

        #endregion

        #region Methods

        private double CalculationTotalPrice(double timeInMinutes, PlayPlace playPlace, ICollection<InventoryItem> inventoryItems)
        {
            double totalPrice = timeInMinutes / 60 * playPlace.Price;
            foreach(InventoryItem item in inventoryItems)
            {
                totalPrice += timeInMinutes / 60 * item.Price;
            }
            return totalPrice;
        }

        public override bool Equals(object obj)
        {
            return obj is Order order &&
                   Id == order.Id &&
                   ClientId == order.ClientId &&
                   EqualityComparer<Client>.Default.Equals(Client, order.Client) &&
                   EmployeeId == order.EmployeeId &&
                   EqualityComparer<Employee>.Default.Equals(Employee, order.Employee) &&
                   PlayPlaceId == order.PlayPlaceId &&
                   EqualityComparer<PlayPlace>.Default.Equals(PlayPlace, order.PlayPlace) &&
                   EqualityComparer<List<InventoryItem>>.Default.Equals(InventoryList, order.InventoryList) &&
                   TimeStart == order.TimeStart &&
                   TimeEnd == order.TimeEnd &&
                   TotalPrice == order.TotalPrice &&
                   PaymentStatus == order.PaymentStatus;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(ClientId);
            hash.Add(Client);
            hash.Add(EmployeeId);
            hash.Add(Employee);
            hash.Add(PlayPlaceId);
            hash.Add(PlayPlace);
            hash.Add(InventoryList);
            hash.Add(TimeStart);
            hash.Add(TimeEnd);
            hash.Add(TotalPrice);
            hash.Add(PaymentStatus);
            return hash.ToHashCode();
        }

        #endregion


        //public Order(Client client, Employee employee, ICollection<IPaymentItem> items, DateTime timeStart, DateTime timeEnd)
        //{
        //    Client = client;
        //    Employee = employee;
        //    Items = items;

        //    TimeStart = timeStart;
        //    TimeEnd = timeEnd;

        //    TotalPrice = CalculationTotalPrice((timeEnd - timeStart).TotalMinutes, items);
        //}

        //private double CalculationTotalPrice(double timeInMinutes, ICollection<IPaymentItem> items)
        //{
        //    double totalPrice = 0;
        //    foreach (IPaymentItem item in items)
        //    {
        //        totalPrice += timeInMinutes / 60 * item.Price;
        //    }
        //    return totalPrice;
        //}

    }
}
