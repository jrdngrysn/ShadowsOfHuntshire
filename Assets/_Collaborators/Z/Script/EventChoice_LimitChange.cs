using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class EventChoice_LimitChange : EventChoice {
        public Vector3Int ChangeA;

        public override void Effect(List<Character> Characters, out Event AddEvent)
        {
            AddEvent = null;
            foreach (Character C in GlobalControl.Main.Characters)
            {
                if (!C.Active)
                    continue;
                C.ChangeVitality(-ChangeA.x);
                C.ChangePassion(-ChangeA.y);
                C.ChangeReason(-ChangeA.z);
            }
        }
    }
}