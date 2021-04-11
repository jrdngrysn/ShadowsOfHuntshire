using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class EventChoice_Fish : EventChoice {
        public List<Event> AddEvents;

        public override void Effect(List<Character> Characters, out Event AddEvent)
        {
            if (KeyBase.Main.GetKey("FishIndex") < AddEvents.Count)
            {
                AddEvent = AddEvents[(int)KeyBase.Main.GetKey("FishIndex")];
                KeyBase.Main.ChangeKey("FishIndex", 1);
            }
            else
            {
                AddEvent = AddEvents[Random.Range(0, AddEvents.Count)];
            }
        }
    }
}