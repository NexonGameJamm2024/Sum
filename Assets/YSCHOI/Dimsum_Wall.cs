using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dimsum_Wall: MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    private Vector3 scale;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (gameObject.transform.localScale.x < 1f)
        {
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x + 0.001f, gameObject.transform.localScale.y + 0.001f);
            gameObject.transform.position = new Vector2(gameObject.transform.position.x + 0.003f, gameObject.transform.position.y);
        }
    }
}
