using Services;
using System.Windows;

namespace Views
{
    public partial class InventoryItemRegisterWindow : Window
    {
        private readonly InventoryItemService inventoryItemService = new InventoryItemService();
        private readonly PlayPlaceService playPlaceService = new PlayPlaceService();

        public InventoryItemRegisterWindow()
        {
            InitializeComponent();

            Loaded += InventoryItemRegisterWindow_Loaded;
        }

        private void InventoryItemRegisterWindow_Loaded(object sender, RoutedEventArgs e)
        {
            typePlayPlaceField.ItemsSource = playPlaceService.GetTypePlayPlace();
        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            if (typePlayPlaceField.SelectedItem != null && 
                nameInventoryItemField.Text != "" &&
                priceInventoryItemField.Text != "" && 
                priceOfPenaltyInventoryItemField.Text != "")
            {
                double price = playPlaceService.CorrectPrice(priceInventoryItemField.Text);
                double priceOfPenalty = playPlaceService.CorrectPrice(priceOfPenaltyInventoryItemField.Text);

                if (price != -1 && priceOfPenalty != -1)
                {
                    inventoryItemService.RegisterInventoryItem(nameInventoryItemField.Text, typePlayPlaceField.SelectedItem.ToString(), price, priceOfPenalty);
                    MessageBox.Show($"{nameInventoryItemField.Text} добавлен(а)");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Введите цены корректно.\n" +
                                    "Допустимы только цифры и одна точка");
                }
            }
            else
            {
                MessageBox.Show("Введите все поля");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(typePlayPlaceField.SelectedItem.ToString());
            this.Close();
        }
    }
}
