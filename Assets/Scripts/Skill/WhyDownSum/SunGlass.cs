using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
    public Transform[] CreatePosisiton;

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
                Vector2 v = gameObject.transform.position - player_tf.position;
                float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;    
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
        if (Timer > 3.5f)
        {
            Timer = 0;
            isSkill = true;
            MakeBridge();
        }
    }

    void MakeBridge()
    {
        isSkill = false;
        Vector2 v = gameObject.transform.position - player_tf.position;
        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        
        if (112.5f > angle && angle >= -22.5f)
        {
            Instantiate(bridge, CreatePosisiton[0].position, Quaternion.Euler(0, 0, angle));
        }
        else if (-22.5f > angle && angle >= -67.5f)
        {
            Instantiate(bridge, CreatePosisiton[1].position, Quaternion.Euler(0, 0, angle));
        }
        else if (-67.5f > angle && angle >= -112.5f)
        {
            Instantiate(bridge, CreatePosisiton[2].position, Quaternion.Euler(0, 0, angle));
        }
        else if (-112.5f > angle && angle >= -157.5)
        {
            Instantiate(bridge, CreatePosisiton[3].position, Quaternion.Euler(0, 0, angle));
        }
        else if (180f >= angle && angle > 157.5f)
        {
            Instantiate(bridge, CreatePosisiton[4].position, Quaternion.Euler(0, 0, angle));
        }
        else if (-180f <= angle && angle >= 112.5f)
        {
            Instantiate(bridge, CreatePosisiton[4].position, Quaternion.Euler(0, 0, angle));
        }

        //Instantiate(bridge, CreatePosisiton.position, Quaternion.Euler(0, 0, angle));
    }

    void CheckDist()
    {
        Dist = Vector3.Distance(gameObject.transform.position, player_tf.position);

        if (Dist > 10)
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
