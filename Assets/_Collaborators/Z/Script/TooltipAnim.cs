using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace THAN
{
    public class TooltipAnim : MonoBehaviour {
        public TextMeshPro TEXT;
        public GameObject CenterPivot;
        public GameObject LeftPivot;
        public GameObject RightPivot;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            float Length = TEXT.textBounds.max.x - TEXT.textBounds.min.x;
            CenterPivot.transform.localScale = new Vector3(Length, 1, 1);
            LeftPivot.transform.localPosition = new Vector3(TEXT.textBounds.min.x, 0, 0);
            RightPivot.transform.localPosition = new Vector3(TEXT.textBounds.max.x, 0, 0);
        }

        public void Render(string Value)
        {
            TEXT.text = Value;
            TEXT.ForceMeshUpdate();
            Update();
        }
    }
}