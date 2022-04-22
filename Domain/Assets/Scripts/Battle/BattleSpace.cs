using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSpace
{
    /// <summary>
    /// List of all tiles.
    /// </summary>
    public List<BattleTile> tiles;
    /// <summary>
    /// List of tiles on player 0 side.
    /// </summary>
    public List<BattleTile> tiles0;
    /// <summary>
    /// List of tiles on player 1 side.
    /// </summary>
    public List<BattleTile> tiles1;

    /// <summary>
    /// Constructor for BattleSpace.
    /// Creates numRows (6) * numColumns (8) tiles and adds them to list.
    /// </summary>
    public BattleSpace()
    {
        int numRows = 6;
        int numColumns = 8;
        int count = numRows * numColumns;

        float initX = -5.249f;
        float distX = -5.249f - (-3.749f);

        float initZ1 = -4.763f;
        float initZ2 = -3.897f;
        float distZ = -4.763f - (-3.031f);

        int num = 0;
        tiles = new List<BattleTile>();
        tiles0 = new List<BattleTile>();
        tiles1 = new List<BattleTile>();

        for (int i = 0; i < numColumns; i++)
        {
            for (int j = 0; j < numRows; j++)
            {
                num++;
                BattleTile x;

                if (i % 2 == 0)
                {
                    x = new BattleTile(initX - (distX * i), 0f, initZ1 - (distZ * j), i * numRows + j);

                }
                else
                {
                    x = new BattleTile(initX - (distX * i), 0f, initZ2 - (distZ * j), i * numRows + j);
                }

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

        Debug.Log(tiles0.Count);
        Debug.Log(tiles1.Count);
    }
}