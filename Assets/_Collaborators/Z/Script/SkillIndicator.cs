using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class SkillIndicator : MonoBehaviour {
        public Character Source;
        public int TargetIndex;
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
            if (!Source || !GetTarget())
            {
                SR.enabled = false;
                return;
            }
            SR.enabled = true;
            if (Source.CurrentSkill == GetTarget())
                SR.sprite = ActiveSprite;
            else
                SR.sprite = DisableSprite;
        }

        public Skill GetTarget()
        {
            if (Source.Skills.Count <= TargetIndex)
                return null;
            return Source.Skills[TargetIndex];
        }
    }
}