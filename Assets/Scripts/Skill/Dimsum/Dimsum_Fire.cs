using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class Dimsum_Fire : MonoBehaviour
{
    private Animator anim;
    private float _timer;
    
    public SpriteRenderer Dimsum;
    [SerializeField]
    GameObject Effect;
    [SerializeField]
    GameObject Fire;

    public int time = 5;
    public bool isFire = false;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Fire") && !isFire)
        {
            _timer = _timer + Time.deltaTime;
            
            if(_timer > 3f)
            {
                SetFire();
                isFire = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Fire"))
        {
            _timer = 0f;
        }
    }

    private void SetFire()
    {
        SoundManager.instance.EffectSoundPlay((int)SoundManager.EffectType.BigFire);
            
        Effect.SetActive(true);
        Fire.SetActive(true);
            
        anim.SetBool("Skill2", true);
        anim.SetTrigger("doSkill2");
        
        FireDuration(time).Forget();
    }
    
    async UniTask FireDuration(float time)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(time));
        Fire.SetActive(false);
        Effect.SetActive(false);
        anim.SetBool("Skill2", false);
        _timer = 0f;
        isFire = false;
    }
}
