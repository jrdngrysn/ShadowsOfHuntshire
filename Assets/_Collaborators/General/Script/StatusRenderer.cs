using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace THAN
{
    public class StatusRenderer : MonoBehaviour {
        public static StatusRenderer Main;
        public GameObject AnimBase;
        public TextMeshPro DescriptionText;
        public UIPanel Panel;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Render(Skill S, Character C)
        {
            transform.position = C.transform.position;
            if (C.transform.position.x <= -50)
                DescriptionText.transform.localPosition = new Vector3(19f, DescriptionText.transform.localPosition.y);
            else
                DescriptionText.transform.localPosition = new Vector3(-19f, DescriptionText.transform.localPosition.y);
            AnimBase.SetActive(true);
            DescriptionText.text = S.GetDescription(C);
            DescriptionText.ForceMeshUpdate();
            Vector2 c = DescriptionText.textBounds.center + DescriptionText.transform.position;
            float w = DescriptionText.renderedWidth + 2;
            float h = DescriptionText.renderedHeight + 2;
            Panel.Render(c.x - w * 0.5f, c.x + w * 0.5f, c.y + h * 0.5f, c.y - h * 0.5f);
            Panel.AnimBase.SetActive(true);
        }

        public void Disable()
        {
            AnimBase.SetActive(false);
            DescriptionText.text = "";
            Panel.AnimBase.SetActive(false);
        }
    }
}