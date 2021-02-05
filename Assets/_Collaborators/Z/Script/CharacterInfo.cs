using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class CharacterInfo : MonoBehaviour {
        public float Vitality;
        public float Passion;
        public float Reason;
        public bool Hidden_Vitality;
        public bool Hidden_Passion;
        public bool Hidden_Reason;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Import(CharacterInfo Info, Character Base)
        {
            Vitality = Info.Vitality;
            Passion = Info.Passion;
            Reason = Info.Reason;
            Hidden_Vitality = Info.Hidden_Vitality;
            Hidden_Passion = Info.Hidden_Passion;
            Hidden_Reason = Info.Hidden_Reason;
        }

        public void SetVitality(float Value)
        {
            Vitality = Value;
        }

        public void SetPassion(float Value)
        {
            Passion = Value;
        }

        public void SetReason(float Value)
        {
            Reason = Value;
        }

        public void SetHidden_Vitality(bool Value)
        {
            Hidden_Vitality = Value;
        }

        public void SetHidden_Passion(bool Value)
        {
            Hidden_Passion = Value;
        }

        public void SetHidden_Reason(bool Value)
        {
            Hidden_Reason = Value;
        }
    }
}