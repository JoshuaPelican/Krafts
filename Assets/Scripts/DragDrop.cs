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
            mousePos = InputUtility.MousePosition();
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);

            Vector3 rotation = transform.rotation.eulerAngles;
            rotation.z -= InputUtility.HorizontalAxis(true) * 100 * Time.deltaTime;

            transform.rotation = Quaternion.Euler(rotation);
        }
    }

    public void SetActive(bool value)
    {
        active = value;
    }
}
