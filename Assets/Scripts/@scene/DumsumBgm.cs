using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumsumBgm : MonoBehaviour
{
    void Start()
    {
        SoundManager.instance.EffectSoundPlay((int)SoundManager.EffectType.DimComebackToInvisible);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
