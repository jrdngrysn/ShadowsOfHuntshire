using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class ChoiceEffect_RandomChange : ChoiceEffect {
        public List<Event> AddEvents;
        public List<Vector3Int> StatChanges;

        public override void Effect(List<Character> Characters, out Event AddEvent)
        {
            int a = Random.Range(0, AddEvents.Count);
            foreach (Character C in Characters)
            {
                C.ChangeVitality(StatChanges[a].x);
                C.ChangePassion(StatChanges[a].y);
                C.ChangeReason(StatChanges[a].z);
            }
            AddEvent = AddEvents[a];
        }
    }
}