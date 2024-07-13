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

    private void Update()
    {
        RestartButtom();
    }
    public void RestartButtom()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }

    }

    public void RestartScene()
    {
        if (SceneManager.GetActiveScene().name == "Dimsum")
        {
            GameObject[] DummyObject = GameObject.FindGameObjectsWithTag("Throw");

            foreach(GameObject temp in DummyObject)
            {
                Destroy(temp);
            }

            Player.GetComponent<PlayerMovement>().ResetValues(StartPoint.transform.position);
        }
        else if (SceneManager.GetActiveScene().name == "JuSum")
        {
            Player.GetComponent<PlayerMovement>().ResetValues(StartPoint.transform.position);
            SceneManager.LoadScene("JuSum");
        }
        else
            Player.GetComponent<PlayerMovement>().ResetValues(StartPoint.transform.position);
    }

    public void NextScene()
    {
        if (SceneManager.GetActiveScene().name == "Dimsum")
            SceneManager.LoadScene("DownSum");
        else if (SceneManager.GetActiveScene().name == "DownSum")
            SceneManager.LoadScene("JuSum");
        else
            SceneManager.LoadScene("End");

    }
}
