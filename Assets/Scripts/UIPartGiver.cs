using UnityEngine;
using UnityEngine.UI;

public class UIPartGiver : MonoBehaviour
{
    public GameObject partToGive;
    private Transform weaponTransform;

    private void Start()
    {
        weaponTransform = GameObject.FindWithTag("Craft").transform;

        GetComponent<Image>().sprite = partToGive.GetComponent<SpriteRenderer>().sprite;
    }

    public void GivePart()
    {
        if(ToolManager.instance.SelectedTool == ToolManager.Tool.Manipulate)
        {
            ToolManager.instance.NextZ();
            Instantiate(partToGive, new Vector3(transform.position.x, transform.position.y, ToolManager.instance.currentZ), Quaternion.identity, weaponTransform);
        }
    }
}
