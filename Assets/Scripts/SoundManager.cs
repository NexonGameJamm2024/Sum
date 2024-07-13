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
    public AudioSource walkSound;
    public AudioSource WindSound;
    public AudioSource WaveSound;
    public AudioClip[] bgList;
    public AudioClip[] effectList;
    public AudioClip[] walkList;
    public enum EffectType
    {
        ButtonClick
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
        if (arg0.name == "Intro")
        {
            BgSoundPlay(bgList[0]);
        }
        else if (arg0.name == "MainMenu")
        {
            //BgSoundPlay(bgList[1]);
            bgSound.Stop();
        }
        else if (arg0.name == "DimSum")
        {
            //BgSoundPlay(bgList[1]);
            bgSound.Stop();
            walkSound.clip = walkList[0];
            WindSound.Play();
        }
        else if (arg0.name == "DownSum")
        {
            //BgSoundPlay(bgList[1]);
            walkSound.clip = walkList[1];
            WaveSound.Play();
        }
        else if (arg0.name == "End")
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