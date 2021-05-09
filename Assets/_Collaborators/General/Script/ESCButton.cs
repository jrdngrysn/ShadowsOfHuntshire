using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN {
    public class ESCButton : MonoBehaviour
    {
        PauseControl pauseControl;


        private void Start()
        {
            if (GameObject.Find("Pause") != null)
            {
                pauseControl = GameObject.Find("Pause").GetComponent<PauseControl>();
            }
        }

        private void OnMouseDown()
        {
            pauseControl.Active = !pauseControl.Active;
        }
    }
}