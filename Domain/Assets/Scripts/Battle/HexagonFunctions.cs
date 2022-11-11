using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonFunctions
{
    public int maxX;
    public int maxY;
    public int[,,] evenqDifferences;

    public HexagonFunctions(int maxX, int maxY)
    {
        this.maxX = maxX;
        this.maxY = maxY;

        evenqDifferences = new int[,,] {
            //even x
            {{1,0},{1,-1 },{0,-1 },{-1,-1},{-1,0},{0,1}},
            //odd x
            {{1,1},{1,0},{0,-1},{-1,0},{-1,1},{0,1}}
        };
    }

    public (int, int) IndexToDimensional(int i)
    {
        return (i / (maxY+1), i % (maxY+1));
    }

    public Axial RectangularToAxial(int x, int y) => new Axial(x, y - (x - (x & 1)) / 2);

    public (int, int) AxialToRectangular(Axial hex) => (hex.q, hex.r + (hex.q - (hex.q & 1)) / 2);

    public bool isValid(int x, int y) => (x >= 0 && x <= maxX && y >= 0 && y <= maxY);

    public List<(int, int)> GetNeighbors(int x, int y)
    {
        List<(int, int)> output = new();
        int parity = x & 1;
        for (int i = 0; i < 6; i++)
        {
            (int, int) temp = (x + evenqDifferences[parity, i, 0], y + evenqDifferences[parity, i, 1]);
            if (isValid(temp.Item1, temp.Item2))
            {
                output.Add(temp);
            }
        }
        return output;
    }
}
