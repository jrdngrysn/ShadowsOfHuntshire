using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class ChoiceEffect_EndingTrigger : ChoiceEffect {
        public string Key;

        public override void Effect(List<Character> Characters, out Event AddEvent)
        {
            KeyBase.Main.ChangeKey(Key, 1);
            base.Effect(Characters, out AddEvent);
        }
    }
}