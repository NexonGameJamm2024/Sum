using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCheck : MonoBehaviour
{
    private GameManager _gm;

    private void Start()
    {
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _gm.RestartScene(GameManager.RestartType.Fall);
            SoundManager.instance.EffectSoundPlay((int)SoundManager.EffectType.Fall);
        }
    }
}
