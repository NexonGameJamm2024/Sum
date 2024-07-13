using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhyDownSum_Skill1 : MonoBehaviour
{
    public bool isFirst = false;
    private float Timer;
    private Vector3 lastPos;
    public Animator bodyAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseDrag()
    {
        if (!isFirst)
        {
            isFirst = true;
            bodyAnim.SetBool("isIdle", false);
            bodyAnim.SetTrigger("doSurprise");
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90f));
            gameObject.GetComponent<SunGlass>().isAir = true;
        }

        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = new Vector2(pos.x, pos.y);
    }
}
