using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class TerrainData : MonoBehaviour
{
    public static TerrainData instance;
    private List<List<List<int>>> map;

    void Awake()
    {
        instance = this.GetComponent<TerrainData>();
        init();
    }
    void init()
    {
        map = new List<List<List<int>>>(10);
        for (int i = 0; i < 5; i++) {
            map.Add(new List<List<int>>(5));
            for (int j = 0; j < 5; j++) {
                map[i].Add(new List<int>(5));
                for (int k = 0; k < 5; k++)
                {
                    map[i][j][k] = -1;
                }
            }
        }
    }
    
    public int fallItem(int x, int y, int type)
    {
        for (int i = map.Count; i >1; i--)
        {
            if (map[i - 1][y][x] != -1)
            {
                map[i][y][x] = type;
                return i;
            }
        }

        return -1;
    }
}
