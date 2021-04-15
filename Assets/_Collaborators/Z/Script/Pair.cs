using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class Pair : MonoBehaviour {
        public Character C1;
        public Character C2;
        public GameObject Mask;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Ini(Character I, Character II)
        {
            C1 = I;
            C2 = II;
        }

        public Character GetCharacter(int Index)
        {
            if (Index == 0)
                return C1;
            else if (Index == 1)
                return C2;
            return null;
        }

        public void SetPosition(Vector2 Target)
        {
            transform.position = new Vector3(Target.x, Target.y, transform.position.z);
        }

        public void Effect()
        {
            if (!C1 || !C2)
                return;
            if (C1.CurrentSkill || C2.CurrentSkill)
            {
                if (C1.CurrentSkill)
                {
                    C1.CurrentSkill.Effect(C1, C2);
                    C1.OnSkillTriggered();
                }
                if (C2.CurrentSkill)
                {
                    C2.CurrentSkill.Effect(C2, C1);
                    C2.OnSkillTriggered();
                }
                return;
            }
            float V1 = C1.GetVitality();
            float P1 = C1.GetPassion();
            float R1 = C1.GetReason();
            float V2 = C2.GetVitality();
            float P2 = C2.GetPassion();
            float R2 = C2.GetReason();
            C1.BoundValueChange(V2, P2, R2);
            C2.BoundValueChange(V1, P1, R1);
        }

        public void ActivateMask()
        {
            //Mask.SetActive(true);
            if (C1)
                C1.ActivateMask();
            if (C2)
                C2.ActivateMask();
            GlobalControl.Main.MaskedPairs.Add(this);
        }

        public void DisableMask()
        {
            if (C1)
                C1.DisableMask();
            if (C2)
                C2.DisableMask();
            /*if (Mask)
                Mask.SetActive(false);*/
        }
    }
}