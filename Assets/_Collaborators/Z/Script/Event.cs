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
        public string RequiredKey;
        public int StartTime = -1;
        [TextArea]
        public string Content;
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
            if (GlobalControl.Main.CurrentTime < StartTime)
                return false;
            if (!P && FreeSources.Count <= 0)
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

        public string GetContent()
        {
            return Content;
        }
    }
}