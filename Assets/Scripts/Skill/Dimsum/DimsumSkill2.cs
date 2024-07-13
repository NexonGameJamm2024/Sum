using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimsumSkill2 : MonoBehaviour
{
    private int cookedCount;

    private bool isLeftTouch;
    private bool isRightTouch;
    private bool isTransparent;
    private Animator anim;
    private Color originalColor;
    
    public SpriteRenderer Dimsum;
    [SerializeField]
    GameObject Effect;
    [SerializeField]
    GameObject Fire;


    private void Start()
    {
        originalColor = Dimsum.color;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        CookedCheck();
        CookedSkill();
    }


    private void CookedSkill()
    {
        if (!isTransparent && !isLeftTouch && Input.GetKeyDown(KeyCode.A))
        {
            cookedCount++;
            isRightTouch = false;
            isLeftTouch = true;
        }
        else if (!isTransparent && !isRightTouch && Input.GetKeyDown(KeyCode.D))
        {
            cookedCount++;
            isLeftTouch = false;
            isRightTouch = true;
        }
    }

    private void CookedCheck()
    {
        if (cookedCount >= 20)
        {
            cookedCount = 0;
            SetDimsumTransparency(0.5f);
            ActivateObstacleTriggers();
            isTransparent = true;

            Effect.transform.GetChild(1).gameObject.SetActive(false);
            Effect.SetActive(false);
            Fire.SetActive(true);

            anim.SetBool("Skill2", true);
            anim.SetTrigger("doSkill2");
            StartCoroutine(ResetDimsumTransparency(3f));
        }
        else if(cookedCount == 15)
        {
            Effect.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (cookedCount == 10)
        {
            Effect.SetActive(true);
        }
    }


    private void SetDimsumTransparency(float alpha)
    {
        Color dimsumColor = Dimsum.color;
        dimsumColor.a = alpha;
        Dimsum.color = dimsumColor;
    }


    private IEnumerator ResetDimsumTransparency(float delay)
    {
        yield return new WaitForSeconds(delay);
        Fire.SetActive(false);
        anim.SetBool("Skill2", false);
        Dimsum.color = originalColor;
        isTransparent = false;
        isRightTouch = false;
        isLeftTouch = false;
        DiableObstacleTriggers();
    }

    private void ActivateObstacleTriggers()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (GameObject obstacle in obstacles)
        {
            Collider2D collider = obstacle.GetComponent<Collider2D>();

            if (collider != null)
            {
                collider.isTrigger = true;
            }
        }
    }

    private void DiableObstacleTriggers()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (GameObject obstacle in obstacles)
        {
            Collider2D collider = obstacle.GetComponent<Collider2D>();

            if (collider != null)
            {
                collider.isTrigger = false;
            }
        }
    }
}
