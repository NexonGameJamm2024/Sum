using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Dimsum_Skill1 : MonoBehaviour
{
    public float maxY;
    public float minY;
    public float checkMaxY;
    public float checkMinY;

    [SerializeField]
    private GameObject Wall;
    [SerializeField]
    private Transform CreatePosition;

    private Rigidbody2D rb;
    private float x;
    private float Timer;
    private Vector3 lastPos;
    public bool isDrag = false;
    private bool isFirst = false;

    private void Start()
    {
        TryGetComponent(out rb);
    }

    private void OnMouseDrag()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (!isFirst)
        {
            x = pos.x;
            isFirst = true;
            rb.gravityScale = 0.5f;
            PlayerMovement.isDrag = true;

        }

        if(lastPos.x != transform.localPosition.x)
        {
            lastPos = transform.localPosition;
            Timer = Timer + Time.deltaTime;
            Debug.Log(Timer);
        }

        if (x + 0.5f > pos.x && x - 0.3f < pos.x && maxY > pos.y && minY < pos.y)
        {
            gameObject.transform.position = new Vector2(pos.x, pos.y);
        }
        else if (x + 0.5f > pos.x && x - 0.3f < pos.x && minY > pos.y)
        {
            gameObject.transform.position = new Vector2(pos.x, minY);
        }
        else if(x + 0.5f > pos.x && x - 0.3f < pos.x && maxY < pos.y)
        {
            gameObject.transform.position = new Vector2(pos.x, maxY);
        }
        else if(maxY > pos.y && minY < pos.y)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, pos.y);
        }
        else if(maxY < pos.y)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, maxY);
        }
        else if (minY > pos.y)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, minY);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PlayerMovement.isDrag && Timer > 0.5f)
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            GameObject temp = Instantiate(Wall, CreatePosition.position, Quaternion.identity);
        }

        PlayerMovement.isDrag = false;
        rb.gravityScale = 1f;
        isFirst = false;
        Timer = 0f;
    }
}
