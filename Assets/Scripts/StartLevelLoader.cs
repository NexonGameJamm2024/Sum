using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLevelLoader : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("DownSum", LoadSceneMode.Single);
    }
}
