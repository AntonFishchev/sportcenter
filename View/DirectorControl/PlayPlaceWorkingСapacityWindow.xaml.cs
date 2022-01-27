using Entities;
using Services;
using System.Windows;

namespace Views
{
    public partial class PlayPlaceWorkingСapacityWindow : Window
    {
        private readonly PlayPlaceService playPlaceService = new PlayPlaceService();

        public PlayPlaceWorkingСapacityWindow()
        {
            InitializeComponent();

            Loaded += PlayPlaceWorkingСapacityWindow_Loaded;
        }

        private void PlayPlaceWorkingСapacityWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshWorkingСapacityPlayPlace();
        }

        // Обновление списков работоспособных и неработоспособных игровых площадок
        private void RefreshWorkingСapacityPlayPlace()
        {
            nameWorkingСapacityClosePlayPlaceField.ItemsSource = playPlaceService.GetWorkingСapacityPlayPlace();
            nameWorkingСapacityOpenPlayPlaceField.ItemsSource = playPlaceService.GetNonWorkingСapacityPlayPlace();
        }

        // Возврат игровой площадки с ремонта
        private void btnOpenDone_Click(object sender, RoutedEventArgs e)
        {
            PlayPlace playPlace = (PlayPlace)nameWorkingСapacityOpenPlayPlaceField.SelectedItem;
            if (playPlace != null)
            {
                playPlaceService.WorkingСapacityOnPlayPlace(playPlace);
                MessageBox.Show($"{playPlace.Name} возвращен(а) с ремонта");
                this.Close();
                RefreshWorkingСapacityPlayPlace();                
            }
            else
            {
                MessageBox.Show("Выберете игровую площадку");
            }
        }

        // Отправка игровой площадки на ремонт
        private void btnСloseDone_Click(object sender, RoutedEventArgs e)
        {
            PlayPlace playPlace = (PlayPlace)nameWorkingСapacityClosePlayPlaceField.SelectedItem;
            if (playPlace != null)
            {
                playPlaceService.WorkingСapacityOffPlayPlace(playPlace);
                MessageBox.Show($"{playPlace.Name} отправлен(а) на ремонт");
                this.Close();
                RefreshWorkingСapacityPlayPlace();
            }
            else
            {
                MessageBox.Show("Выберете игровую площадку");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
