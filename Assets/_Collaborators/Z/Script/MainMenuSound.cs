using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class MainMenuSound : MonoBehaviour {
        public static MainMenuSound Main;
        public AudioSource Source;
        public AudioReverbFilter Reverb;
        public bool AlreadyDead;

        public void Awake()
        {
            if (!Main || Main.AlreadyDead)
            {
                DontDestroyOnLoad(gameObject);
                Main = this;
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (GlobalControl.Main && !AlreadyDead)
                StartCoroutine("Fade");
        }

        public IEnumerator Fade()
        {
            yield return new WaitForSeconds(0.5f);
            AlreadyDead = true;
            while (Reverb.decayTime < 15)
            {
                Reverb.decayTime += Time.deltaTime * 20;
                yield return 0;
            }
            Source.Pause();
            Destroy(gameObject, 10);
        }
    }
}