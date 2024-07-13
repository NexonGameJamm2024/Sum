using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCheck : MonoBehaviour
{
    [SerializeField]
    GameObject GameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.GetComponent<GameManager>().RestartScene();
    }
}
