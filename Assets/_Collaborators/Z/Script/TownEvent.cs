using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class TownEvent : Event {
        [Space]
        public int TriggerTime;

        public bool CanTrigger()
        {
            return GlobalControl.Main.CurrentTime == TriggerTime;
        }
    }
}