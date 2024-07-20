using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    string text = "가나다라마바사아자타 파 하하 하 하하하 하 하 하 하";
    private float Timer;
    private BoxCollider2D boxCollider;
    private int index = 0;
    public float speed;
    private Vector3 Rotate;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(Destroy));
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Timer = Timer + Time.deltaTime;
        if (Timer > 0.1f && index < text.Length)
        {
            Timer = 0f;
            this.gameObject.GetComponent<TextMeshPro>().text += text[index];
            boxCollider.size = new Vector2(boxCollider.size.x + 0.35f, boxCollider.size.y);
            boxCollider.offset = new Vector2(boxCollider.offset.x + 0.17f, 0);
            index++;
        }
        else if (index == text.Length)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }

        
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
