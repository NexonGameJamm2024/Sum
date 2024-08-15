using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Collider2D col;
        TryGetComponent(out col);

        //col.offset = new Vector2(0f, 0f);
    }
}
