using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class ConfirmButton_Retry : ConfirmButton {

        public override void OnMouseDown()
        {
            GlobalControl.Main.Retry();
        }
    }
}