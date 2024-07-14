using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetCount : MonoBehaviour
{
    // Start is called before the first frame update
    public int type;
    [SerializeField]
    TextMeshPro txt;

    private void Awake()
    {
        if(type == 0)
        {
            txt.text = SoundManager.instance.VomitCount.ToString();
            //txt.text = "15";
        }
        else if (type == 1)
        {
            txt.text = SoundManager.instance.RapCount.ToString();
            //txt.text = "16";
        }
        else
        {
            int timer = (int)SoundManager.instance.Timer;
            string a = (timer / 60).ToString();
            string b = ":";
            string c = (timer % 60).ToString();
            string d = "√ ";
            txt.text = a + b+ c + d;
        }
    }
}
