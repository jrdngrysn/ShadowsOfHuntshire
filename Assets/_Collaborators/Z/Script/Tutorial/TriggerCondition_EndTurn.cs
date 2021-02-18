using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class TriggerCondition_EndTurn : TriggerCondition {

        public override bool Pass()
        {
            return GlobalControl.Main.EndTurnAnim.Animating;
        }
    }
}