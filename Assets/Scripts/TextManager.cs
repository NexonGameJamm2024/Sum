using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;

    // Start is called before the first frame update
    void Start()
    {
        nameText.transform.position = new Vector3(nameText.transform.position.x, nameText.transform.position.y, nameText.transform.position.z);
    }
}
