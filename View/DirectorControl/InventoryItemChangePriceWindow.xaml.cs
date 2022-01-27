using Entities;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Views
{
    public partial class InventoryItemChangePriceWindow : Window
    {
        private readonly InventoryItemService inventoryItemService = new InventoryItemService();
        private readonly PlayPlaceService playPlaceService = new PlayPlaceService();

        public InventoryItemChangePriceWindow()
        {
            InitializeComponent();

            Loaded += InventoryItemChangePriceWindow_Loaded;
        }

        private void InventoryItemChangePriceWindow_Loaded(object sender, RoutedEventArgs e)
        {
            nameInventoryItemChangePriceField.ItemsSource = inventoryItemService.GetCurrentInventoryItems();
        }

        // Вывод старой цены выбранного предмета инвентаря
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            if (cmb.SelectedItem != null)
            {
                InventoryItem inventoryItem = (InventoryItem)cmb.SelectedItem;
                oldPriceInventoryItemChangePriceField.Content = inventoryItem.Price;
                oldPriceOfPenaltyInventoryItemChangePriceField.Content = inventoryItem.PriceOfPenalty;
            }
        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            InventoryItem oldInventoryItem = (InventoryItem)nameInventoryItemChangePriceField.SelectedItem;
            if (oldInventoryItem != null)
            {
                double price = playPlaceService.CorrectPrice(newPriceInventoryItemChangePriceField.Text);
                double priceOfPenalty = playPlaceService.CorrectPrice(newPriceOfPenaltyInventoryItemChangePriceField.Text);

                if(price != -1 && priceOfPenalty != -1)
                {
                    inventoryItemService.ChangePriceInvemtoryItem(oldInventoryItem, price, priceOfPenalty);
                }
                else
                {
                    MessageBox.Show("Введите цены корректно.\n" +
                                    "Допустимы только цифры и одна точка");
                }
            }
            else
            {
                MessageBox.Show("Выберите предмет инвентаря");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
