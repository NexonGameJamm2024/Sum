using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimsumSkill2 : MonoBehaviour
{
    private int cookedCount;

    private bool isLeftTouch;
    private bool isRightTouch;
    private bool isTransparent;

    private Color originalColor;

    public SpriteRenderer Dimsum;

    private void Start()
    {
        originalColor = Dimsum.color;
    }

    private void Update()
    {
        CookedCheck();
        CookedSkill();
    }


    private void CookedSkill()
    {
        if (!isTransparent && !isLeftTouch && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            cookedCount++;
            isRightTouch = false;
            isLeftTouch = true;
        }
        else if (!isTransparent && !isRightTouch && Input.GetKeyDown(KeyCode.RightArrow))
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
            StartCoroutine(ResetDimsumTransparency(3f));
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
