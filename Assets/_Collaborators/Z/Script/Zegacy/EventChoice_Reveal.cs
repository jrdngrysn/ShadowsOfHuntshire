using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class EventChoice_Reveal : EventChoice {

        public override void Effect(List<Character> Characters, out Event AddEvent)
        {
            AddEvent = null;
            foreach (Character C in Characters)
            {
                C.SetHidden_Vitality(false);
                C.SetHidden_Passion(false);
                C.SetHidden_Reason(false);
            }
        }
    }
}