using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class ChoiceEffect : MonoBehaviour {

        // Start is called before the first frame update
        public virtual void Start()
        {

        }

        // Update is called once per frame
        public virtual void Update()
        {

        }

        public virtual void Effect(List<Character> Characters, out Event AddEvent)
        {
            AddEvent = null;
        }
    }
}