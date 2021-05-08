using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class ScrollDot : MonoBehaviour {
        public ScrollBar Bar;
        public bool Holding;
        public float HoldPoint;
        public float OriPoint;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Holding && Input.GetMouseButtonUp(0))
                Holding = false;

            if (Holding)
                Bar.SetPosition(Cursor.Main.GetPosition().x - HoldPoint + OriPoint);
        }

        public void OnMouseDown()
        {
            if (GlobalControl.Main)
                GlobalControl.Main.PlaySound("Hover");
            Holding = true;
            HoldPoint = Cursor.Main.GetPosition().x;
            OriPoint = transform.localPosition.x;
        }
    }
}