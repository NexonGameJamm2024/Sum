using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SunGlass : MonoBehaviour
{
    public GameObject player;
    private Transform player_tf;
    public bool isAir;
    private Vector3 lastPos;
    private float Timer;
    private bool isSkill;
    private float Dist;

    [SerializeField] TMP_Text text1;
    [SerializeField] TMP_Text bridge;

    // Start is called before the first frame update
    void Start()
    {
        player_tf = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        State();
        CheckTimer();
    }

    void State()
    {
        if (!isAir && !isSkill)
        {
            gameObject.transform.position = new Vector3(player_tf.position.x, player_tf.position.y + 0.25f, player_tf.position.z);
        }
        else
        {
            text1.enabled = false;
            CheckDist();
            if (lastPos != transform.localPosition)
            {
                lastPos = transform.localPosition;
            }
            else
            {
                Timer = Timer + Time.deltaTime;
                //Debug.Log(Timer);
            }
        }
    }

    void CheckTimer()
    {
        if (Timer > 5)
        {
            Timer = 0;
            Debug.Log("Skill");
            isSkill = true;
            MakeBridge();
        }
    }

    void MakeBridge()
    {
        isSkill = false;
        float angle = Mathf.Atan2(transform.position.y - player_tf.position.y, transform.position.x - player_tf.position.x) * Mathf.Rad2Deg / 2;


        Instantiate(bridge, new Vector2((transform.position.y + player_tf.position.y) / 2, (transform.position.x + player_tf.position.x) / 2), Quaternion.Euler(0, 0, angle));
    }

    void CheckDist()
    {
        Dist = Vector3.Distance(gameObject.transform.position, player_tf.position);

        if (Dist > 8)
        {
            isAir = false;
            isSkill = false;
            StartCoroutine(nameof(ResetFirst));
        }
    }

    IEnumerator ResetFirst()
    {
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<WhyDownSum_Skill1>().isFirst = false;
    }

}
