using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using THAN;

public class Skill : MonoBehaviour {
    public List<SkillCondition> Conditions;
    public List<SkillEffect> Effects;
    public List<SkillEffect> EmptyEffects;
    [Space]
    public int CoolDown;
    [HideInInspector] public int CurrentCoolDown;
    [TextArea] public string Description;

    // Start is called before the first frame update
    public virtual void Start()
    {

    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    public virtual void StartOfTurn()
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
            E.Effect(Source, Partner, this);
    }

    public virtual void EmptyEffect(Character Source)
    {
        foreach (SkillEffect E in EmptyEffects)
            E.Effect(Source, null, this);
    }

    public virtual string GetDescription(Character Source)
    {
        return Translate(Description, Source);
    }

    public string Translate(string Text, Character Source)
    {
        string C = "";
        string S = Text;
        while (S.IndexOf("*") != -1)
        {
            C += S.Substring(0, S.IndexOf("*"));
            S = S.Substring(S.IndexOf("*") + 1);
            if (S.IndexOf("*") == -1)
                break;
            string Key = S.Substring(0, S.IndexOf("*"));
            S = S.Substring(S.IndexOf("*") + 1);
            if (Key == "Character_Name")
                C += Source.GetName();
            else
                C += Key;
        }
        C += S;
        return C;
    }
}