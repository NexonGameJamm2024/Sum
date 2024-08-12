using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Jusum_Stage5 : MonoBehaviour
{
    [SerializeField]
    private int ObstacleNum;
    [SerializeField]
    private GameObject [] whySumObstacle;
    public MakeBridge obj;
    private int count = 0;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            anim.SetTrigger("doSkill");

            if (collision.gameObject.name == "Mask")
            {
                whySumObstacle[0].transform.GetChild(0).gameObject.SetActive(true);
                whySumObstacle[0].GetComponent<WhySum_Obstacle>().SetActiveMask();
            }
            else if (collision.gameObject.name == "Dimsum_Open")
            {
                Debug.Log("Dead");
            }
            else
            {
                count++;

                if (count == ObstacleNum)
                {
                    obj.Vomit();
                }
            }
            
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Text"))
        {
            Debug.Log("Dead");
        }
    }

    private void Update()
    {
    }
    

}