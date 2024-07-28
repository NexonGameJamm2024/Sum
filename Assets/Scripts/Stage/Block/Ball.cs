using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System;
using Cysharp.Threading.Tasks;

public class Ball : MonoBehaviour
{
    public float ballSpeed = 10f;

    private Vector2 _ballDirection;
    private bool _isBallReleased = false;

    [SerializeField] private Vector3 playerPosition;

    private Rigidbody2D _rb;
    private CircleCollider2D _cc;
    private GameObject _temp;
    private bool _isDel = false;
    private int _count = 0;
    private Vector3 _ballPos;
    
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out _rb);
        TryGetComponent(out _cc);
        playerPosition = GameObject.Find("WhyDownSum").transform.position;
        _ballDirection = Vector2.up.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isBallReleased)
        {
            Vector3 ballPosition = playerPosition;
            ballPosition.y += 0.185f;
            transform.position = ballPosition;

            if (Input.GetMouseButtonDown(0))
            {
                _isBallReleased = true;
                _ballDirection = new Vector2(Random.Range(-1f, 1f), 1).normalized;
            }
        }
        else
        {
            {
                transform.Translate(_ballDirection * (ballSpeed * Time.deltaTime));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Block"))
        {
            RemoveBrick(other.gameObject);
        }
    }

    void RemoveBrick(GameObject brick)
    {
        if (_count >= 2)
        {
            _count = 0;
            return;
        }

        _ballPos = transform.position;
        Vector2 pos = Vector2.zero;

        Collider2D[] col = Physics2D.OverlapCircleAll(_ballPos, _cc.radius / 2, LayerMask.GetMask("Block"));

        Debug.Log(col.Length);
        
        _count = col.Length;

        GameObject[] colObj = new GameObject[col.Length];
        float[] p = new float[2];

        if (col.Length == 3)
        {
            int sour = 0;

            if (_ballDirection.y >= 0)
            {
                if (_ballDirection.x >= 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (col[i].transform.position.x >= _ballPos.x && col[i].transform.position.y >= _ballPos.y)
                        {
                            sour = i;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (col[i].transform.position.x >= _ballPos.x && col[i].transform.position.y >= _ballPos.y)
                        {
                            sour = i;
                        }
                    }
                }
            }
            else
            {
                if (_ballDirection.x >= 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (col[i].transform.position.x >= _ballPos.x && col[i].transform.position.y < _ballPos.y)
                        {
                            sour = i;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (col[i].transform.position.x >= _ballPos.x && col[i].transform.position.y < _ballPos.y)
                        {
                            sour = i;
                        }
                    }
                }
            }
            
            int cnt = 0;

            for (int i = 0; i < 3; i++)
            {
                if (i != sour)
                {
                    p[cnt] = Vector2.Distance(_ballPos, col[i].transform.position);
                    colObj[cnt] = col[i].gameObject;
                    cnt++;
                }
            }

            if (p[0] <= p[1])
                _temp = colObj[0];
            else
                _temp = colObj[1];
        }
        else if (col.Length == 2)
        {
            colObj[0] = col[0].gameObject;
            colObj[1] = col[1].gameObject;

            p[0] = Vector2.Distance(_ballPos, colObj[0].transform.position);
            p[1] = Vector2.Distance(_ballPos, colObj[1].transform.position);

            if (p[0] <= p[1]) _temp = colObj[0];
            else _temp = colObj[1];
        }
        else
            _temp = brick;

        BoxCollider2D bc = _temp.GetComponent<BoxCollider2D>();

        if (col.Length > 1)
        {
            if (colObj[0].transform.position.y == colObj[1].transform.position.y)
            {
                if (_ballDirection.y >= 0)
                    pos = Vector2.down;
                else
                    pos = Vector2.up;
            }
            else if (colObj[0].transform.position.x == colObj[1].transform.position.x)
            {
                if (_ballDirection.x >= 0)
                    pos = Vector2.left;
                else
                    pos = Vector2.right;
            }
            else
            {
                _isDel = true;

                if (_ballDirection.y >= 0)
                {
                    if (_ballDirection.x >= 0)
                        pos = (Vector2.left + Vector2.down).normalized;
                    else
                        pos = (Vector2.right + Vector2.down).normalized;
                }
                else
                {
                    if (_ballDirection.x >= 0)
                        pos = (Vector2.left + Vector2.up).normalized;
                    else
                        pos = (Vector2.right + Vector2.up).normalized;
                }
            }
        }
        else
        {
            if (_ballDirection.y >= 0)
            {
                if (_temp.transform.position.x - (bc.size.x / 2) > _ballPos.x && _ballDirection.x >= 0)
                    pos = Vector2.left;
                else if (_temp.transform.position.x - (bc.size.x / 2) <= _ballPos.x && _ballDirection.x < 0)
                    pos = Vector2.right;
                else
                    pos = Vector2.down;
            }
            else
            {
                if (_temp.transform.position.x - (bc.size.x / 2) > _ballPos.x && _ballDirection.x >= 0)
                    pos = Vector2.left;
                else if (_temp.transform.position.x - (bc.size.x / 2) <= _ballPos.x && _ballDirection.x < 0)
                    pos = Vector2.right;
                else
                    pos = Vector2.up;
            }
            
        }
        
        SetColWait().Forget();

        _ballDirection = Vector2.Reflect(_ballDirection, pos);
        
        Debug.Log(_isDel);
        
        if(!_isDel)
            _temp.GetComponent<Block>().TakeDamage();
        else
        {
            colObj[0].GetComponent<Block>().TakeDamage();
            colObj[1].GetComponent<Block>().TakeDamage();
            _isDel = false;
        }

        _temp = null;
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            _ballDirection = Vector2.Reflect(_ballDirection, other.contacts[0].normal);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            float hitPoint = other.contacts[0].point.x;
            float playerCenter = other.transform.position.x;
            float angle = (hitPoint - playerCenter) * 2.0f;
            _ballDirection = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)).normalized;
        }
    }

    async UniTask SetColWait()
    {
        _cc.enabled = false;
        await UniTask.Delay(TimeSpan.FromSeconds(0.005f));
        _cc.enabled = true;
        _count = 0;
    }
}
