using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class OrderRepository : GenericRepository<Order>
    {
        public List<Order> GetAllOrdersByDate(DateTime date)
        {
            return _context.orders.Where(order => (order.TimeStart).Date.Equals(date.Date))
                .Include(order => order.Client)
                .Include(order => order.Employee)
                .Include(order => order.PlayPlace)
                .Include(order => order.InventoryList)
                .ToList();
        }

        public List<Order> GetAllOrdersByClient(Client client)
        {
            return _context.orders.Where(order => order.Client.Equals(client))
                .Include(order => order.Client)
                .Include(order => order.Employee)
                .Include(order => order.PlayPlace)
                .Include(order => order.InventoryList)
                .ToList();
        }

    }
}
