using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace THAN
{
    public class ScrollBar : MonoBehaviour {
        public Vector2 PositionRange;
        public ScrollDot Dot;
        public float CurrentValue;
        public TextMeshPro TEXT;
        public string DefaultText;
        public bool Music;
        public bool SoundEffect;

        // Start is called before the first frame update
        void Start()
        {
            if (Music)
                Ini(TMST.SoundtrackControl.Main.Source.volume);
            if (SoundEffect)
                Ini(SoundControl.Main.Source.volume);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Ini(float Value)
        {
            float x = PositionRange.x + (PositionRange.y - PositionRange.x) * Value;
            Dot.transform.localPosition = new Vector3(x, Dot.transform.localPosition.y, Dot.transform.localPosition.z);
            SetValue(Value);
        }

        public void SetPosition(float Value)
        {
            if (Value < PositionRange.x)
                Value = PositionRange.x;
            else if (Value > PositionRange.y)
                Value = PositionRange.y;
            SetValue((Value - PositionRange.x) / (PositionRange.y - PositionRange.x));
            Dot.transform.localPosition = new Vector3(Value, Dot.transform.localPosition.y, Dot.transform.localPosition.z);
        }

        public void SetValue(float Value)
        {
            CurrentValue = Value;
            TEXT.text = DefaultText + ": " + (int)(Value * 100);
            if (Music)
                TMST.SoundtrackControl.Main.Source.volume = Value;
            if (SoundEffect)
                SoundControl.Main.Source.volume = Value;
        }
    }
}