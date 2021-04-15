using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class EscapeButton : MonoBehaviour {

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameObject.Find("MainMenuController").GetComponent<MainMenuController>().Quit();
            }
        }
    }
}