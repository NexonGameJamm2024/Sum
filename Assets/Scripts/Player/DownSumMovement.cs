using UnityEngine;
using UnityEngine.UIElements;

public class DownSumMovement : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public float JumpForce = 10f;
    [SerializeField] private Transform groundPoint = null;
    public LayerMask GroundLayer;
    public float rotationSpeed = 5f;
    public Sprite[] Bodys;
    private Vector3 lastPos;
    [SerializeField] GameObject Sunglass;
    [SerializeField] GameObject Body;
    private string currentState;

    private float _MoveSpeed;
    private float _jumpForce;
    private Animator bodyAnim;
    public bool isDead = false;

    private bool isGrounded;
    private bool isMove;
    private Rigidbody2D rb;
    private SunGlass SG;

    private void Start()
    {
        TryGetComponent(out rb);
        _MoveSpeed = MoveSpeed;
        _jumpForce = JumpForce;
        SG = Sunglass.GetComponent<SunGlass>();
        bodyAnim = Body.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            Move();
        }
    }

    private void Update()
    {
        if (!isDead)
        {
            GroundCheck();
            Jump();
        }
    }

    private void Move()
    {
        if(Input.GetAxis("Horizontal") > 0)
        {
            float moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * _MoveSpeed, rb.velocity.y);
            Body.transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
            if (currentState != "Walk")
            {
                SoundManager.instance.WalkSound.Play();
                currentState = "Walk";
            }
            if (!SG.isAir)
                Sunglass.transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            float moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * _MoveSpeed, rb.velocity.y);
            Body.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            if (currentState != "Walk")
            {
                SoundManager.instance.WalkSound.Play();
                currentState = "Walk";
            }
            if (!SG.isAir)
                Sunglass.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        else
        {
            if (currentState != "Idle")
            {
                SoundManager.instance.WalkSound.Stop();
                currentState = "Idle";
            }
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }

    private void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            SoundManager.instance.EffectSoundPlay((int)SoundManager.EffectType.Jump);
            rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

            //Debug.Log("나 뛰었다");
        }
    }

    private void GroundCheck()
    {
        Debug.DrawRay(groundPoint.position, Vector3.down * 0.2f, new Color(0, 1, 0));
        var ang = Physics2D.RaycastAll(groundPoint.position, Vector2.down, 0.1f, GroundLayer);
        isGrounded = ang.Length > 0;
    }

    public void ResetValues(Vector3 ResetPosition)
    {
        isGrounded = false;
        _MoveSpeed = MoveSpeed;
        _jumpForce = JumpForce;
        bodyAnim.SetBool("isIdle", true);
        transform.position = ResetPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && rb.velocity.y < 0)
        {
            currentState = "Idle";
        }
    }
}
