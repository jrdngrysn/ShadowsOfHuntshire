using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class Eye : MonoBehaviour {
        public GameObject Orb;
        public Animator Anim;
        [Space]
        public Vector2 DistanceRange;
        public Vector2 PositionRange;
        public Vector2 ScaleRange;
        [Space]
        public Vector2 RangeX;
        public Vector2 RangeY;
        public Vector2 DelayRange;
        public float CurrentDelay;
        [Space]
        public Vector2 CurrentPosition;
        public Vector2 TargetPosition;
        public float LookSpeed;
        [Space]
        public bool Highlighted;
        public float HighlightedTime;

        // Start is called before the first frame update
        void Start()
        {
            Vector2 a = GetRandomPosition();
            LookAt(a);
            TargetPosition = a;
            CurrentDelay = Random.Range(DelayRange.x, DelayRange.y);
        }

        // Update is called once per frame
        void Update()
        {
            HighlightedTime -= Time.deltaTime;
            if (Highlighted)
                HighlightedTime = 0.75f;
            Anim.SetBool("Active", Highlighted);
            if (HighlightedTime > 0)
            {
                LookAt(CurrentPosition);
            }
            else if (GlobalControl.Main.HoldingCharacter)
            {
                Vector2 a = Cursor.Main.GetPosition();
                TargetPosition = a;
                if ((TargetPosition - CurrentPosition).magnitude >= LookSpeed * 10f * Time.deltaTime)
                    LookAt(CurrentPosition + (TargetPosition - CurrentPosition).normalized * LookSpeed * 10f * Time.deltaTime);
                else
                    LookAt(CurrentPosition);
            }
            else
            {
                if ((TargetPosition - CurrentPosition).magnitude >= LookSpeed * Time.deltaTime)
                    LookAt(CurrentPosition + (TargetPosition - CurrentPosition).normalized * LookSpeed * Time.deltaTime);
                else
                    LookAt(CurrentPosition);
                CurrentDelay -= Time.deltaTime;
                if (CurrentDelay <= 0)
                {
                    CurrentDelay = Random.Range(DelayRange.x, DelayRange.y);
                    TargetPosition = GetRandomPosition();
                }
            }
        }

        public void LookAt(Vector2 Position)
        {
            CurrentPosition = Position;
            Vector2 D = Position - (Vector2)transform.position;
            if (D.x == 0)
                D.x = 0.01f;
            if (D.y == 0)
                D.y = 0.01f;
            transform.up = D;

            float Scale = (D.magnitude - DistanceRange.x) / (DistanceRange.y - DistanceRange.x);
            if (Scale > 1)
                Scale = 1;
            else if (Scale < 0)
                Scale = 0;
            float P = PositionRange.x + (PositionRange.y - PositionRange.x) * Scale;
            float S = ScaleRange.x + (ScaleRange.y - ScaleRange.x) * Scale;

            Orb.transform.localScale = new Vector3(1, S, 1);
            Orb.transform.localPosition = new Vector3(0, P, 0);
        }

        public Vector2 GetRandomPosition()
        {
            return new Vector2(Random.Range(RangeX.x, RangeX.y), Random.Range(RangeY.x, RangeY.y));
        }
    }
}