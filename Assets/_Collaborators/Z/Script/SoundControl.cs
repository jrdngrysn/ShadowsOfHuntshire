using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class SoundControl : MonoBehaviour {
        public static SoundControl Main;
        public AudioSource Source;
        public AudioClip PickUp;
        public AudioClip PutDown;
        public AudioClip EventBell;
        public AudioClip Hover;
        public AudioClip Select;

        public void Awake()
        {
            if (!Main)
            {
                Main = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PlaySound(AudioClip Clip)
        {
            Source.PlayOneShot(Clip);
        }
    }
}