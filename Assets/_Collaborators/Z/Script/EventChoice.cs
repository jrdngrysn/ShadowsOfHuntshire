using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class EventChoice : MonoBehaviour {
        [HideInInspector] public Event E;
        public bool TriggerSequence = true;
        [TextArea]
        public string Content;
        public string EffectText;

        public virtual void Awake()
        {
            E = GetComponentInParent<Event>();
        }

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