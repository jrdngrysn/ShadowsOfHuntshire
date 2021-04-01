using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class ChoiceEffect_Missing : ChoiceEffect {
        public string Source;
        public bool OnlySource;
        public bool OnlyNonSource;

        public override void Effect(List<Character> Characters, out Event AddEvent)
        {
            AddEvent = null;
            foreach (Character C in Characters)
            {
                if (OnlySource && C.Name != Source)
                    continue;
                if (OnlyNonSource && C.Name == Source)
                    continue;
                C.Missing();
            }
        }
    }
}