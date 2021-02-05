using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class EndTurnButton : MonoBehaviour {
        public Animator Anim;
        public Collider C2D;
        public bool MouseOn;
        [Space]
        public float StepTime;
        public GameObject Moon;
        public float MoonSpeed;
        public GameObject CircleI;
        public float CircleISpeed;
        public GameObject CircleII;
        public float CircleIISpeed;
        [Space]
        public bool RingRotating;
        public GameObject Ring;
        public float RingSpeed;
        [Space]
        public AudioSource Sound;
        public float MaxSoundVolume;
        public float SoundSpeed;
        public bool SoundActive;
        [Space]
        public bool Animating;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            C2D.enabled = GlobalControl.Main.BoardActive;
            Anim.SetBool("Active", MouseOn || !GlobalControl.Main.BoardActive);
            //Moon.transform.localEulerAngles = new Vector3(0, 0, CurrentMoonRotation);
            //Ring.transform.localEulerAngles = new Vector3(0, 0, CurrentRingRotation);
            RingRotating = GlobalControl.Main.BoardActive;
            if (RingRotating)
                Ring.transform.localEulerAngles = new Vector3(0, 0, Ring.transform.localEulerAngles.z + RingSpeed * Time.deltaTime);
            if (SoundActive && Sound.volume < MaxSoundVolume)
            {
                Sound.volume += SoundSpeed * Time.deltaTime;
                if (Sound.volume > MaxSoundVolume)
                    Sound.volume = MaxSoundVolume;
            }
            else if (!SoundActive && Sound.volume > 0)
            {
                Sound.volume -= SoundSpeed * Time.deltaTime;
                if (Sound.volume < 0)
                    Sound.volume = 0;
            }
        }

        public void Next(float Step)
        {
            Animating = true;
            StartCoroutine(NextIE(Step));
        }

        public IEnumerator NextIE(float Step)
        {
            float a = 0f;
            float MoonOri = Moon.transform.localEulerAngles.z;
            float MoonTarget = MoonOri + MoonSpeed * Step;
            float CircleIOri = CircleI.transform.localEulerAngles.z;
            float CircleITarget = CircleIOri + CircleISpeed * Step;
            float CircleIIOri = CircleII.transform.localEulerAngles.z;
            float CircleIITarget = CircleIIOri + CircleIISpeed * Step;
            SoundActive = true;
            while (a < Step * StepTime)
            {
                a += Time.deltaTime;
                Moon.transform.localEulerAngles = new Vector3(0, 0, MoonOri + (MoonTarget - MoonOri) * (a / (Step * StepTime)));
                CircleI.transform.localEulerAngles = new Vector3(0, 0, CircleIOri + (CircleITarget - CircleIOri) * (a / (Step * StepTime)));
                CircleII.transform.localEulerAngles = new Vector3(0, 0, CircleIIOri + (CircleIITarget - CircleIIOri) * (a / (Step * StepTime)));
                yield return 0;
            }
            SoundActive = false;
            Animating = false;
        }

        /*public void Next()
        {
            StartCoroutine("NextIE");
        }

        public IEnumerator NextIE()
        {
            float a = 0;
            float Ori = CurrentMoonRotation;
            float Target = CurrentMoonRotation - 6f;
            float OriII = CurrentRingRotation;
            float TargetII = CurrentRingRotation + 6f;
            while (a < StepTime)
            {
                a += Time.deltaTime;
                CurrentMoonRotation = Ori + (Target - Ori) * MoonCurve.Evaluate((a / StepTime));
                CurrentRingRotation = OriII + (TargetII - OriII) * RingCurve.Evaluate((a / StepTime));
                yield return 0;
            }
            CurrentMoonRotation = Target;
            CurrentRingRotation = TargetII;
        }*/

        public void OnMouseEnter()
        {
            MouseOn = true;
        }

        public void OnMouseExit()
        {
            MouseOn = false;
        }

        public void OnMouseDown()
        {
            if (GlobalControl.Main.CanEndTurn())
                GlobalControl.Main.EndOfTurn();
        }
    }
}