using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;
    private bool isDragging = false;

    void OnMouseDown()
    {
        if (!isDragging)
        {
            zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            offset = gameObject.transform.position - GetMouseWorldPos();
            isDragging = true;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPos() + offset;
        }
    }

    void Update()
    {
        // Handle touch input for mobile devices
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (!isDragging)
                {
                    zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
                    offset = gameObject.transform.position - GetTouchWorldPos(touch.position);
                    isDragging = true;
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if (isDragging)
                {
                    transform.position = GetTouchWorldPos(touch.position) + offset;
                }
            }
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private Vector3 GetTouchWorldPos(Vector2 touchPosition)
    {
        Vector3 touchPoint = touchPosition;
        touchPoint.z = zCoord;
        return Camera.main.ScreenToWorldPoint(touchPoint);
    }
}
