using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class ConfirmButton : MonoBehaviour {
        public bool Active;
        public EventRenderer ER;
        public SpriteRenderer SR;
        public Sprite ActiveSprite;
        public Sprite DisableSprite;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnMouseDown()
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
    }
}