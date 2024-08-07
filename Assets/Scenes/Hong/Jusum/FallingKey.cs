using UnityEngine;
using Cysharp.Threading.Tasks; 

public class FallingKey: MonoBehaviour
{
    private void Start()
    {
        DestroyAfterDelay().Forget();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log("Game Clear");
            Destroy(gameObject);
        }
    }

    private async UniTask DestroyAfterDelay()
    {
        await UniTask.Delay(5000);
        Destroy(gameObject);
        Debug.Log("Game Over!!!!!!!");
    }
}
