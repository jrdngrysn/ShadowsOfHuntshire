using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using THAN;

public class SkillCondition : MonoBehaviour {

    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual bool Pass(Character Self)
    {
        return true;
    }
}
