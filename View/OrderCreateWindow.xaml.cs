using Entities;
using System;
using System.Windows;
using Services;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace Views
{
    public partial class OrderCreateWindow : Window
    {
        private readonly OrderService orderService;
        private readonly PlayPlaceService playPlaceService;
        private readonly InventoryItemService inventoryItemService;

        private Client client;
        private Employee employee;
        private List<Order> orders;

        public OrderCreateWindow(Client client, Employee employee)
        {
            InitializeComponent();

            this.client = client;
            this.employee = employee;

            orderService = new OrderService();
            playPlaceService = new PlayPlaceService();
            inventoryItemService = new InventoryItemService();         

            Loaded += OrderCreateWindow_Loaded;
        }

        private void OrderCreateWindow_Loaded(object sender, RoutedEventArgs e)
        {
            clientInfo.Text = $"{client.LastName} {client.FirstName} {client.MiddleName}\n{client.Phone} {client.Organization}";
            employeeInfo.Text = $"{employee.LastName} {employee.FirstName} {employee.MiddleName}\n{employee.RoleName}";

            dateOrder.DisplayDateStart = DateTime.Now;
            dateOrder.SelectedDate = DateTime.Now;

            orders = orderService.GetOrderByDate((DateTime)dateOrder.SelectedDate);

            playPlacesListView.ItemsSource = playPlaceService.GetCurrentPlayPlaceSortByType();
            inventoryListView.ItemsSource = inventoryItemService.GetCurrentInventoryItemsSortByTypeAndName();
        }

        private void dateOrder_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                orders = orderService.GetOrderByDate((DateTime)dateOrder.SelectedDate);
            }
            catch (Exception ex)
            {
                dateOrder.SelectedDate = DateTime.Now; 
            }
        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            if (IsCorrectTime())
            {
                TimeSpan timeStart = new TimeSpan(Convert.ToInt32(hourStartTimeField.Text), Convert.ToInt32(minuteStartTimeField.Text), 0);
                TimeSpan timeEnd = new TimeSpan(Convert.ToInt32(hourEndTimeField.Text), Convert.ToInt32(minuteEndTimeField.Text), 0);

                InventoryItem[] inventoryItems = new InventoryItem[inventoryListView.SelectedItems.Count];
                inventoryListView.SelectedItems.CopyTo(inventoryItems, 0);

                if ((PlayPlace)playPlacesListView.SelectedItem != null || playPlacesListView.SelectedItems.Count > 1)
                {
                    if (orders.Where(o => o.PlayPlace.Equals((PlayPlace)playPlacesListView.SelectedItem)).Count() == 0)
                    {
                        orderService.CreateOrder(
                            client, 
                            employee,
                            (PlayPlace)playPlacesListView.SelectedItem,
                            inventoryItems.ToList(),
                            ((DateTime)dateOrder.SelectedDate).Date.Add(timeStart),
                            ((DateTime)dateOrder.SelectedDate).Date.Add(timeStart).Add(timeEnd)
                        );
                        MessageBox.Show($"Заказ добавлен");
                        this.Close();
                    }
                    else
                    {
                        List<Order> ordersSelectesPlayPlace = orders.Where(o => o.PlayPlace.Equals((PlayPlace)playPlacesListView.SelectedItem)).ToList();
                        if (IsFreeTime(ordersSelectesPlayPlace, timeStart, timeEnd))
                        {
                            orderService.CreateOrder(
                                client,
                                employee,
                                (PlayPlace)playPlacesListView.SelectedItem,
                                inventoryItems.ToList(),
                                ((DateTime)dateOrder.SelectedDate).Date.Add(timeStart),
                                ((DateTime)dateOrder.SelectedDate).Date.Add(timeStart).Add(timeEnd)
                            );
                            MessageBox.Show($"Заказ добавлен");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Время попадает на занятый интервал");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Выберите одно игровое поле игровое поле");
                }
            }
            else
            {
                MessageBox.Show("Не корректное время");
            }
        }

        private bool IsCorrectTime()
        {
            if (hourStartTimeField.Text.All(char.IsDigit) && hourStartTimeField.Text != "" &&
                minuteStartTimeField.Text.All(char.IsDigit) && minuteStartTimeField.Text != "" &&
                hourEndTimeField.Text.All(char.IsDigit) && hourEndTimeField.Text != "" &&
                minuteEndTimeField.Text.All(char.IsDigit) && minuteEndTimeField.Text != "")
            {
                int hourStart = Convert.ToInt32(hourStartTimeField.Text);
                int minuteStart = Convert.ToInt32(minuteStartTimeField.Text);
                int hourEnd = Convert.ToInt32(hourEndTimeField.Text);
                int minuteEnd = Convert.ToInt32(minuteEndTimeField.Text);

                if (hourStart >= 0 && hourStart <= 24 &&
                    minuteStart >= 0 && minuteStart <= 60 &&
                    hourEnd >= 0 && hourEnd <= 24 &&
                    minuteEnd >= 0 && minuteEnd <= 60)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsFreeTime(List<Order> ordersSelectesPlayPlace, TimeSpan timeStart, TimeSpan timeEnd)
        {
            foreach (Order order in ordersSelectesPlayPlace)
            {
                if (order.TimeStart.TimeOfDay > timeStart && order.TimeStart.TimeOfDay >= (timeStart + timeEnd) ||
                    order.TimeEnd.TimeOfDay <= timeStart && order.TimeEnd.TimeOfDay < (timeStart + timeEnd))
                {
                    return true;
                }
            }
            return false;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
