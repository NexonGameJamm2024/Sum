using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextScirpt : MonoBehaviour
{
    public Text text;

    private string a = "�ְ� �ܿ� ";
    private string b = "��?";
    private Text temp;
    // Start is called before the first frame update

    private void Awake()
    {
        text.text = a + ((int)(SoundManager.instance.Timer / 60)).ToString() + b;
    }
}
