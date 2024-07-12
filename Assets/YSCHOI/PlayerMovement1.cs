using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{
    public float maxY;
    public float minY;
    public float MoveSpeed = 5f;
    public float JumpForce = 10f;
    public LayerMask GroundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isDrag = false;
    private bool isMaxY = false;
    private Transform tr;

    [SerializeField]
    private GameObject Wall;
    [SerializeField]
    private Transform CreatePosition;

    private void Start()
    {
        TryGetComponent(out rb);
        tr = gameObject.transform;
    }

    private void Update()
    {
        GroundCheck();
        Move();
        Jump();
    }

    private void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * MoveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        if (!isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            isGrounded = true;
            Debug.Log("나 뛰었다");
        }
    }

    private void GroundCheck()
    {
        if (rb.velocity.y < 0) //내려갈떄만 스캔
        {
            Debug.DrawRay(rb.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rb.position, Vector3.down, 1, GroundLayer);
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 1f)
                {
                    isGrounded = false;
                }
            }
        }
    }

    private void OnMouseDrag()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(pos.y - gameObject.transform.position.y, pos.x - gameObject.transform.position.x) * Mathf.Rad2Deg / 2;
        isDrag = true;

        if (maxY < pos.y)
        {
            isMaxY = true;
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, maxY);
        }
        else if (minY > pos.y)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, minY);
            isMaxY = false;
        }
        else
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, pos.y);
            isMaxY = false;
        }

        Debug.Log(angle);

        if (angle <= 90 && angle >= 0)
        {
            gameObject.transform.localEulerAngles = new Vector3(0, 0, angle);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isMaxY && isDrag)
        {
            isDrag = false;
            GameObject temp = Instantiate(Wall, CreatePosition.position, Quaternion.identity);
        }
    }
}
