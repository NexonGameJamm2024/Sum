using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class RotateSquare : MonoBehaviour
{
    [SerializeField] private float rotationAngle = 90f;
    [SerializeField] private float delayTime = 3f;

    [SerializeField] private GameObject RaotationSquare;

    private float currentAngle = 0f;

    void Start()
    {
        RotationObj().Forget();
    }

    void Update()
    {
        CheckRotate();
    }

    private void CheckRotate()
    {
        float remainder = currentAngle % 360;
        if (RaotationSquare.transform.eulerAngles.z == 90)
        {
            Debug.Log("a");
        }
    }

    async UniTaskVoid RotationObj()
    {
        if (RaotationSquare != null)
        {
            while (true)
            {
                float remainder = currentAngle % 360;
                if (Mathf.Abs(remainder - 90) < 0.2f || Mathf.Abs(remainder - -90) < 0.2f ||
                    Mathf.Abs(remainder - 180) < 0.2f || Mathf.Abs(remainder - -180) < 0.2f)
                {
                    RaotationSquare.transform.eulerAngles = new Vector3(0, 0, Mathf.Round(RaotationSquare.transform.eulerAngles.z / 90) * 90);
                    await UniTask.Delay(TimeSpan.FromSeconds(delayTime));

                    currentAngle = 0;
                }
                else
                {
                    RaotationSquare.transform.Rotate(Vector3.forward, rotationAngle * Time.deltaTime);
                    currentAngle += rotationAngle * Time.deltaTime;
                    await UniTask.Yield();
                }
            }
        }
        else
        {
            Debug.LogWarning("할당 오류");
        }
    }

}
