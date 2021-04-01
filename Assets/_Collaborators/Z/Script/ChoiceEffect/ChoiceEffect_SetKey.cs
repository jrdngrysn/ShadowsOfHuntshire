using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class ChoiceEffect_SetKey : ChoiceEffect {
        public string Key;
        public float Target;

        public override void Effect(List<Character> Characters, out Event AddEvent)
        {
            AddEvent = null;
            KeyBase.Main.SetKey(Key, Target);
        }
    }
}