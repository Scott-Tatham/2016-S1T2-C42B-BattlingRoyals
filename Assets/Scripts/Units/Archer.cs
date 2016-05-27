using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Archer : Units
{
    public Archer()
    {
        SetUnitName("Archer");
        SetAttDmg(8.0f);
        SetAttSpeed(1.0f);
        SetDef(0.1f);
        SetHealth(40.0f);
        SetMoveSpeed(15.0f);
        SetRotate(2.0f);
        SetRange(5.0f);
        SetIsRange(true);
        SetIsAlive(true);
        SetIsSelect(false);
        SetUnitCost(8);
        SetUnitWorth(5);
        SetWeight(8);
    }

    void Update()
    {
        moveTo();
    }
}