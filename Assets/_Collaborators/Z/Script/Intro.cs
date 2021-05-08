using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class Intro : MonoBehaviour {
        public float Delay;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Delay > 0)
                Delay -= Time.deltaTime;
            else if (Delay > -999)
            {
                Delay = -999.9f;
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Prototype");
            }

        }
    }
}