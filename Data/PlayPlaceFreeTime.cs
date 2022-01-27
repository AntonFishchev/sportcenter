using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class PlayPlaceFreeTime
    {
        public PlayPlace PlayPlace { get; set; }

        public string MessageFreeTime { get; set; }

        public PlayPlaceFreeTime(PlayPlace playPlace, string messageFreeTime)
        {
            PlayPlace = playPlace;
            
            if (!playPlace.WorkingСapacity)
            {
                MessageFreeTime = "Закрыт на ремонт";
            }
            else
            {
                MessageFreeTime = messageFreeTime;
            }
        }
    }
}
