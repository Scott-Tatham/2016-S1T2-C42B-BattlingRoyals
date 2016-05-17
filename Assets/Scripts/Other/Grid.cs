using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{
    Node[,] grid;
    public int nodeSpacing;

    public int gridSizeX;
    public int gridSizeZ;


    private void Start()
    {
        createGrid();
    }

    public void createGrid()
    {
        grid = new Node[10, 10];
        Vector3 worldPoint = Vector3.zero;
        for (int x = 0; x < 10; x++)
        {
            for (int z = 0; z < 10; z++)
            {
                worldPoint = new Vector3(x * nodeSpacing, 0, z * nodeSpacing);
                grid[x, z] = new Node(true, worldPoint, x, z);
            }
        }
    }

    public List<Node> getAdj(Node node)
    {
        List<Node> adjNodes = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int z = -1; z <= 1; z++)
            {
                if (x == 0 && z == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkZ = node.gridZ + z;

                if (checkX >= 0 && checkX < gridSizeX && checkZ >= 0 && checkZ < gridSizeZ)
                {
                    adjNodes.Add(grid[checkX, checkZ]);
                }
            }
        }
        return adjNodes;
    }
}