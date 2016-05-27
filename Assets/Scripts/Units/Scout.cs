using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Scout : Units
{
    public Scout()
    {
        SetUnitName("Scout");
        SetAttDmg(5.0f);
        SetAttSpeed(0.2f);
        SetDef(0.05f);
        SetHealth(30.0f);
        SetMoveSpeed(20.0f);
        SetRotate(3.0f);
        SetIsAlive(true);
        SetIsSelect(false);
        SetUnitCost(5);
        SetUnitWorth(3);
        SetWeight(5);
    }

    void Update()
    {
        moveTo();
    }
}