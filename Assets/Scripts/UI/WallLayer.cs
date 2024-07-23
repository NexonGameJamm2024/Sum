using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallLayer : MonoBehaviour
{
    public int layerIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.SetSiblingIndex(layerIndex);
    }
}
