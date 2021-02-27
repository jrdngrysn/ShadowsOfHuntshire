using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMST
{
    public class SoundtrackControl : MonoBehaviour {
        public static SoundtrackControl Main;
        public AudioSource Source;
        public AudioClip Soundtrack;
        public double LoopRate;
        public double LastLoopTime;

        public void Awake()
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

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public IEnumerator Process()
        {
            yield return new WaitForSeconds(5.5f);
            Source.PlayOneShot(Soundtrack);
            LastLoopTime = AudioSettings.dspTime;
            while (true)
            {
                if (AudioSettings.dspTime >= LastLoopTime + LoopRate)
                {
                    Source.PlayOneShot(Soundtrack);
                    LastLoopTime = AudioSettings.dspTime;
                }
                yield return 0;
            }
        }
    }
}