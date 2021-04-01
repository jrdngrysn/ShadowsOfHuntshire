using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class ChoiceEffect_StatChange_All : ChoiceEffect {
        public Vector3Int Change;

        public override void Effect(List<Character> Characters, out Event AddEvent)
        {
            AddEvent = null;
            foreach (Character C in GlobalControl.Main.Characters)
            {
                if (!C.Active)
                    continue;
                C.ChangeVitality(-Change.x);
                C.ChangePassion(-Change.y);
                C.ChangeReason(-Change.z);
            }
        }
    }
}