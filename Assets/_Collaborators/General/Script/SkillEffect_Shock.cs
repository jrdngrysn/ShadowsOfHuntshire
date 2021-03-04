using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using THAN;

public class SkillEffect_Shock : SkillEffect
{
    private enum statEnum { Vitality, Passion, Reason };


    [Header("Partner Stat To Reduce")]
    [SerializeField] private statEnum pStat = statEnum.Vitality;

    [Header("Self Stat To Reduce")]
    [SerializeField] private statEnum sStat = statEnum.Vitality;

    [Header("Self Stat To Increase")]
    [SerializeField] private statEnum siStat = statEnum.Vitality;


    [Header("Stat Reduction Amount (Positive)")]
    [SerializeField] private int statChange;
    public override void Effect(Character Source, Character Partner, Skill skill)
    {
        switch (pStat)
        {
            case statEnum.Vitality:
                Partner.ChangeVitality(-statChange);
                break;
            case statEnum.Passion:
                Partner.ChangePassion(-statChange);
                break;
            case statEnum.Reason:
                Partner.ChangeReason(-statChange);
                break;
            default:
                break;
        }

        switch (sStat)
        {
            case statEnum.Vitality:
                Source.ChangeVitality(-statChange);
                break;
            case statEnum.Passion:
                Source.ChangePassion(-statChange);
                break;
            case statEnum.Reason:
                Source.ChangeReason(-statChange);
                break;
            default:
                break;
        }

        switch (siStat)
        {
            case statEnum.Vitality:
                Source.ChangeVitality(statChange);
                break;
            case statEnum.Passion:
                Source.ChangePassion(statChange);
                break;
            case statEnum.Reason:
                Source.ChangeReason(statChange);
                break;
            default:
                break;
        }
    }
}
