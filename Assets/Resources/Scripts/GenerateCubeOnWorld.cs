using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCubeOnWorld : MonoBehaviour
{
	//生成されるオブジェクトのプレハブ
	public GameObject boxPrefab;

	[SerializeField]
	Camera _camera;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		//画面タップした際の処理
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out RaycastHit hit))
			{
				Debug.Log("光線が当たったオブジェクトの情報");
				Debug.Log("名前：　" + hit.transform.name);
				Debug.Log("位置：　" + hit.transform.position);
				Debug.Log("距離：　" + hit.distance);
				
				
				hit.transform.GetComponent<MeshRenderer>().material.color = Color.green;
				
				//hit.point ヒットした座標

				Instantiate (boxPrefab, hit.point, Quaternion.identity);
			}
		}
	}

	
	(int, int) GetCellPositionOnGrid(Mesh plane)
	{
		Bounds b = plane.bounds;
		(float, float) xzMaxPosition = (b.max.x, b.max.z);
		(float, float) xzMinPosition = (b.min.x, b.min.z);

		int xCellNum = 4;
		int zCellNum = 4;
		float xCellSize = (xzMaxPosition.Item1 - xzMinPosition.Item1) / xCellNum;
		float zCellSize = (xzMaxPosition.Item2 - xzMinPosition.Item2) / zCellNum;

		return (0, 0);
	}
	
}
