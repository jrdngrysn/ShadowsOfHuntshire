using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class ChoiceEffect_Next : ChoiceEffect {
        public Event DisableEvent;
        public Event ActiveEvent;

        public override void Effect(List<Character> Characters, out Event AddEvent)
        {
            AddEvent = null;
            if (DisableEvent)
                DisableEvent.Active = false;
            if (ActiveEvent)
                ActiveEvent.Active = true;
        }
    }
}