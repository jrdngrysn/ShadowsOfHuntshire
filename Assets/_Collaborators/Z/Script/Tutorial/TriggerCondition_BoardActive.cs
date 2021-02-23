using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class TriggerCondition_BoardActive : TriggerCondition {
        public bool Positive;

        public override bool Pass()
        {
            return GlobalControl.Main.BoardActive == Positive;
        }
    }
}