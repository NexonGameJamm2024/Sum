using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using Cysharp.Threading.Tasks;

public class JellySpawnManager : MonoBehaviour
{
    // Start is called before the first frame update

    private BoxCollider2D _spawnRange;
    public int spawnCount = 4;
    public float spawnTime = 2f;
    [SerializeField] private GameObject[] jelly;
    private void Awake()
    {
        TryGetComponent(out _spawnRange);
        Spawn(spawnTime).Forget();
    }
    
    private Vector2 GetRandomPosition()
    {
        Vector2 basePosition = transform.position;
        Vector2 size = _spawnRange.bounds.size;                 

        //x, y축 랜덤 좌표 얻기
        float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posY = basePosition.y + Random.Range(-size.y / 2f, size.y / 2f);

        Vector2 spawnPos = new Vector2(posX, posY);

        return spawnPos;
    }
    
    async UniTask Spawn(float time)
    {
        for (int i = 0; i < spawnCount; i++) //count만큼 책 생성
        {
            Vector2 spawnPos = GetRandomPosition(); //랜덤 위치 return
            int jellyNum = Random.Range(0, 3); 
            
            //원본, 위치, 회전값을 매개변수로 받아 오브젝트 복제
            GameObject instance = Instantiate(jelly[jellyNum], spawnPos, Quaternion.identity);
        }
        
        await UniTask.Delay(TimeSpan.FromSeconds(time));
        
        Spawn(time).Forget();
    }
}
