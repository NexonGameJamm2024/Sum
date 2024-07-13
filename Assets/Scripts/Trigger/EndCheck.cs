using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCheck : MonoBehaviour
{
    [SerializeField]
    GameObject GameManager;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.instance.EffectSoundPlay((int)SoundManager.EffectType.PlopSound);
            GameObject temp = GameObject.FindGameObjectWithTag("Canvas");
            temp.GetComponent<FadeController>().FadeOut();
            StartCoroutine(nameof(ChangeScene));
        }
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1.6f);
        GameManager.GetComponent<GameManager>().NextScene();
    }
}
