using UnityEngine;
using Cysharp.Threading.Tasks;

public class FallingObjectsSpawner : MonoBehaviour
{
    public GameObject[] fallingObjPrefabs;

    [Header("소환 주기")]
    public float spawnInterval = 2.0f;

    private float SpawnHeight = 10.0f;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        StartSpawningAsync().Forget();
    }

    private async UniTaskVoid StartSpawningAsync()
    {
        while (true)
        {
            SpawnObject();
            await UniTask.Delay(System.TimeSpan.FromSeconds(spawnInterval));
        }
    }

    private void SpawnObject()
    {
        int prefabIndex = Random.Range(0, fallingObjPrefabs.Length);
        GameObject prefab = fallingObjPrefabs[prefabIndex];

        float cameraWidth = 2f * mainCamera.orthographicSize * mainCamera.aspect;
        float halfCameraWidth = cameraWidth / 2f;

        float randomX = Random.Range(-halfCameraWidth, halfCameraWidth);
        Vector3 spawnPosition = new Vector3(randomX, SpawnHeight, 0f);

        GameObject spawnedObject = Instantiate(prefab, spawnPosition, Quaternion.identity, transform);
        spawnedObject.transform.parent = transform;
    }
}
