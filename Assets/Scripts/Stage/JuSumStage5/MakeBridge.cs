using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeBridge : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject Wall;
    [SerializeField]
    private Transform CreatePosition;

    private Animator _anim;

    private void Start()
    {
        TryGetComponent(out _anim);
    }

    public void Vomit()
    {
        GameObject temp = Instantiate(Wall, CreatePosition.position, Quaternion.identity);
        _anim.Play("Vomit");
        SoundManager.instance.EffectSoundPlay((int)SoundManager.EffectType.Vomit);
        SoundManager.instance.VomitCount++;
    }
}
