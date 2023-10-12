using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrainFromMap : MonoBehaviour
{
    public Terrain terrain;
    
    public void Generate()
    {
        // ある座標の高さを取得
        float height = terrain.SampleHeight(new Vector3(1, 0, 1));
        UnityEngine.TerrainData td = terrain.terrainData;
        
        // [x,y]の高さマップを取得
        var heights = td.GetHeights(0, 0, td.heightmapResolution, td.heightmapResolution);
        td.SetHeights(0, 0, heights);
        
        
        // terrain
        
//         // 主なテクスチャを取得
//         TerrainData terrainData = terrain.terrainData;
//         Texture2D texture = terrainData.splatPrototypes[0]; 
//
// // テクスチャの設定
//         terrainData.splatPrototypes[0] = texture;
//         terrain.terrainData = terrainData; 
    }
}
