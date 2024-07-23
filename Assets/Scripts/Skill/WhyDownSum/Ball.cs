using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    public float ballSpeed = 10f;

    private Vector2 ballDirection;
    private bool isBallReleased = false;

    [SerializeField] private Vector3 playerPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        ballDirection = Vector2.up.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBallReleased)
        {
            Vector3 ballPosition = playerPosition;
            ballPosition.y += 0.185f;
            transform.position = ballPosition;

            if (Input.GetMouseButtonDown(0))
            {
                isBallReleased = true;
                ballDirection = new Vector2(Random.Range(-1f, 1f), 1).normalized;
            }
        }
        else
        {
            {
                transform.Translate(ballDirection * (ballSpeed * Time.deltaTime));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            ballDirection = Vector2.Reflect(ballDirection, other.contacts[0].normal);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            float hitPoint = other.contacts[0].point.x;
            float playerCenter = other.transform.position.x;
            float angle = (hitPoint - playerCenter) * 2.0f;
            ballDirection = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)).normalized;
        }
    }
}
