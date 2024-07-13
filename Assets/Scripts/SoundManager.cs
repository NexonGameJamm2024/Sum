using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource bgSound;
    public AudioSource effectSound;
    public AudioSource WalkSound;
    public AudioSource WindSound;
    public AudioSource WaveSound;
    public AudioClip[] bgList;
    public AudioClip[] effectList;
    public AudioClip[] walkList;
    public enum EffectType
    {
        Fall,
        Talk,
        DimComebackToInvisible,
        DimMove,
        DimSwing,
        GameClear,
        JusMove,
        ButtonClick,
        PlopSound,
        WhyMove,
        WhyReget,
        WhyTaken
    };

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "StartScene")
        {
            BgSoundPlay(bgList[1]);
        }
        else if (arg0.name == "Dimsum")
        {
            bgSound.Stop();
            BgSoundPlay(bgList[0]);
            WalkSound.clip = walkList[0];
        }
        else if (arg0.name == "DownSum")
        {
            bgSound.Stop();
            BgSoundPlay(bgList[1]);
        }
        else if (arg0.name == "JuSum")
        {
            bgSound.Stop();
            BgSoundPlay(bgList[2]);
        }
        else if (arg0.name == "EndScene")
        {
            BgSoundPlay(bgList[2]);
            WaveSound.Stop();
            WindSound.Stop();
            if (arg0.name == "DeadEnding")
            {
                //SoundManager.instance.EffectSoundPlay((int)SoundManager.EffectType.Die);
            }
        }
    }

    public void BgSoundPlay(AudioClip clip)
    {
        bgSound.clip = clip;
        bgSound.loop = true;
        bgSound.volume = 0.35f;
        bgSound.Play();
    }

    public void EffectSoundPlay(int type)
    {
        effectSound.volume = 0.7f;
        effectSound.PlayOneShot(effectList[type]);
    }

    public void HorrorEffectSoundPlay(AudioClip horror)
    {
        effectSound.volume = 1f;
        effectSound.PlayOneShot(horror);
    }

}