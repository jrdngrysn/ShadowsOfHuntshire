using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class Bark : MonoBehaviour {
        public Character C;
        public int CurrentBarkIndex;
        public List<string> Barks;

        // Start is called before the first frame update
        public virtual void Start()
        {

        }

        // Update is called once per frame
        public virtual void Update()
        {

        }

        public virtual bool CanBark()
        {
            return true;
        }

        public virtual void Effect()
        {
            CurrentBarkIndex++;
            if (CurrentBarkIndex >= Barks.Count)
                CurrentBarkIndex = 0;
            GlobalControl.Main.Bark(GetBark());
        }

        public virtual string GetBark()
        {
            return Barks[CurrentBarkIndex];
        }
    }
}