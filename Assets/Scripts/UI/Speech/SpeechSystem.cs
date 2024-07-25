using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeechSystem : MonoBehaviour
{
    [Header("텍스트 등장 second")]
    [SerializeField]
    private float AppearDelay = 10f;

    [Header("텍스트 퇴장 second")]
    [SerializeField]
    private float DisappearDelay = 4f;

    [Header("텍스트")]
    [SerializeField]
    private string[] texts = { "첫 번째 텍스트입니다.", "두 번째 텍스트입니다.", "세 번째 텍스트입니다." };

    [Header("TextMesh")]
    [SerializeField]
    TMP_Text SpeechText;


    void Start()
    {
        InvokeRepeating("ChangeText", 0f, AppearDelay);
    }

    void ChangeText()
    {
        int randomIndex = Random.Range(0, 3);

        SpeechText.text = texts[randomIndex];
        Invoke("ClearText", DisappearDelay);
    }

    void ClearText()
    {
        SpeechText.text = "";
    }
}
