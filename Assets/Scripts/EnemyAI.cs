using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : GameManager
{
    private int unitWeightOne;
    private int unitWeightTwo;
    private int unitWeightThree;
    private int unitWeightFour;
    private int overallCost;

    private int overallRes;

    private List<Units> quarterOne;
    private List<Units> quarterTwo;
    private List<Units> quarterThree;
    private List<Units> quarterFour;

    private Vector3 position;

    enum Conditions { quarterOne, quarterTwo, quarterThree, quarterFour }

    void Start()
    {
        quarterOne = new List<Units>();
        quarterTwo = new List<Units>();
        quarterThree = new List<Units>();
        quarterFour = new List<Units>();

        InvokeRepeating("checkList", 0, 10.0f);
    }

    void Update()
    {

    }

    public void checkList()
    {
        foreach (Units unit in GetAllUnits())
        {
            if (unit.tag == "Player Unit")
            {
                position = unit.transform.position;

                if (position.x < 0 && position.z > 0)
                {
                    unitWeightOne = unitWeightOne + unit.GetWeight();
                    quarterOne.Add(unit);
                }

                else if (position.x > 0 && position.z > 0)
                {
                    unitWeightTwo = unitWeightTwo + unit.GetWeight();
                    quarterTwo.Add(unit);
                }

                else if (position.x < 0 && position.z < 0)
                {
                    unitWeightThree = unitWeightThree + unit.GetWeight();
                    quarterThree.Add(unit);
                }

                else if (position.x > 0 && position.z < 0)
                {
                    unitWeightFour = unitWeightFour + unit.GetWeight();
                    quarterFour.Add(unit);
                }
            }
        }

        Debug.Log(unitWeightOne);
        Debug.Log(unitWeightTwo);
        Debug.Log(unitWeightThree);
        Debug.Log(unitWeightFour);

        for (int i = 0; i < quarterOne.Count; i++)
        {
            Debug.Log(quarterOne[i]);
        }

        for (int i = 0; i < quarterTwo.Count; i++)
        {
            Debug.Log(quarterTwo[i]);
        }

        for (int i = 0; i < quarterThree.Count; i++)
        {
            Debug.Log(quarterThree[i]);
        }

        for (int i = 0; i < quarterFour.Count; i++)
        {
            Debug.Log(quarterFour[i]);
        }

        assignTargets();
    }

    public void assignTargets()
    {
        overallCost = unitWeightOne + unitWeightTwo + unitWeightThree + unitWeightFour;
        Debug.Log(overallCost);

        foreach (Units unit in GetAllUnits())
        {
            if (unit.tag == "Enemy Unit")
            {
                overallRes = overallRes + unit.GetWeight();
            }
        }

        Debug.Log(overallRes);
        foreach (Units enemyUnit in allUnits)
        {
            if (enemyUnit.tag == "Enemy Unit")
            {
                foreach (Units unit in allUnits)
                {
                  
                    }
                }
            }
        }
    }
}