using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeValue : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Dimsum_Skill1>().maxY += 5f;
            Destroy(gameObject);
        }
    }
}
