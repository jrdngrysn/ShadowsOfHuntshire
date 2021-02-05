using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMST
{
    public class SoundtrackControl : MonoBehaviour {
        public static SoundtrackControl Main;
        public GameObject Soundtrack;
        public double LoopRate;
        public double LastLoopTime;

        // Start is called before the first frame update
        void Start()
        {
            if (!Main)
            {
                Main = this;
                DontDestroyOnLoad(gameObject);
                StartCoroutine("Process");
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public IEnumerator Process()
        {
            GameObject G = Instantiate(Soundtrack, transform);
            Destroy(G, 180f);
            LastLoopTime = AudioSettings.dspTime;
            while (true)
            {
                if (AudioSettings.dspTime >= LastLoopTime + LoopRate)
                {
                    GameObject G2 = Instantiate(Soundtrack, transform);
                    Destroy(G2, 180f);
                    LastLoopTime = AudioSettings.dspTime;
                }
                yield return 0;
            }
        }
    }
}