using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using THAN;

public class Skill : MonoBehaviour {
    public int CoolDown;
    [HideInInspector] public int CurrentCoolDown;
    public List<SkillCondition> Conditions;
    public List<SkillEffect> Effects;

    // Start is called before the first frame update
    public virtual void Start()
    {

    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    public virtual void EndOfTurn()
    {
        CurrentCoolDown--;
    }

    public virtual bool CanTrigger(Character Source)
    {
        foreach (SkillCondition C in Conditions)
        {
            if (!C.Pass(Source))
                return false;
        }
        return CurrentCoolDown <= 0;
    }

    public virtual void Effect(Character Source, Character Partner)
    {
        foreach (SkillEffect E in Effects)
            E.Effect(Source, Partner);
    }
}