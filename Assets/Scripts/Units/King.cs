using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class King : Units
{
    public King()
    {
        SetUnitName("King");
        SetCharName("George");
        SetAttDmg(50.0f);
        SetAttSpeed(0.5f);
        SetDef(0.5f);
        SetHealth(300.0f);
        SetMoveSpeed(10.0f);
        SetRange(3.0f);
        SetIsAlive(true);
        SetIsSelect(false);
        SetWeight(30);
    }
    
    void Update()
    {
        moveTo();
    }
}