using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject StartPoint;
    [SerializeField]
    GameObject Player;
    [SerializeField]
    FadeController Fade;

    
    public enum RestartType
    {
        Fall,
        Obstacle,
        Button
    }
    private void Update()
    {
        RestartButtom();
    }
    
    public void RestartButtom()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            RestartScene(RestartType.Button);
        }

    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void RestartScene(RestartType type)
    {
        Fade.FadeOut();

        switch (type)
        {
            case RestartType.Fall :
                Invoke("checkScene", 1f);
                break;
            case RestartType.Obstacle :
                Invoke("ResetScene", 1f);
                break;
            default:
                Invoke("ResetScene", 1f);
                break;
                    
        }
    }

    private void checkScene()
    {
        if (SceneManager.GetActiveScene().name == "Dimsum")
        {
            GameObject[] DummyObject = GameObject.FindGameObjectsWithTag("Throw");

            foreach (GameObject temp in DummyObject)
            {
                Destroy(temp);
            }

            Player.GetComponent<PlayerMovement>().ResetValues(StartPoint.transform.position);
            Fade.FadeIn();
        }
        else if (SceneManager.GetActiveScene().name == "JuSum")
        {
            Player.GetComponent<PlayerMovement>().ResetValues(StartPoint.transform.position);
            SceneManager.LoadScene("JuSum");
        }
        else
        { 
            Player.GetComponent<DownSumMovement>().ResetValues(StartPoint.transform.position);
            Fade.FadeIn();
        }
    }

    private void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextScene()
    {
        if (SceneManager.GetActiveScene().name == "JuSum")
            SceneManager.LoadScene("Dimsum");
        else if (SceneManager.GetActiveScene().name == "DownSum")
            SceneManager.LoadScene("JuSum");
        else
            SceneManager.LoadScene("EndScene");

    }
}
