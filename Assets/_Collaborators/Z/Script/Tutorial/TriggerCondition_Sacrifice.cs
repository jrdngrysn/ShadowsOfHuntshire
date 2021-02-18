using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class TriggerCondition_Sacrifice : TriggerCondition {

        public override bool Pass()
        {
            return GlobalControl.Main.GetSacrificeActive();
        }
    }
}