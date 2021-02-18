using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class TriggerCondition : MonoBehaviour {

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public virtual bool Pass()
        {
            return false;
        }
    }
}