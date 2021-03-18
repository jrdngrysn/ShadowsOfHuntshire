using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using THAN;


public class SkillCondition_StatCheck : SkillCondition
{

    private enum statEnum {Vitality, Passion, Reason};


    [Header ("Stat Checked to Trigger Ability")]
    [SerializeField] private statEnum stat = statEnum.Vitality;


    [Header ("Ability Will Activate When Stat is Greater than or Equal")]
    [SerializeField] private int statThreshold;

    public override bool Pass(Character Self)
    {
        switch (stat)
        {
            case statEnum.Vitality:
                    return Self.GetVitality() >= statThreshold;
            case statEnum.Passion:
                    return Self.GetPassion() >= statThreshold;
            case statEnum.Reason:
                    return Self.GetReason() >= statThreshold;
            default:
                return false;
        }
    }
}
