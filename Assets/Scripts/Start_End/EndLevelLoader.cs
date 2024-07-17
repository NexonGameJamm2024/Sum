using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelLoader : MonoBehaviour
{
    void OnEnable()
    {
        SoundManager.instance.Timer = 0f;
        SoundManager.instance.VomitCount = 0;
        SoundManager.instance.RapCount = 0;
        SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
    }
}
