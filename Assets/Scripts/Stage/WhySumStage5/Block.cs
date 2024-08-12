using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using System;

public class Block : MonoBehaviour
{
    [SerializeField] private int hp = 1;

    public void TakeDamage()
    {
        hp -= 1;
        if (hp == 0)
            DestroyWait().Forget();

    }
    // Start is called before the first frame update
    async UniTask DestroyWait()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.01f));
        Destroy(gameObject);
    }
    
}
