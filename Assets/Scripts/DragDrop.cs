using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private Vector2 mousePos;
    public static bool holding;
    private bool active;

    private void Start()
    {
        SetActive(true);
    }

    private void OnMouseDown()
    {
        if(ToolManager.instance.SelectedTool == ToolManager.Tool.Manipulate && !active && !holding)
        {
            SetActive(true);
        }
        else if(ToolManager.instance.SelectedTool == ToolManager.Tool.Manipulate && active && holding)
        {
            SetActive(false);
        }
    }
    private void Update()
    {
        if (active)
        {
            mousePos = InputUtility.MousePosition;
            transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);

            Vector3 rotation = transform.rotation.eulerAngles;
            rotation.z -= InputUtility.HorizontalAxis(true) * 100 * Time.deltaTime;

            transform.rotation = Quaternion.Euler(rotation);
        }
    }

    public void SetActive(bool value)
    {
        active = value;
        holding = value;
    }
}
