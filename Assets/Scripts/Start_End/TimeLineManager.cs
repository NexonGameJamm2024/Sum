using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimeLineManager : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public TimelineAsset timeline;

    public void Play()
    {
        // ���� playableDirector�� ��ϵǾ� �ִ� Ÿ�Ӷ����� ����
        playableDirector.Play();
        SoundManager.instance.EffectSoundPlay((int)SoundManager.EffectType.ButtonClick);
    }

    public void PlayFromTimeline()
    {
        Debug.Log("hi");
        // ���ο� timeline�� ����
        playableDirector.Play(timeline);
    }
}
