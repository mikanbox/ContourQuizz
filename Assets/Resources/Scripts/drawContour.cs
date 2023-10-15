using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class drawContour : MonoBehaviour
{
    [SerializeField]
    public GameObject baseObject;
    
    public void drawContourOnbject(List<List<int>> heightmap, GameObject baseMesh)
    {
        // heights map に応じて object の上に描画する
        Bounds bounds = baseMesh.GetComponent<MeshRenderer>().bounds;
        Vector3 meshSize = bounds.size;
        
        Vector3[] vertices = baseMesh.GetComponent<Mesh>().vertices;
        float xmin = Single.PositiveInfinity, xmax = Single.NegativeInfinity, zmin = Single.PositiveInfinity, zmax = Single.NegativeInfinity;
        foreach (var v in vertices)
        {
            xmin = Mathf.Min(xmin, v.x);
            xmax = Mathf.Max(xmax, v.x);
            zmin = Mathf.Min(zmin, v.z);
            zmax = Mathf.Max(zmax, v.z);
        }
        float y = vertices[0].y;
        
        var res = ExtractSameHeightMaps(heightmap);
    }
    
    void Start() {
         List<List<int>> heightmap = new List<List<int>>
         {
             new List<int>{1,0,0,0},
             new List<int>{0,0,0,0},
             new List<int>{0,0,2,2},
             new List<int>{0,1,2,2}
         };
         var res = ExtractSameHeightMaps(heightmap);
         
         // heights map に応じて object の上に描画する
         // Bounds bounds = baseObject.GetComponent<MeshRenderer>().bounds;
         // Vector3 meshSize = bounds.size;
         //
         Vector3[] vertices = baseObject.GetComponent<MeshFilter>().mesh.vertices;
         float xmin = Single.PositiveInfinity, xmax = Single.NegativeInfinity, zmin = Single.PositiveInfinity, zmax = Single.NegativeInfinity;
         foreach (var v in vertices)
         {
             xmin = Mathf.Min(xmin, v.x);
             xmax = Mathf.Max(xmax, v.x);
             zmin = Mathf.Min(zmin, v.z);
             zmax = Mathf.Max(zmax, v.z);
         }
         float y = vertices[0].y;
         
    }



    static List<KeyValuePair<int,List<Vector2Int>>> ExtractSameHeightMaps(List<List<int>> heightmap)
    {
        List<KeyValuePair<int,List<Vector2Int>>> res = new List<KeyValuePair<int,List<Vector2Int>>>();
        int maxheight = 10;

        for (int h = maxheight; h >0; h--) {
            var visited = new bool[heightmap.Count, heightmap[0].Count];
            
            for (int i = 0; i < heightmap.Count; i++) {
                for (int j = 0; j < heightmap[0].Count; j++) {
                    
                    if (heightmap[i][j] >= h && !visited[i, j])
                    {
                        Debug.Log("Find : " + h + $"({i}, {j})");
                        List<Vector2Int> extractedMap = new List<Vector2Int>();
                        Queue<Vector2Int> q = new Queue<Vector2Int>();
                        q.Enqueue(new Vector2Int(i,j));

                        while (q.Count != 0) {
                            var p = q.Dequeue();
                            if (p.x < 0 || p.x >= heightmap.Count || p.y < 0 || p.y >= heightmap.Count) continue;
                            if (visited[p.x,p.y] || heightmap[p.x][p.y] < h) continue;
                            extractedMap.Add(p);
                            visited[p.x,p.y] = true;
                            
                            q.Enqueue(new Vector2Int(p.x - 1, p.y));
                            q.Enqueue(new Vector2Int(p.x + 1, p.y));
                            q.Enqueue(new Vector2Int(p.x , p.y - 1));
                            q.Enqueue(new Vector2Int(p.x , p.y + 1));
                        }
                        res.Add(new KeyValuePair<int, List<Vector2Int>>(h,extractedMap));
                    }
                }
            }
            
        }

        return res;
    }

}
