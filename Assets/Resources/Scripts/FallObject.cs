using UnityEngine;

public class FallObject : MonoBehaviour
{
    private Vector2Int pos;
    private int height;

    public void init(int x, int y)
    {
        pos = new Vector2Int(x, y);
        height = TerrainData.instance.fallItem(x, y, 0);
    }
}
