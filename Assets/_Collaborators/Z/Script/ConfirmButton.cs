using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class ConfirmButton : MonoBehaviour {
        public bool Active;
        public bool MouseOn;
        public EventRenderer ER;
        public SpriteRenderer SR;
        public Sprite ActiveSprite;
        public Sprite DisableSprite;

        // Start is called before the first frame update
        public virtual void Start()
        {

        }

        // Update is called once per frame
        public virtual void Update()
        {

        }

        public virtual void OnMouseDown()
        {
            if (Active)
                ER.TryDecide();
        }

        public void SetRender(bool Value)
        {
            if (Value)
                SR.sprite = ActiveSprite;
            else
                SR.sprite = DisableSprite;
        }

        public void OnMouseEnter()
        {
            if (Active)
                MouseOn = true;
        }

        public void OnMouseExit()
        {
            MouseOn = false;
        }
    }
}