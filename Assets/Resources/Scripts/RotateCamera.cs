using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField]
    private Camera mainCam;
    void Awake()
    {
        mainCam = Camera.main;
    }


    public void Rotate(string direction)
    {
        if (direction == "Left")
        {
            leftRotate();
        }
        if (direction == "Right")
        {
            rightRotate();
        }
    }
    void leftRotate()
    {
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, -90, 0), Vector3.one);
        Vector3 originalPos = mainCam.transform.position;
        Vector3 rotatedPos = rotationMatrix.MultiplyPoint(originalPos);
        mainCam.transform.position = rotatedPos;
        
        Vector3 rotationAngles = mainCam.transform.eulerAngles;
        var targetRotation = new Vector3(20, rotationAngles.y - 90, 0);
        var targetQuaternion = Quaternion.Euler(targetRotation);
        mainCam.transform.rotation = targetQuaternion;
    }

    void rightRotate()
    {
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, 90, 0), Vector3.one);
        Vector3 originalPos = mainCam.transform.position;
        Vector3 rotatedPos = rotationMatrix.MultiplyPoint(originalPos);
        mainCam.transform.position = rotatedPos;
        
        Debug.Log(mainCam.transform.rotation);
        Vector3 rotationAngles = mainCam.transform.eulerAngles;
        var targetRotation = new Vector3(20, rotationAngles.y + 90, 0);
        var targetQuaternion = Quaternion.Euler(targetRotation);
        mainCam.transform.rotation = targetQuaternion;
    }
    
}
