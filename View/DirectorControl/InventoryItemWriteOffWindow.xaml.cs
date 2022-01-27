using Entities;
using Services;
using System.Windows;

namespace Views
{
    public partial class InventoryItemWriteOffWindow : Window
    {
        private readonly InventoryItemService inventoryItemService = new InventoryItemService();
        public InventoryItemWriteOffWindow()
        {
            InitializeComponent();

            Loaded += InventoryItemWriteOffWindow_Loaded;
        }

        private void InventoryItemWriteOffWindow_Loaded(object sender, RoutedEventArgs e)
        {
            nameInventoryItemWriteOffField.ItemsSource = inventoryItemService.GetCurrentInventoryItems();
        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            InventoryItem inventoryItem = (InventoryItem)nameInventoryItemWriteOffField.SelectedItem;
            if(inventoryItem != null)
            {
                inventoryItemService.WriteOffInventoryItem(inventoryItem);
                MessageBox.Show($"{inventoryItem.Name} списан(а)");
                this.Close();
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
