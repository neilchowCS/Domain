using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSpace
{
    public List<BattleTile> tiles;
    public List<BattleTile> tiles0;
    public List<BattleTile> tiles1;

    public BattleSpace()
    {
        int numRows = 6;
        int numColumns = 8;
        int count = numRows * numColumns;
        int num = 0;
        tiles = new List<BattleTile>();
        tiles0 = new List<BattleTile>();
        tiles1 = new List<BattleTile>();

        for (int i = 0; i<numColumns; i++)
        {
            for (int j = 0; j<numRows; j++)
            {
                num++;
                BattleTile x = new BattleTile(i, 0, j);
                tiles.Add(x);
                if (num > count / 2)
                {
                    tiles1.Add(x);
                }
                else
                {
                    tiles0.Add(x);
                }
            }
        }

    }
}