using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class TriggerCondtion_TownEvent : TriggerCondition {
        public bool Positive;

        public override bool Pass()
        {
            return GlobalControl.Main.TownEventActive == Positive;
        }
    }
}