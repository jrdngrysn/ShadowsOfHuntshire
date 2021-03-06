using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using THAN;

public class SkillEffect_Global : SkillEffect
{
    private enum statEnum { Vitality, Passion, Reason };


    [Header("Global Stat To Reduce")]
    [Header("Ensure this is entered into the EMPTY EFFECT")]
    [SerializeField] private statEnum pStat = statEnum.Vitality;

    [Header("Self Stat To Reduce")]
    [SerializeField] private statEnum sStat = statEnum.Vitality;

    [Header("Self Stat To Increase")]
    [SerializeField] private statEnum siStat = statEnum.Vitality;


    [Header("Global Reduction Amount (Positive)")]
    [SerializeField] private int otherChange;

    [Header("Self Reduction Amount (Positive)")]
    [SerializeField] private int selfChange;
    public override void Effect(Character Source, Character Partner, Skill skill)
    {
        switch (pStat)
        {
            case statEnum.Vitality:
                foreach (Character ch in GlobalControl.Main.Characters) {
                    if (ch.Active)
                    {
                        ch.ChangeVitality(-otherChange);
                    }
                }
                break;
            case statEnum.Passion:
                foreach (Character ch in GlobalControl.Main.Characters)
                {
                    if (ch.Active)
                    {
                        ch.ChangeVitality(-otherChange);
                    }
                }
                break;
            case statEnum.Reason:
                foreach (Character ch in GlobalControl.Main.Characters)
                {
                    if (ch.Active)
                    {
                        ch.ChangeVitality(-otherChange);
                    }
                }
                break;
            default:
                break;
        }

        switch (sStat)
        {
            case statEnum.Vitality:
                Source.ChangeVitality(-selfChange);
                break;
            case statEnum.Passion:
                Source.ChangePassion(-selfChange);
                break;
            case statEnum.Reason:
                Source.ChangeReason(-selfChange);
                break;
            default:
                break;
        }

        switch (siStat)
        {
            case statEnum.Vitality:
                Source.SetVitality(15);
                break;
            case statEnum.Passion:
                Source.SetPassion(15);
                break;
            case statEnum.Reason:
                Source.SetReason(15);
                break;
            default:
                break;
        }
    }
}
