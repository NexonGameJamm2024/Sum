using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Target;
    Transform TargetTr;
    public float maxY = 40f;
    public float minY = 24f;

    void Start()
    {
        TargetTr = Target.transform;
    }

    void LateUpdate()
    {
        float targetY = TargetTr.position.y;

        if (targetY > maxY)
        {
            transform.position = new Vector3(TargetTr.position.x, maxY, transform.position.z);
        }
        else if (targetY < minY)
        {
            transform.position = new Vector3(TargetTr.position.x, minY, transform.position.z);
        }
        else if (!PlayerMovement.isDrag)
        {
            transform.position = new Vector3(TargetTr.position.x, targetY, transform.position.z);
        }
    }
}
