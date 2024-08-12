using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Out_Trigger : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<JuSum_Stage3>().isTrigger = false;
        }
    }
}
