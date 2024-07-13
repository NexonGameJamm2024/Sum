using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JusumJusumSkill1 : MonoBehaviour
{
    private int TrashCount;
    private bool isTrigger;

    private int breakIndex;
    private int JusumCount;

    [SerializeField]
    private GameObject[] platform;

    [SerializeField]
    private GameObject board;

    // 현재 부딪힌 장애물 번호
    public int hitObstacleNumber;

    // 장애물 배열
    private bool[] obstaclesHit = new bool[101];

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {

            Destroy(collision.gameObject);

            int obstacleNumber = GetObstacleNumber(collision.gameObject);

            if (obstacleNumber > 0 && obstacleNumber <= 100)
            {
                hitObstacleNumber = obstacleNumber;
                obstaclesHit[hitObstacleNumber] = true;

                CheckSmallerObstacles(hitObstacleNumber);
            }

            if (PlayerMovement.isDrag == true)
            {
                PlayerMovement.isDrag = false;
                PlayerMovement.isGrounded = false;
            }
            TrashCount++;
        }
    }
    private int GetObstacleNumber(GameObject obstacle)
    {
        //===장애물의 이름에서 번호 추출===//
        string name = obstacle.name;
        if (int.TryParse(name.Split('_')[1], out int number))
        {
            return number;
        }
        return -1;
    }

    private void CheckSmallerObstacles(int currentObstacle)
    {
        for (int i = 1; i < currentObstacle; i++)
        {
            if (!obstaclesHit[i])
            {
                if (board != null)
                {
                    CancelInvoke("BreakPlatformCount");
                    Destroy(board.gameObject);
                }
            }
        }
    }
    private void Update()
    {
        TrashCountCheck();
    }

    private void TrashCountCheck()
    {
        if (!isTrigger && TrashCount >= 2)
        {
            TrashCount = 0;
            isTrigger = true;
            StartCoroutine(BreakPlatformCount(1.5f));
        }
    }

    private IEnumerator BreakPlatformCount(float delay)
    {
        yield return new WaitForSeconds(delay);
        BreakPlatform();
        breakIndex++;
    }

    private void BreakPlatform()
    {
        if (board == null) return;

        if (breakIndex < platform.Length)
        {
            board.transform.position += new Vector3(3.38f, 0f, 0f);
        }
        else if (breakIndex >= platform.Length)
        {
            CancelInvoke("BreakPlatformCount");
        }

        StartCoroutine(BreakPlatformCount(0.5f));
    }

}