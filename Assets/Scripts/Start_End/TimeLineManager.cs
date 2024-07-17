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
        // 현재 playableDirector에 등록되어 있는 타임라인을 실행
        playableDirector.Play();
        SoundManager.instance.EffectSoundPlay((int)SoundManager.EffectType.ButtonClick);
    }

    public void PlayFromTimeline()
    {
        Debug.Log("hi");
        // 새로운 timeline을 시작
        playableDirector.Play(timeline);
    }
}
