using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject A;  //A라는 GameObject변수 선언
    Transform AT;
    void Start()
    {
        AT = A.transform;
    }
    void LateUpdate()
    {
        transform.position = new Vector3(AT.position.x, AT.position.y, transform.position.z);
    }
}