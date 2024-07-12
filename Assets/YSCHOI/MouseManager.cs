using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    bool isClicked = false;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

            if (hit.collider != null && hit.collider.gameObject.name == "Dimsum") // 캐릭터 클릭시
            {
                GameObject Obj = hit.transform.gameObject;
                Debug.Log(Obj.name);
                isClicked = true;
                //DragObj(Obj);
            }
        }
    }

    private void DragObj(GameObject obj) // 드래그
    {
        while(isClicked)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            obj.transform.position = pos;
        }
    }

    private void OnMouseDrag()
    {
        Debug.Log("Drag");
    }

}
