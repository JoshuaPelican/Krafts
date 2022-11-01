using UnityEngine;

public class TrashCan : MonoBehaviour
{
    private void OnMouseOver()
    {
        ToolManager.instance.readyTrashTool = true;
    }

    private void OnMouseExit()
    {
        ToolManager.instance.readyTrashTool = false;
    }
}
