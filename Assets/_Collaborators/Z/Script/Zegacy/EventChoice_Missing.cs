using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class EventChoice_Missing : EventChoice_StatChange {

        public override void Effect(List<Character> Characters, out Event AddEvent)
        {
            base.Effect(Characters, out AddEvent);
            for (int i = Characters.Count - 1; i >= 0; i--)
            {
                if (Characters[i].Name != GetComponent<Event>().Source)
                    Characters[i].Missing();
            }
        }
    }
}