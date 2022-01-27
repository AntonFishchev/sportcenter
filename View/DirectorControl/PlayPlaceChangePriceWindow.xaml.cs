using Entities;
using Services;
using System.Windows;
using System.Windows.Controls;

namespace Views
{
    public partial class PlayPlaceChangePriceWindow : Window
    {
        private readonly PlayPlaceService playPlaceService = new PlayPlaceService();

        public PlayPlaceChangePriceWindow()
        {
            InitializeComponent();

            Loaded += PlayPlaceChangePriceWindow_Loaded;
        }

        private void PlayPlaceChangePriceWindow_Loaded(object sender, RoutedEventArgs e)
        {
            namePlayPlaceChangePriceField.ItemsSource = playPlaceService.GetCurrentPlayPlaces();
        }
        
        // Вывод старой цены выбранной игровой площадки
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            if (cmb.SelectedItem != null)
            {
                PlayPlace playPlace = (PlayPlace)cmb.SelectedItem;
                oldPricePlayPlaceChangePriceField.Content = playPlace.Price;
            }
        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            PlayPlace playPlace = (PlayPlace)namePlayPlaceChangePriceField.SelectedItem;
            if (playPlace != null)
            {
                double price = playPlaceService.CorrectPrice(newPricePlayPlaceChangePriceField.Text);
                if (price != -1)
                {
                    playPlaceService.ChangePricePlayPLace((PlayPlace)namePlayPlaceChangePriceField.SelectedItem, price);
                    MessageBox.Show($"Цена у \"{playPlace.Name}\" изменена.\n" +
                                    $"{playPlace.Price} -> {price}");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Введите цену корректно.\n" +
                                    "Допустимы только цифры и одна точка");
                }
            }
            else
            {
                MessageBox.Show("Выберите игровую площадку");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
