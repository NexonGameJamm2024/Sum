using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class MovablePlatform : MonoBehaviour
{
    private Transform _pos;
    public float time = 1f;
    public Transform startPos;
    public Transform endPos;
    public float speed;
    private bool canMove = false;

    private void Start()
    {
        _pos = endPos;
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, _pos.position, Time.deltaTime * speed);

            if (transform.position == endPos.position)
            {
                //canMove = false;
                _pos = startPos;
                //MoveWait(time).Forget();
            }
            else if (transform.position == startPos.position)
            {
                //canMove = false;
                _pos = endPos;
                //MoveWait(time).Forget();
            }
        }
    }
    
    async UniTask MoveWait(float time)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(time));
        canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Land")
        {
            other.transform.parent.gameObject.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Land")
        {
            other.transform.parent.gameObject.transform.SetParent(null);
        }
    }
    
}
