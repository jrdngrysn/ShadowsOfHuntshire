using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class UIPanel : MonoBehaviour {
        public GameObject AnimBase;
        public List<GameObject> Objects;
        public List<SpriteRenderer> SRs;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Render(float Left, float Right, float Up, float Down)
        {
            float x = Left + (Right - Left) * 0.5f;
            float y = Down + (Up - Down) * 0.5f;
            SetPosition(new Vector2(x, y));
            SetSize(new Vector2(Right - Left, Up - Down));
        }

        public void SetPosition(Vector2 Position)
        {
            transform.position = new Vector3(Position.x, Position.y, transform.position.z);
        }

        public void SetSize(Vector2 Size)
        {
            Objects[0].transform.localScale = new Vector3(Size.x, Size.y, 1);
            Objects[1].transform.localPosition = new Vector3(0, Size.y / 2f);
            Objects[1].transform.localScale = new Vector3(Size.x, 1, 1);
            Objects[2].transform.localPosition = new Vector3(Size.x / 2f, Size.y / 2f);
            Objects[3].transform.localPosition = new Vector3(Size.x / 2f, 0);
            Objects[3].transform.localScale = new Vector3(1, Size.y, 1);
            Objects[4].transform.localPosition = new Vector3(Size.x / 2f, -Size.y / 2f);
            Objects[5].transform.localPosition = new Vector3(0, -Size.y / 2f);
            Objects[5].transform.localScale = new Vector3(Size.x, 1, 1);
            Objects[6].transform.localPosition = new Vector3(-Size.x / 2f, -Size.y / 2f);
            Objects[7].transform.localPosition = new Vector3(-Size.x / 2f, 0);
            Objects[7].transform.localScale = new Vector3(1, Size.y, 1);
            Objects[8].transform.localPosition = new Vector3(-Size.x / 2f, Size.y / 2f);
        }

        public void SetColor(Color Value)
        {
            foreach (SpriteRenderer SR in SRs)
                SR.color = Value;
        }
    }
}