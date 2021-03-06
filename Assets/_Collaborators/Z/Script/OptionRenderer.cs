﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class OptionRenderer : MonoBehaviour {
        public bool Active;
        public bool MouseOn;
        [Space]
        [SerializeField] GameObject myPanel; // for settings, it's the pause prefab
        public GameObject DisableBase;
        public GameObject ActiveBase;
        public Animator Anim;
        public Collider C2D;
        [Space]
        public bool closePanel;
        public bool Retry;
        public bool Exit;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnMouseEnter()
        {
            ActiveBase.SetActive(true);
            DisableBase.SetActive(false);
            MouseOn = true;
            Anim.SetBool("Selecting", true);
            if (GlobalControl.Main)
                GlobalControl.Main.PlaySound("Hover");
        }

        public void OnMouseExit()
        {
            DisableBase.SetActive(true);
            ActiveBase.SetActive(false);
            MouseOn = false;
            Anim.SetBool("Selecting", false);
        }

        public void OnMouseDown()
        {
            if (closePanel)
                myPanel.SetActive(false);
            if (Retry)
                GlobalControl.Main.Retry();
            if(Exit)
                Application.Quit();
        }
    }
}