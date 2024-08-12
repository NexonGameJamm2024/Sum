using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhySumActiveTrigger : MonoBehaviour
{
    public WhySum_Obstacle obj;
    public GameObject[] mask;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            obj.SetActive();
            mask[0].SetActive(true);
        }
    }
}
