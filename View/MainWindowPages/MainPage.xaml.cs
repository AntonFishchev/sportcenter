using Entities;
using Services;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Views
{
    public partial class MainPage : Page
    {
        private readonly OrderService orderService;
        private readonly PlayPlaceService playPlaceService;

        private List<PlayPlace> playPlaces;


        public MainPage()
        {
            InitializeComponent();

            this.orderService = new OrderService();
            this.playPlaceService = new PlayPlaceService();

            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            chooseDate.SelectedDate = DateTime.Now;
            currentDate.Content = ((DateTime)chooseDate.SelectedDate).Date.ToString("D");

            RefreshPlayPlaceListView();
        }



        private void RefreshPlayPlaceListView()
        {
            playPlacesList.ItemsSource = playPlaceService.GetPlayPlaceFreeTime(
                    orderService.GetOrderByDate((DateTime)chooseDate.SelectedDate),
                    playPlaceService.GetCurrentPlayPlaces()
                );
        }

        private void chooseDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                currentDate.Content = ((DateTime)chooseDate.SelectedDate).Date.ToString("D");
            }
            catch (Exception ex)
            {
                chooseDate.SelectedDate = DateTime.Now;
                currentDate.Content = ((DateTime)chooseDate.SelectedDate).Date.ToString("D");
            }

            RefreshPlayPlaceListView();
        }
    }
}
