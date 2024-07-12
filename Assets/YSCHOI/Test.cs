using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float maxY;
    public float minY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDrag()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (maxY < pos.y)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, maxY);
        }
        else if(minY > pos.y)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, minY);
        }
        else
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, pos.y);
        }    
    }
}
