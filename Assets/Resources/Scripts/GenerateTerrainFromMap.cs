using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrainFromMap : MonoBehaviour
{
    public Terrain terrain;
    
    public void Generate()
    {
        // 最小値が33かも
        int mapResolution = 257;
        int zoom = mapResolution / 5;
        terrain.GetComponent<TerrainCollider>().terrainData.heightmapResolution = mapResolution;
        terrain.GetComponent<TerrainCollider>().terrainData.baseMapResolution = mapResolution;
        terrain.GetComponent<TerrainCollider>().terrainData.alphamapResolution = mapResolution;
        float[, ]  heights = new float[mapResolution,mapResolution];

        for (int i = 0; i < TerrainData.instance.map.Count; i++) {
            for (int j = 0; j < TerrainData.instance.map[i].Count; j++) {
                for (int k = 10 - 1; k >= 0; k--) {
                    if (TerrainData.instance.map[i][j][k] != -1) {
                        for (int l = 0; l < zoom; l++) {
                            for (int m = 0; m < zoom; m++) {
                                heights[i * zoom + l,j * zoom + m] = (k + 1f) / 10.0f;
                            }
                        }
                        break;
                    }
                }
            }
        }

        Filter(ref heights, mapResolution, 7);
        Filter(ref heights, mapResolution, 7);
        Filter(ref heights, mapResolution, 7);
        Filter(ref heights, mapResolution, 7);
        // Filter(ref heights, mapResolution, 5);

        terrain.GetComponent<TerrainCollider>().terrainData.SetHeights(0,0,heights);
        terrain.GetComponent<TerrainCollider>().terrainData.SyncHeightmap();
        
        UnityEngine.TerrainData td = terrain.terrainData;
        // 高さは80 なので 80 / 10 * x
        for (int i = 1; i < 10; i++)
        {
            ApplyTextureByHeight(ref td, 8 * (i - 0.1f), 8 * (i - 0.05f), 1);
            // ApplyTextureByHeight(ref td, 0.9f * 8 * i, 0.98f * 8 * i, 1);
        }
        
        float[, ]  _heights = new float[mapResolution,mapResolution];
        for (int i = 0; i < mapResolution; i++) {
            for (int j = 0; j <mapResolution; j++) {
                _heights[i, j] = 0f;
            }
        }

        // terrain.GetComponent<TerrainCollider>().terrainData.SetHeights(0,0,_heights);
        // terrain.GetComponent<TerrainCollider>().terrainData.SyncHeightmap();
        //
    }

    void Filter(ref float[,] heights, int size, int filtersize)
    {
        float[,] _heights = new float[size,size];
        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++) {

                float sum = 0;
                float count = 0;
                for (int k = -filtersize; k < filtersize + 1; k++) {
                    for (int l = -filtersize; l < filtersize + 1; l++) {
                        if (i + k < 0 || i + k >= size || j + l < 0 || j + l >= size) {
                            sum += heights[i, j];
                            count++;
                            continue;
                        }
                        sum += heights[i + k, j + l];
                        count++;
                    }
                }
                _heights[i, j] = sum / count;
                // _heights[i, j] = sum / Mathf.Pow(filtersize*2 + 1,2f);
                // if (heights[i, j] > 0)
                // {
                //     Debug.Log($"{heights[i, j]}; _heights[{i}, {j}] = {sum} / {count};  {_heights[i, j]}");
                // }
            }
        }
        
        heights = _heights;
    }
    
    

    void ApplyTextureByHeight(ref UnityEngine.TerrainData terrainData, float minHeight, float maxHeight, int textureIndex) {
        float[,,] splatmapData = terrainData.GetAlphamaps(0, 0, terrainData.alphamapWidth, terrainData.alphamapHeight);
        
        for (int y = 0; y < terrainData.alphamapHeight; y++)
        {
            for (int x = 0; x < terrainData.alphamapWidth; x++)
            {
                float normalizedHeight = terrainData.GetHeight(y, x);
                if (normalizedHeight >= minHeight && normalizedHeight <= maxHeight) {
                    splatmapData[x, y, 1] = 1;
                    splatmapData[x, y, 0] = 0f;
                }

            }
        }
        
        terrain.GetComponent<TerrainCollider>().terrainData.SetAlphamaps(0, 0, splatmapData);
    }
}
