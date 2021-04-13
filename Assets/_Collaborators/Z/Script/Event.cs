using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class Event : MonoBehaviour {
        public string Title;
        public string Source;
        public List<string> FreeSources;
        [Space]
        public Event BaseEvent;
        public bool Active = true;
        public bool IgnorePairing;
        [HideInInspector] public string RequiredKey;
        [HideInInspector] public int StartTime = -1;
        public int Priority;
        public List<PriorityModifier> PMods;
        [TextArea]
        public string Content;
        public List<EventPage> Pages;
        public string AddContent;
        public bool DisplaySource;
        public List<EventChoice> Choices;

        // Start is called before the first frame update
        public void Start()
        {

        }

        // Update is called once per frame
        public void Update()
        {

        }
        
        public virtual bool Pass(Pair P)
        {
            if (!Active)
                return false;
            if (GlobalControl.Main.CurrentTime < StartTime)
                return false;
            if (!P && FreeSources.Count <= 0 && !IgnorePairing)
                return false;
            foreach (string s in FreeSources)
                if (!Character.Find(s) || !Character.Find(s).Active)
                    return false;
            if (Source != "" && !Character.Find(Source))
                return false;
            if (RequiredKey != "" && KeyBase.Main.GetKey(RequiredKey) <= 0)
                return false;
            return true;
        }

        public List<EventChoice> GetChoices()
        {
            return Choices;
        }

        public EventChoice GetChoice(int Index)
        {
            if (Choices.Count <= Index)
                return null;
            return Choices[Index];
        }

        public string GetTitle()
        {
            return Title;
        }

        public Character GetSource()
        {
            return Character.Find(Source);
        }

        public string GetContent(int Page)
        {
            if (!BaseEvent)
            {
                if (Pages.Count <= 0)
                    return Content;
                else
                    return Pages[Page].Content;
            }
            else
                return "<color=#ffffff00>" + BaseEvent.GetContent(BaseEvent.GetMaxPage()) + "</color>" + Content;
        }

        public int GetMaxPage()
        {
            if (Pages.Count <= 0)
                return 0;
            else
                return Pages.Count - 1;
        }

        public int GetPriority(Pair P)
        {
            int a = Priority;
            foreach (PriorityModifier PM in PMods)
                a = PM.GetPriority(a, GetSource(), P);
            return a;
        }
    }
}