using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class ChoiceEffect_Disappear : ChoiceEffect {
        public string Key;
        public int Time;

        public override void Effect(List<Character> Characters, out Event AddEvent)
        {
            AddEvent = null;
            for (int i = Characters.Count - 1; i >= 0; i--)
            {
                if (Characters[i].Name == Key)
                    Characters[i].MissingDelay(Time);
            }
        }
    }
}