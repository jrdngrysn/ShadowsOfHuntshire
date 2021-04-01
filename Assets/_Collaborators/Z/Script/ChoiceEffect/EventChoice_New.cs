using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class EventChoice_New : EventChoice {
        public List<ChoiceEffect> Effects;
        public Event DAE;

        public override void Effect(List<Character> Characters, out Event AddEvent)
        {
            AddEvent = null;
            foreach (ChoiceEffect CE in Effects)
            {
                Event E = null;
                CE.Effect(Characters, out E);
                if (E)
                    AddEvent = E;
            }
            if (DAE)
                AddEvent = DAE;
        }
    }
}