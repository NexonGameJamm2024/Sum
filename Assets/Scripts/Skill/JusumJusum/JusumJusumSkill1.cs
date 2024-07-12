using UnityEngine;

public class JusumJusumSkill1 : MonoBehaviour
{
    private int dragCount = 0;
    private Vector3 offset;
    private bool isDragging = false;
    private bool wasDraggedUp = false; // 이전 드래그가 위로 였는지 여부

    public GameObject trashPrefab;
    public GameObject jewelPrefab;

    private void Update()
    {
        CheckDragCount();
    }

    private void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, offset.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;

            float deltaY = Input.GetAxis("Mouse Y");

            if (deltaY > 0 && !wasDraggedUp) // 위로 드래그
            {
                dragCount++;
                wasDraggedUp = true;
            }
            else if (deltaY < 0 && wasDraggedUp) // 아래로 드래그
            {
                dragCount++;
                wasDraggedUp = false;
            }
        }
    }

    private void CheckDragCount()
    {
        if (dragCount >= 6)
        {
            dragCount = 0;
            int randomIndex = Random.Range(0, 100);

            if (randomIndex < 95)
            {
                Vector3 spawnPosition = transform.position + new Vector3(2f, 0f, 0f);
                GameObject trash = Instantiate(trashPrefab, spawnPosition, Quaternion.identity);
                Destroy(trash, 3f);
            }
            else if (randomIndex >= 95)
            {
                Vector3 spawnPosition = transform.position + new Vector3(2f, 0f, 0f);
                Instantiate(jewelPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}
