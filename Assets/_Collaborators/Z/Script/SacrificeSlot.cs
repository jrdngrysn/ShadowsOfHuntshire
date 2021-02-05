using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class SacrificeSlot : MonoBehaviour {
        public Animator Anim;
        public List<Eye> Eyes;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Active()
        {
            Anim.SetBool("Active", true);
            foreach (Eye E in Eyes)
                E.Highlighted = true;
        }

        public void Disable()
        {
            Anim.SetBool("Active", false);
            foreach (Eye E in Eyes)
                E.Highlighted = false;
        }
    }
}