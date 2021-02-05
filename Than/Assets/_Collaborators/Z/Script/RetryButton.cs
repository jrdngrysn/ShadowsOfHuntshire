using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class RetryButton : MonoBehaviour {
        public GameObject RetryObject;
        public Collider C2D;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!GlobalControl.Main.GetSacrificeActive() || GlobalControl.Main.HaveSacrifice())
            {
                C2D.enabled = false;
                RetryObject.SetActive(false);
                return;
            }

            C2D.enabled = true;
            RetryObject.SetActive(true);
        }

        public void OnMouseDown()
        {
            if (GlobalControl.Main.GetSacrificeActive() && !GlobalControl.Main.HaveSacrifice() && !GlobalControl.Main.HoldingCharacter)
                GlobalControl.Main.Retry();
        }
    }
}