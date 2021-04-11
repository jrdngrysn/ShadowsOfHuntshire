using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace THAN
{
    public class NewCardRenderer : MonoBehaviour {
        public Animator Anim;
        public TextMeshPro NameText;
        public TextMeshPro ContentText;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Active(Character C)
        {
            ContentText.text = C.GetIntro();
            NameText.text = C.GetName();
            Anim.SetBool("Active", true);
        }

        public void Disable()
        {
            Anim.SetBool("Active", false);
        }
    }
}