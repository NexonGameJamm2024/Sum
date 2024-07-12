using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    string text = "왜들 그리 다운돼있섬 뭐가 문제야 Say Something 분위기가 겁나 싸한디 요즘 이게 유행이여 알어?";
    private float Timer;
    private BoxCollider2D boxCollider;
    private int index = 0;
    public float speed;

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
        //transform.Translate(transform.right * speed * Time.deltaTime);
        if (Timer > 0.1f && index < text.Length)
        {
            Timer = 0f;
            this.gameObject.GetComponent<TextMeshPro>().text += text[index];
            boxCollider.size = new Vector2(boxCollider.size.x + 0.22f, boxCollider.size.y);
            boxCollider.offset = new Vector2(boxCollider.offset.x + 0.1f, boxCollider.size.y);
            index++;
        }   

        
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(8);
        Destroy(gameObject);
    }
}
