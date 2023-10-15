using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class TerrainData : MonoBehaviour
{
    public static TerrainData instance;
    public List<List<List<int>>> map;
    public List<List<int>> heightmap2d;

    void Awake()
    {
        instance = this.GetComponent<TerrainData>();
        init();
    }
    void init()
    {
        map = new List<List<List<int>>>();
        heightmap2d = new List<List<int>>();
        for (int i = 0; i < 5; i++) {
            map.Add(new List<List<int>>());
            for (int j = 0; j < 5; j++) {
                map[i].Add(new List<int>());
                for (int k = 0; k < 10; k++)
                {
                    map[i][j].Add(-1);
                }
            }
        }

        for (int i = 0; i < 5; i++)
        {
            heightmap2d.Add(new List<int>());
            for (int j = 0; j < 5; j++)
            {
                heightmap2d[i].Add(0);
            }
        }

    }
    
    public int fallItem(int x, int y, int type)
    {
        for (int i = map.Count; i >= 1; i--)
        {
            if (map[y][x][i - 1] != -1)
            {
                map[y][x][i] = type;
                heightmap2d[y][x] = i;
                return i;
            } else if (i == 1)
            {
                map[y][x][0] = type;
                heightmap2d[y][x] = 0;
                return i - 1;
            }
        }

        return -1;
    }
}
