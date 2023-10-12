using UnityEngine;

public class GenerateCubeOnWorld : MonoBehaviour
{
    //生成されるオブジェクトのプレハブ
    [SerializeField]
    private GameObject boxPrefab;

    private Camera mainCam;

    void Awake()
    {
        mainCam = Camera.main;
    }
    
    private void Update()
    {
        //画面タップした際の処理
        if (Input.GetMouseButtonDown(0))
        {
            var ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                Debug.Log("光線が当たったオブジェクトの情報");
                Debug.Log("名前：　" + hit.transform.name);
                Debug.Log("位置：　" + hit.transform.position);
                Debug.Log("距離：　" + hit.distance);

                if (hit.transform.GetComponent<ObjectValue>() != null)
                {
                    if (hit.transform.GetComponent<ObjectValue>().index[0] == -1)
                    {
                        // GameObject obj = Instantiate(boxPrefab, hit.point, Quaternion.identity);
                        GameObject obj = Instantiate(boxPrefab, hit.transform.position, Quaternion.identity);
                        
                        obj.GetComponent<FallObject>().init(hit.transform.GetComponent<ObjectValue>().index[2],hit.transform.GetComponent<ObjectValue>().index[1]);
                        obj.SetActive(true);
                    }
                }
            }
        }
    }
}