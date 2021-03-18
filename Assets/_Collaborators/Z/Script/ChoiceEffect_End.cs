using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class ChoiceEffect_End : ChoiceEffect {

        public override void Effect(List<Character> Characters, out Event AddEvent)
        {
            AddEvent = null;
            GlobalControl.Main.CurrentEndEvent = GlobalControl.Main.SacrificeEndEvent;
        }
    }
}