using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class GeneratePanels : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;
    private Vector2 panelSize = new Vector2(5, 5); 
    // Start is called before the first frame update
    void Start()
    {
        if (panel != null)
        {
            for (int i = 0; i < panelSize.Y; i++)
            {
                for (int j = 0; j < panelSize.X; j++)
                {
                    float z = (1.0f / panelSize.X) * i - 0.2f * (panelSize.X - 1) /2.0f ;
                    float x = (1.0f / panelSize.Y) * j - 0.2f * (panelSize.Y - 1) /2.0f;
                    // Transform tr = panel.transform;
                    // tr.position = new UnityEngine.Vector3(x,0,z);
                    GameObject obj = Instantiate(panel, transform);
                    obj.SetActive(true);
                    obj.GetComponent<ObjectValue>().index[0] = -1;
                    obj.GetComponent<ObjectValue>().index[1] = i;
                    obj.GetComponent<ObjectValue>().index[2] = j;
                    obj.transform.localPosition = new UnityEngine.Vector3(x,0,z);
                    
                }
                
                
            }
        }
    }
}
