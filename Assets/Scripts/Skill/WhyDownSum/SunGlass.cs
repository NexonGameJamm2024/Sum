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
    public bool isSkill;
    private float Dist;
    public Animator bodyAnim;

    [SerializeField] TMP_Text text1;
    [SerializeField] TMP_Text bridge;
    public Transform CreatePosisiton;

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
            gameObject.transform.position = new Vector3(player_tf.position.x, player_tf.position.y + 0.1f, player_tf.position.z);
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
        if (Timer > 4f)
        {
            Timer = 0f;
            MakeBridge();
        }
    }

    void MakeBridge()
    {
        SoundManager.instance.EffectSoundPlay((int)SoundManager.EffectType.Talk);
        SoundManager.instance.RapCount++;
        bodyAnim.SetTrigger("doSkill");
        Vector3 v = gameObject.transform.position - player_tf.position;
        float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        Vector3 vDir = v.normalized;
        
        TMP_Text temp = Instantiate(bridge, CreatePosisiton.position + vDir * 1.5f, Quaternion.Euler(0, 0, angle));
    }

    void CheckDist()
    {
        Dist = Vector3.Distance(gameObject.transform.position, player_tf.position);

        if (Dist > 10)
        {
            isAir = false;
            transform.rotation = player.transform.GetChild(3).rotation;
            isSkill = false;
            StartCoroutine(nameof(ResetFirst));
            bodyAnim.SetBool("isIdle", true);
            SoundManager.instance.EffectSoundPlay((int)SoundManager.EffectType.WhyReget);
        }
    }

    IEnumerator ResetFirst()
    {
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<WhyDownSum_Skill1>().isFirst = false;
    }

}
