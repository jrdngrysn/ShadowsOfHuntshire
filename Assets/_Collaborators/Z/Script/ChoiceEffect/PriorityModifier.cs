using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class PriorityModifier : MonoBehaviour {

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public virtual int GetPriority(int Value, Character Source, Pair P)
        {
            return Value;
        }
    }
}