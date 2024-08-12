using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;
using System;

public class WhySum_Obstacle : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TMP_Text bridge;
    public Transform createPosition;
    public Transform playerTf;
    [SerializeField] private float time = 1f;

    private bool activeMask = false;


    public void SetActive()
    {
        RepeatTask(time).Forget();
    }

    public void SetActiveMask()
    {
        activeMask = true;
    }
    
    private void Obstacle()
    {
        SoundManager.instance.EffectSoundPlay((int)SoundManager.EffectType.Talk);
        SoundManager.instance.RapCount++;
        
        Vector3 v = playerTf.position - gameObject.transform.position;
        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        Vector3 vDir = v.normalized;
        
        TMP_Text temp = Instantiate(bridge, createPosition.position + vDir * 1.5f, Quaternion.Euler(0, 0, angle));
    }
    
    async UniTask RepeatTask(float time)
    {
        Obstacle();
        
        await UniTask.Delay(TimeSpan.FromSeconds(time));
        
        if(!activeMask)
            RepeatTask(time).Forget();
    }
}
