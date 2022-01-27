using Services;
using System.Windows;

namespace Views
{
    public partial class PlayPlaceRegisterWindow : Window
    {
        private readonly PlayPlaceService playPlaceService = new PlayPlaceService();

        public PlayPlaceRegisterWindow()
        {
            InitializeComponent();

            Loaded += PlayPlaceRegisterWindow_Loaded;
        }

        private void PlayPlaceRegisterWindow_Loaded(object sender, RoutedEventArgs e)
        {
            typePlayPlaceField.ItemsSource = playPlaceService.GetTypePlayPlace();
        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            double price = playPlaceService.CorrectPrice(pricePlayPlaceField.Text);
            if (price != -1)
            {
                if (namePlayPlaceField.Text != "" && typePlayPlaceField.SelectedItem.ToString() != "")
                {
                    playPlaceService.RegisterPlayPlace(namePlayPlaceField.Text, typePlayPlaceField.Text, price);
                    MessageBox.Show($"{namePlayPlaceField.Text} добавлен(а)");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Введены не все поля");
                }
            }
            else
            {
                MessageBox.Show("Введите цену корректно.\n" +
                                "Допустимы только цифры и одна точка");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
