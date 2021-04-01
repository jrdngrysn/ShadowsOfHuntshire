using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class EventChoice_StatChange_Source : EventChoice {
        public Vector3Int ChangeA;

        public override void Effect(List<Character> Characters, out Event AddEvent)
        {
            AddEvent = null;
            foreach (Character C in Characters)
            {
                if (C.Name != GetComponent<Event>().Source)
                    continue;
                C.ChangeVitality(ChangeA.x);
                C.ChangePassion(ChangeA.y);
                C.ChangeReason(ChangeA.z);
            }
        }
    }
}