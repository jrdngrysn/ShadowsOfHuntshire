using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class Slot_New : Slot {

        public override void Awake()
        {
            if (IniActive)
                GlobalControl.Main.AddSlot(this);
        }

        public override void OnMouseEnter()
        {
            if (GlobalControl.Main.NewCharacterActive)
                GlobalControl.Main.SelectingSlot = this;
        }

        public override void OnMouseExit()
        {
            if (GlobalControl.Main.SelectingSlot == this)
                GlobalControl.Main.SelectingSlot = null;
        }
    }
}