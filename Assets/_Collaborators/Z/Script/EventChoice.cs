using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class EventChoice : MonoBehaviour {
        public bool TriggerSequence = true;
        [TextArea]
        public string Content;
        public string EffectText;

        public virtual void Effect(List<Character> Characters, out Event AddEvent)
        {
            AddEvent = null;
        }

        public virtual bool Pass(Pair P)
        {
            return true;
        }

        public string GetContent()
        {
            return Content;
        }
    }
}