using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private Vector2 mousePos;
    private bool active;

    private void Start()
    {
        SetActive(true);
    }

    private void OnMouseDrag()
    {
        if (active)
        {
            mousePos = GetMousePosition();
            transform.position = mousePos;
        }
    }

    public Vector2 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void SetActive(bool value)
    {
        active = value;
    }
}
