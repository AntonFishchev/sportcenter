using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using static Data.Enums;

namespace Services
{
    public class PlayPlaceService
    {
        private readonly GenericRepository<PlayPlace> _playPlaceRepository;

        public PlayPlaceService()
        {
            _playPlaceRepository = new GenericRepository<PlayPlace>();
        }

        // Добавление новой игровой площадки.
        public void RegisterPlayPlace(string name, string type, double price)
        {
            PlayPlace playPlace = new PlayPlace(name, type, price);
            _playPlaceRepository.Create(playPlace);
        }

        // Проверка строки с ценой на корректность.
        // Если в строке встречается только одна точка и все остальные символы это цифры,
        // то возвращаем цену в типе double. Иначе возвращаем -1.
        public double CorrectPrice(string price)
        {
            if (price == null || price == "")
            {
                return -1;
            }

            price = price.Replace('.', ',');
            char[] chars = price.ToCharArray();
            bool dot = false;

            for (int i = 0; i < chars.Length; i++)
            {
                // Проверяем на точки
                if (chars[i] == ',')
                {
                    // Если точка встречается впервые, указать флаг
                    if (!dot)
                    {
                        dot = true;
                    }
                    // Если точка уже встречалась вернуть -1
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    // Если символ не точка проверяем, что это не число и выбрасываем -1 
                    if (!char.IsDigit(chars[i]))
                    {
                        return -1;
                    }
                }
            }

            return Math.Round(Convert.ToDouble(price), 2);
        }

        // Получает все актуальные игровые площадки
        public List<PlayPlace> GetCurrentPlayPlaces()
        {
            List<PlayPlace> list = new List<PlayPlace>();
            foreach (PlayPlace playPlace in _playPlaceRepository.GetAll())
            {
                if (playPlace.DateOfWriteOff == null)
                {
                    list.Add(playPlace);
                }
                
            }
            return list;
        }

        // Получение всех типов игровый площадок
        public List<string> GetTypePlayPlace()
        {
            List<string> list = new List<string>();
            foreach (string playPlace in Enum.GetNames(typeof(PlayPlaceType)))
            {
                if (playPlace.Contains('_'))
                {
                    list.Add(playPlace.Replace('_', ' '));
                }
                else
                {
                    list.Add(playPlace);
                }
            }
            return list;
        }

        // Получение всех работоспособных игровых площадок
        public List<PlayPlace> GetWorkingСapacityPlayPlace()
        {
            List<PlayPlace> list = new List<PlayPlace>();
            foreach (PlayPlace playPlace in GetCurrentPlayPlaces())
            {
                if (playPlace.WorkingСapacity)
                {
                    list.Add(playPlace);
                }
            }
            return list;
        }

        // Получение всех неработоспособных игровых площадок
        public List<PlayPlace> GetNonWorkingСapacityPlayPlace()
        {
            List<PlayPlace> list = new List<PlayPlace>();
            foreach (PlayPlace playPlace in GetCurrentPlayPlaces())
            {
                if (!playPlace.WorkingСapacity)
                {
                    list.Add(playPlace);
                }
            }
            return list;
        }

        // Игровая плащадка работоспособна 
        public void WorkingСapacityOnPlayPlace(PlayPlace playPlace)
        {
             playPlace.WorkingСapacity = true;
            _playPlaceRepository.Update(playPlace);
        }

        // Игровая плащадка неработоспособна 
        public void WorkingСapacityOffPlayPlace(PlayPlace playPlace)
        {
            playPlace.WorkingСapacity = false;
            _playPlaceRepository.Update(playPlace);
        }

        // Изменение цены игровой площадки
        // PS. Цена площадки не меняется, а создается новая с теми же данными, но новой ценой
        //     Нужно для истории клиента
        public void ChangePricePlayPLace(PlayPlace oldPlayPlace, double newPrice)
        {
            PlayPlace newPlayPlace = new PlayPlace(oldPlayPlace.Name, oldPlayPlace.Type, newPrice, oldPlayPlace.WorkingСapacity);
            oldPlayPlace.DateOfWriteOff = DateTime.Now;
            _playPlaceRepository.Create(newPlayPlace);
            _playPlaceRepository.Update(oldPlayPlace);
        }

        public List<PlayPlace>[] GetCurrentPlayPlaceByType()
        {
            List<PlayPlace> playPlaces = GetCurrentPlayPlaces();

            List<PlayPlace> playPlacesFootball = new List<PlayPlace>();
            List<PlayPlace> playPlacesTennis = new List<PlayPlace>();
            List<PlayPlace> playPlacesVolleyball = new List<PlayPlace>();
            List<PlayPlace> playPlacesBasketball = new List<PlayPlace>();

            foreach (PlayPlace playPlace in playPlaces)
            {
                switch (playPlace.Type)
                {
                    case "Футбол":
                        playPlacesFootball.Add(playPlace);
                        break;
                    case "Большой теннис":
                        playPlacesTennis.Add(playPlace);
                        break;
                    case "Воллейбол":
                        playPlacesVolleyball.Add(playPlace);
                        break;
                    case "Баскетбол":
                        playPlacesBasketball.Add(playPlace);
                        break;
                }
            }

            List<PlayPlace>[] result = new List<PlayPlace>[]
            {
                playPlacesFootball, playPlacesTennis, playPlacesVolleyball, playPlacesBasketball
            };

            return result;
        }

        public List<PlayPlace> GetCurrentPlayPlaceSortByType()
        {
            return GetCurrentPlayPlaces().OrderBy(x => x.Type).ToList();
        }

        public List<PlayPlaceFreeTime> GetPlayPlaceFreeTime(List<Order> orders, List<PlayPlace> currentPlayPlaces)
        {
            List<PlayPlaceFreeTime> resultList = new List<PlayPlaceFreeTime>();

            foreach (PlayPlace playPlace in currentPlayPlaces)
            {
                Order[] orderList = orders
                    .Where(o => o.PlayPlace.Equals(playPlace))
                    .OrderBy(o => o.TimeStart)
                    .ToArray();

                if (orderList.Length == 0)
                {
                    resultList.Add(new PlayPlaceFreeTime(playPlace, "Весь день свободен"));
                }
                else
                {
                    string message = "Занято ";
                    foreach (Order order in orderList)
                    {
                        message += $"c {order.TimeStart.ToString("t")} до {order.TimeEnd.ToString("t")}, ";
                    }
                    message = message.Substring(0, message.Length - 2) + ".";
                    resultList.Add(new PlayPlaceFreeTime(playPlace, message));
                }
            }

            return resultList.OrderBy(o => o.PlayPlace.Type).ThenBy(o => o.PlayPlace.Name).ToList();
        }
    }
}
