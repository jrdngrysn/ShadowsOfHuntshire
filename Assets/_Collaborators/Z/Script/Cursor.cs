using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class Cursor : MonoBehaviour {
        public static Cursor Main;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        public void Update()
        {
            PositionUpdate();
            if (Input.GetMouseButtonDown(0))
                Effect();
        }

        public void FixedUpdate()
        {
            PositionUpdate();
        }

        public void PositionUpdate()
        {
            Vector3 a = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(a.x, a.y, transform.position.z);
        }

        public void Effect()
        {
            if (!GlobalControl.Main.GetBoardActive())
                return;

            if (!GlobalControl.Main.HoldingCharacter)
            {
                if (GlobalControl.Main.GetSelectingCharacter())
                {
                    Character C = GlobalControl.Main.GetSelectingCharacter();
                    C.PickUp();
                    GlobalControl.Main.PlaySound("PickUp");
                }
            }
            else
            {
                if (GlobalControl.Main.GetSelectingCharacter() && GlobalControl.Main.GetSelectingCharacter().CurrentSlot != GlobalControl.Main.SacrificeSlot)
                {
                    if (GlobalControl.Main.GetSelectingCharacter().CanPair())
                    {
                        GlobalControl.Main.GetSelectingCharacter().CreatePair(GlobalControl.Main.HoldingCharacter);
                        GlobalControl.Main.PlaySound("PutDown");
                    }
                }
                else
                {
                    Slot S = GetSelectingSlot();
                    if (!S.GetCharacter() && (S != GlobalControl.Main.SacrificeSlot || GlobalControl.Main.HoldingCharacter.CanDie()))
                    {
                        Character C = GlobalControl.Main.HoldingCharacter;
                        C.PutDown(S);
                        C.TryBark();
                        GlobalControl.Main.PlaySound("PutDown");
                    }
                }
            }
        }

        public Slot GetSelectingSlot()
        {
            float a = 9999f;
            Slot Temp = null;
            foreach (Slot S in GlobalControl.Main.Slots)
            {
                if (S == GlobalControl.Main.SacrificeSlot && !GlobalControl.Main.GetSacrificeActive())
                    continue;
                float b = (S.GetPosition() - GetPosition()).magnitude;
                if (b < a)
                {
                    a = b;
                    Temp = S;
                }
            }
            return Temp;
        }

        public Vector2 GetPosition()
        {
            return transform.position;
        }
    }
}