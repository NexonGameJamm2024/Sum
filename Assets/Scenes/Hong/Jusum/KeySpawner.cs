using System.Collections;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class KeySpawner : MonoBehaviour
{
    public GameObject keyPrefab;

    [Header("최소 소환 주기")]
    public float minSpawnInterval = 30.0f;

    [Header("최대 소환 주기")]
    public float maxSpawnInterval = 60.0f;

    private float spawnHeight = 10.0f;
    private float spawnInterval;

    private Camera mainCamera;

    private async void Start()
    {
        mainCamera = Camera.main;
        spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        await UniTask.Delay((int)(spawnInterval * 1000));
        SpawnKey();
    }

    private void SpawnKey()
    {
        if (keyPrefab == null)
        {
            Debug.LogError("할당 오류");
            return;
        }

        float cameraWidth = 2f * mainCamera.orthographicSize * mainCamera.aspect;
        float halfCameraWidth = cameraWidth / 2f;

        float randomX = Random.Range(-halfCameraWidth, halfCameraWidth);
        Vector3 spawnPosition = new Vector3(randomX, spawnHeight, 0f);

        GameObject spawnedObject = Instantiate(keyPrefab, spawnPosition, Quaternion.identity, transform);
        spawnedObject.transform.parent = transform;
    }
}
