using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotTrigger : MonoBehaviour
{
    public GameObject potUI;
    public GameObject[] potCheckUI;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(other.tag);
            Debug.Log(other.GetComponent<Dimsum_Fire>().isFire);
            if (other.GetComponent<Dimsum_Fire>().isFire)
            {
                potUI.SetActive(true);
            }
        }
        else if (other.CompareTag("Vegatable"))
        {
            potCheckUI[other.GetComponent<Vegatable>().type - 1].SetActive(true);
            Destroy(other.gameObject);
        }
    }
}
