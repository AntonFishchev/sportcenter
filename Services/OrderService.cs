using Data;
using Data.Repository;
using Entities;
using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;

        public OrderService()
        {
            _orderRepository = new OrderRepository();
        }

        //public List<Order>[] GetOrderByDatenoooooo(DateTime date)
        //{
        //    List<Order> orders = _orderRepository.GetAllOrdersByDate(date);
        //    List<Order> ordersFootbal = new List<Order>();
        //    List<Order> ordersTennis = new List<Order>();
        //    List<Order> ordersVolleyball = new List<Order>();
        //    List<Order> ordersBasketball = new List<Order>();

        //    foreach (Order order in orders)
        //    {
        //        switch (order.PlayPlace.Type)
        //        {
        //            case "Футбол":
        //                ordersFootbal.Add(order);
        //                break;
        //            case "Большой теннис":
        //                ordersTennis.Add(order);
        //                break;
        //            case "Воллейбол":
        //                ordersVolleyball.Add(order);
        //                break;
        //            case "Баскетбол":
        //                ordersBasketball.Add(order);
        //                break;
        //        }
        //    }

        //    List<Order>[] result = new List<Order>[]
        //    {
        //        ordersFootbal, ordersTennis, ordersVolleyball, ordersBasketball
        //    };
        //    return result;
        //}

        public List<Order> GetOrderByDate(DateTime date)
        {
            return _orderRepository.GetAllOrdersByDate(date);
        }

        public List<Order> GetOrdersByClient(Client client)
        {
            return _orderRepository.GetAllOrdersByClient(client).OrderByDescending(o => o.TimeStart.Date).ToList();
        }

        public void CreateOrder(
                Client client,
                Employee employee,
                PlayPlace playPlace,
                List<InventoryItem> inventoryItems,
                DateTime timeStart,
                DateTime timeEnd
            )
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                try
                {
                    context.clients.Attach(client);
                }
                catch (Exception ex)
                {

                }
                try
                {
                context.employees.Attach(employee);
                }
                catch (Exception ex)
                {

                }
                try
                {
                context.playPlaces.Attach(playPlace);
                }
                catch (Exception ex)
                {

                }
                foreach(InventoryItem inventoryItem in inventoryItems)
                {
                    try
                    {
                    context.inventoryItems.Attach(inventoryItem);
                    }
                    catch (Exception ex)
                    {

                    }

                }


                Order order = new Order(
                    client,
                    employee,
                    playPlace,
                    inventoryItems,
                    timeStart,
                    timeEnd
                );

                context.orders.Add(order);
                context.SaveChanges();
            }
            
        }

        public void PaymentOrder(Order order)
        {
            order.PaymentStatus = true;
            _orderRepository.Update(order);
        }
    }
}
