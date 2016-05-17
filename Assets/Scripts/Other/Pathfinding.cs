using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour
{
    public Grid gridReference;

    public GameObject start;
    public GameObject finish;

    private void Start()
    {
        gridReference = GetComponent<Grid>();
    }
}