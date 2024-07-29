using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeathObstacle : MonoBehaviour
{

    private GameManager _gm;

    private void Start()
    {
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.name == "WhyDownSum")
                other.GetComponent<DownSumMovement>().isDead = true;
            else
                other.GetComponent<PlayerMovement>().isDead = true;
            other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            _gm.RestartScene(GameManager.RestartType.Obstacle);
        }
    }
}
