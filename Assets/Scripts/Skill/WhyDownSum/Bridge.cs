using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bridge : MonoBehaviour
{
    public string outputText = "가나다라마바사아자타 파 하하 하 하하하 하 하 하 하";
    private float Timer;
    private BoxCollider2D boxCollider;
    private int index = 0;
    public float speed;
    private Vector3 Rotate;
    private TextMeshPro text;
    public bool isColliderOn = true;
    private bool isDone = false;
    [SerializeField] private float Duration = 10f;
    [SerializeField] private float Blinking = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        text = this.gameObject.GetComponent<TextMeshPro>();
        if (isColliderOn)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            text.color = new Color(0, 0, 0, 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Timer = Timer + Time.deltaTime;
        if (Timer > 0.1f && index < outputText.Length)
        {
            Timer = 0f;
            text.text += outputText[index];
            boxCollider.size = new Vector2(boxCollider.size.x + 0.35f, boxCollider.size.y);
            boxCollider.offset = new Vector2(boxCollider.offset.x + 0.17f, 0);
            index++;
        }
        else if (index == outputText.Length && !isDone)
        {
            isDone = true;
            
            if (!isColliderOn)
            {
                boxCollider.enabled = true;
                text.color = new Color(0, 0, 0, 255f);
            }
            
            DestroyWait().Forget();
            ChangeColor().Forget();
        }

        
    }

    async UniTask DestroyWait()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(Duration));
        Destroy(gameObject);
    }
    
    async UniTask ChangeColor()
    {
        var token = this.GetCancellationTokenOnDestroy();
        bool isRed = false;
        await UniTask.Delay(TimeSpan.FromSeconds(Duration - 2f), cancellationToken: token);
        
        while (true)
        {
            if (!isRed)
            {
                text.color = Color.red;
                isRed = true;
            }
            else
            {
                text.color = Color.black;
                isRed = false;
            }
            
            await UniTask.Delay(TimeSpan.FromSeconds(Blinking), cancellationToken: token);
        }
    }
}
