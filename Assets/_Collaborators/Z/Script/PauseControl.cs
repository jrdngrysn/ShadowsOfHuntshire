using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class PauseControl : MonoBehaviour {
        public static PauseControl Main;
        public GameObject AnimBase;
        public bool Active;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            AnimBase.SetActive(Active);
            if (Input.GetKeyDown(KeyCode.Escape))
                Active = !Active;
        }
    }
}