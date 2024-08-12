using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dimsum_Wall: MonoBehaviour
{
    //private Rigidbody2D _rb;
    private PolygonCollider2D _col;
    private Transform _tf;

    [SerializeField] private bool direction = true;
    [SerializeField] private float maxScale = 0.75f;
    [SerializeField] private float Scale = 0.005f;
    [SerializeField] private float Speed = 0.05f;
    
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out _tf);
        TryGetComponent(out _col);
        //TryGetComponent(out _rb);
    }

    private void Update()
    {
        if (_tf.localScale.x < maxScale)
        {
            _tf.localScale = new Vector2(_tf.localScale.x + Scale, _tf.localScale.y + Scale);
            if(direction)
                _tf.position = new Vector2(_tf.position.x + Speed, _tf.position.y);
            else
                _tf.position = new Vector2(_tf.position.x - Speed, _tf.position.y);
        }
        else
        {
            _col.enabled = true;
            //_rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
