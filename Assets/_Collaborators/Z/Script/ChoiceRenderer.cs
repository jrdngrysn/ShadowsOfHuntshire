using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace THAN
{
    public class ChoiceRenderer : MonoBehaviour {
        public bool Active;
        public bool MouseOn;
        public float MouseOnDelay;
        public EventChoice CurrentEC;
        [Space]
        public int Index;
        public EventRenderer ER;
        public Animator Anim;
        public Collider C2D;
        public TextMeshPro EmptyText;
        public TextMeshPro SelectingText;

        // Start is called before the first frame update
        void Start()
        {
            if (Active)
                Activate(null);
        }

        // Update is called once per frame
        void Update()
        {
            MouseOnDelay -= Time.deltaTime;
            if (MouseOn)
                MouseOnDelay = 0.4f;
        }

        public void Render(EventChoice EC)
        {
            if (!EC)
            {
                EmptyText.text = "";
                SelectingText.text = "";
                return;
            }

            CurrentEC = EC;
            if (EC.EffectText != "")
            {
                EmptyText.text = EC.GetContent() + "  [" + EC.EffectText + "]";
                SelectingText.text = EC.GetContent() + "  [" + EC.EffectText + "]";
            }
            else if (EC.GetContent() == "")
            {
                EmptyText.text = "[" + EC.EffectText + "]";
                SelectingText.text = "[" + EC.EffectText + "]";
            }
            else
            {
                EmptyText.text = EC.GetContent();
                SelectingText.text = EC.GetContent();
            }
        }

        public void Activate(EventChoice EC)
        {
            Render(EC);
            Anim.SetBool("Active", true);
            C2D.enabled = true;
            Active = true;
        }

        public void Disable()
        {
            if (!Active)
                return;
            Anim.SetBool("Active", false);
            C2D.enabled = false;
            OnMouseExit();
            Active = false;
        }

        public void OnMouseEnter()
        {
            MouseOn = true;
            Anim.SetBool("Selecting", true);
            GlobalControl.Main.PlaySound("Hover");
        }

        public void OnMouseExit()
        {
            MouseOn = false;
            Anim.SetBool("Selecting", false);
        }

        public void OnMouseDown()
        {
            if (Active)
            {
                ER.Decide(Index);
                GlobalControl.Main.PlaySound("Select");
            }
        }
    }
}