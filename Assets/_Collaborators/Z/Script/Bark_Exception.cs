using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class Bark_Exception : Bark {
        [Space]
        public string ExceptionKey;
        public string ExceptionKeyII;
        public int CurrentSpecialBarkIndex;
        public List<string> SpecialBarks;

        public override void Effect()
        {
            if (C.GetPair() && C.GetPartner().GetName() != ExceptionKey && C.GetPartner().GetName() != ExceptionKeyII)
            {
                CurrentSpecialBarkIndex++;
                if (CurrentSpecialBarkIndex >= SpecialBarks.Count)
                    CurrentSpecialBarkIndex = 0;
                GlobalControl.Main.Bark(SpecialBarks[CurrentSpecialBarkIndex]);
            }
            else
            {
                CurrentBarkIndex++;
                if (CurrentBarkIndex >= Barks.Count)
                    CurrentBarkIndex = 0;
                GlobalControl.Main.Bark(GetBark());
            }
        }
    }
}