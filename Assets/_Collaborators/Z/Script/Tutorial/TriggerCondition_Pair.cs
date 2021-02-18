using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class TriggerCondition_Pair : TriggerCondition {

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override bool Pass()
        {
            return GlobalControl.Main.Pairs.Count > 0;
        }
    }
}