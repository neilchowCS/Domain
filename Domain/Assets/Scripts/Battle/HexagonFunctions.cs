using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    public bool isValid((int, int) x) => isValid(x.Item1, x.Item2);

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

    public int GetDistance(int x1, int y1, int x2, int y2)
    {
        /*
        function axial_distance(a, b):
    return (abs(a.q - b.q)
          + abs(a.q + a.r - b.q - b.r)
          + abs(a.r - b.r)) / 2
        */
        Axial a = RectangularToAxial(x1, y1);
        Axial b = RectangularToAxial(x2, y2);
        return (Math.Abs(a.q - b.q)
          + Math.Abs(a.q + a.r - b.q - b.r)
          + Math.Abs(a.r - b.r)) / 2;
    }

    public List<(int, int)> HexWithinDistance(int x, int y, int d)
    {
        Axial a = RectangularToAxial(x, y);
        List<(int, int)> output = new();
        for (int q = -d; q <= d; q++)
        {
            //max(-N, -q - N) ≤ r ≤ min(+N, -q + N):
            for (int r = Math.Max(-d, -q-d); r <= Math.Min(d, -q+d); r++)
            {
                //results.append(axial_add(center, Hex(q, r)))
                (int, int) b = AxialToRectangular(new Axial(a.q + q, a.r + r));
                if (isValid(b))
                {
                    output.Add(b);
                }
            }
        }

        return output;
    }

    public List<(int, int)> GetLine(int x1, int y1, int x2, int y2, int count)
    {
        if (GetDistance(x1, y1, x2, y2) > 1)
        {
            return new List<(int, int)>();
        }

        List<(int, int)> output = new();
        output.Add((x2,y2));
        Axial a = RectangularToAxial(x1, y1);
        Axial b = RectangularToAxial(x2, y2);
        for (int i = 0; i < count; i++)
        {
            Axial d = new Axial(b.q - a.q, b.r - a.r);
            Axial o = new Axial(b.q + d.q, b.r + d.r);
            (int, int) rect = AxialToRectangular(o);
            if (isValid(rect))
            {
                output.Add(rect);
                a = b;
                b = o;
            }
            else
            {
                break;
            }
        }

        return output;
    }
}