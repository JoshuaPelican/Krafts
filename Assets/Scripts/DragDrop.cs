using UnityEngine;

public class DragDrop : MonoBehaviour
{
    public static bool holding;
    private bool active;

    private void Start()
    {
        SetActive(true);
    }

    private void OnMouseDown()
    {
        if (ToolManager.instance.SelectedTool == ToolManager.Tool.Manipulate && !active && !holding)
        {
            Debug.Log("Pickup " + name);
            ToolManager.instance.NextZ();
            SetActive(true);
        }
    }

    private void Update()
    {
        if (active)
        {
            Vector3 offsetPos = new Vector3(InputUtility.MousePosition.x, InputUtility.MousePosition.y, ToolManager.instance.currentZ);
            transform.position = offsetPos;

            float rotationSpeed = InputUtility.HorizontalAxis(true) * 100 * Time.deltaTime;

            transform.Rotate(Vector3.forward, rotationSpeed);
        }
    }

    private void OnMouseUp()
    {
        if (ToolManager.instance.SelectedTool == ToolManager.Tool.Manipulate && active)
        {
            Debug.Log("Place Down " + name);
            SetActive(false);
        }
    }

    public void SetActive(bool value)
    {
        active = value;
        holding = value;
    }
}
