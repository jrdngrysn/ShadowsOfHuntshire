using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class TutorialAnim : MonoBehaviour {
        public Animator Anim;
        [Space]
        public List<TriggerCondition> ActiveConditions;
        public List<TriggerCondition> DisableConditions;
        [HideInInspector] public bool Active;
        [HideInInspector] public bool AlreadyDead;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!Active && !AlreadyDead)
            {
                bool a = true;
                foreach (TriggerCondition TC in ActiveConditions)
                {
                    if (!TC.Pass())
                        a = false;
                }
                if (a)
                    Activate();
            }
            else if (Active && !AlreadyDead)
            {
                bool a = true;
                foreach (TriggerCondition TC in DisableConditions)
                {
                    if (!TC.Pass())
                        a = false;
                }
                if (a)
                    Disable();
            }
        }

        public void Activate()
        {
            Active = true;
            Anim.SetBool("Active", true);
        }

        public void Disable()
        {
            AlreadyDead = true;
            Active = false;
            Anim.SetBool("Active", false);
        }
    }
}