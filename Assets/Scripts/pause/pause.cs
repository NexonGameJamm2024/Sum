using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause : MonoBehaviour
{
    [SerializeField]
    private GameObject PausePanel;

    [SerializeField]
    private GameObject a;

    [SerializeField]
    private GameObject b;

    private bool isCheck;

    void Update()
    {
        OpenPausePanel();
    }

    void OpenPausePanel()
    {
        if (!isCheck && Input.GetKeyDown(KeyCode.Escape))
        {
            PausePanel.SetActive(true);
            isCheck = true;
        }
        else if (isCheck && Input.GetKeyDown(KeyCode.Escape))
        {
            PausePanel.SetActive(false);
            isCheck = false;
        }
    }

    public void ExitBtn()
    {
        Application.Quit();
    }

    public void GoToMain()
    {
        SceneManager.LoadScene("StartScene");
    }
}
