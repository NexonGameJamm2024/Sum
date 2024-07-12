using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Target;
    Transform TargetTr; 

    void Start()
    {
        TargetTr = Target.transform; 
    }

    void LateUpdate()
    {
        if (!PlayerMovement.isDrag)
        {
            transform.position = new Vector3(TargetTr.position.x, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
