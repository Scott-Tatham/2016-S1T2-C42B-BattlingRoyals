using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node
{
    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridZ;

    public int gCost;
    public int hCost;
    public Node parent;

    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridZ)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridZ = _gridZ;
    }

    public int fCost()
    {
        return (gCost + hCost);
    }
}