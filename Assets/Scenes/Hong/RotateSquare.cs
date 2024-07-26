using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class RotateSquare : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private float delayTime = 3f;
    [SerializeField] private GameObject rotationSquare; 
    [SerializeField] private GameObject platform;

    private float targetAngle;
    private bool isPaused;

    void Start()
    {
        targetAngle = 0f;
        isPaused = false;
        Rotate().Forget();
    }

    async UniTaskVoid Rotate()
    {
        if (rotationSquare == null)
        {
            Debug.LogWarning("할당 오류");
            return;
        }

        while (true)
        {
            if (isPaused)
            {
                await UniTask.Yield();
                continue;
            }

            float currentAngle = NormalizeAngle(rotationSquare.transform.eulerAngles.z);
            float angleToTarget = Mathf.DeltaAngle(currentAngle, targetAngle);

            if (Mathf.Abs(angleToTarget) < rotationSpeed * Time.deltaTime)
            {
                rotationSquare.transform.eulerAngles = new Vector3(0, 0, targetAngle);
                if (Mathf.Approximately(targetAngle, 90f))
                {
                    platform.SetActive(false);   
                }
                else
                {
                    platform.SetActive(true);
                }

                isPaused = true;
                await UniTask.Delay(TimeSpan.FromSeconds(delayTime));
                targetAngle = (targetAngle + 90f) % 360f;
                isPaused = false;
            }
            else
            {
                // 회전 계속
                float step = rotationSpeed * Time.deltaTime;
                rotationSquare.transform.Rotate(Vector3.forward, Mathf.Sign(angleToTarget) * step);
            }

            await UniTask.Yield();
        }
    }

    private float NormalizeAngle(float angle)
    {
        return (angle + 360f) % 360f;
    }
}
