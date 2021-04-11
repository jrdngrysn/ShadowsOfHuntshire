using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace THAN
{
    public class ChoiceRenderer : MonoBehaviour {
        public bool Active;
        public EventChoice CurrentEC;
        [Space]
        public int Index;
        public EventRenderer ER;
        public Animator Anim;
        public Collider C2D;
        public TextMeshPro EmptyText;
        public TextMeshPro SelectingText;
        public TextMeshPro EffectText;
        public TextMeshPro SelectingEffectText;

        // Start is called before the first frame update
        void Start()
        {
            if (Active)
                Activate(null);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Render(EventChoice EC)
        {
            if (!EC)
            {
                EmptyText.text = "";
                SelectingText.text = "";
                EffectText.text = "";
                SelectingEffectText.text = "";
                return;
            }

            CurrentEC = EC;
            if (EC.GetContent() == "")
            {
                EmptyText.text = EC.EffectText;
                SelectingText.text = EC.EffectText;
                EffectText.text = "";
                SelectingEffectText.text = "";
            }
            else
            {
                string s = ER.ProcessContent(EC.GetContent(), EC.E.GetSource());
                EmptyText.text = s;
                SelectingText.text = s;
                EffectText.text = EC.EffectText;
                SelectingEffectText.text = EC.EffectText;
            }
        }

        public void Activate(EventChoice EC)
        {
            if (!EC)
                return;
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
            OnUnselect();
            Active = false;
        }

        public void OnMouseDown()
        {
            if (Active)
                ER.Select(Index);
        }

        public void OnSelect()
        {
            Anim.SetBool("Selecting", true);
            GlobalControl.Main.PlaySound("Hover");
        }

        public void OnUnselect()
        {
            Anim.SetBool("Selecting", false);
        }

        public void OnConfirm()
        {
            ER.Decide(Index);
            GlobalControl.Main.PlaySound("Select");
        }
    }
}