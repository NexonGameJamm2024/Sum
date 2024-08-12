using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cysharp.Threading.Tasks;
using UnityEditor;

public class JuSum_Stage3 : MonoBehaviour
{
    [SerializeField] private Transform tp;
    [TextArea(3, 10)]
    public string annotation = "";
    [SerializeField] private float[] jellySize;
    [SerializeField] private float[] jumpForce;
    
    public bool isTrigger = false;
    private PlayerMovement _pm;
    private Animator _anim;
    public Animation anim_test;

    private bool isActive = false;

    private void Start()
    {
        TryGetComponent(out _pm);
        TryGetComponent(out _anim);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jelly") && !isActive)
        {
            string type = other.GetComponent<Jelly>().type;
            ChangeSizeWait(type).Forget();
            Destroy(other.gameObject);
        }
    }
    
    async UniTask ChangeSizeWait(string type)
    {
        Time.timeScale = 0.5f;
        isActive = true;
        //_anim.Play("GreenJelly", -1, 0f); 
        
        // create a curve to move the GameObject and assign to the clip
        AnimationClip clip = new AnimationClip();
        clip.legacy = true;
        
        Keyframe[] keys_x;
        Keyframe[] keys_y;
        keys_x = new Keyframe[2];
        keys_y = new Keyframe[2];
        keys_x[0] = new Keyframe(0f, tp.localScale.x);
        keys_y[0] = new Keyframe(0f, tp.localScale.y);
        
        if (type == "Yellow")
        {
            keys_x[1] = new Keyframe(1f, jellySize[0]);
            keys_y[1] = new Keyframe(1f, jellySize[0]);
            _pm.ChangeJumpForce(jumpForce[0]);
        }
        else if (type == "Green")
        {
            keys_x[1] = new Keyframe(1f, jellySize[1]);
            keys_y[1] = new Keyframe(1f, jellySize[1]);
            _pm.ChangeJumpForce(jumpForce[1]);
        }
        else if (type == "Red")
        {
            keys_x[1] = new Keyframe(1f, jellySize[2]);
            keys_y[1] = new Keyframe(1f, jellySize[2]);
            _pm.ChangeJumpForce(jumpForce[2]);
        }
        else if (type == "Blue")
        {
            keys_x[1] = new Keyframe(1f, jellySize[3]);
            keys_y[1] = new Keyframe(1f, jellySize[3]);
            _pm.ChangeJumpForce(jumpForce[3]);
        }
        //keys[2] = new Keyframe(16.0f, 0f);

        var curve_x = new AnimationCurve(keys_x);
        var curve_y = new AnimationCurve(keys_y);
        
        clip.SetCurve("", typeof(Transform), "localScale.x", curve_x);
        clip.SetCurve("", typeof(Transform), "localScale.y", curve_y);
        
        anim_test.AddClip(clip, "test");
        anim_test.Play("test");
        
        await UniTask.Delay(TimeSpan.FromSeconds(1f), ignoreTimeScale: true);
        
        if (isTrigger && type != "Blue")
        {
            Debug.Log("Dead");
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        
        isActive = false;
    }
}
