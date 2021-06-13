using UnityEngine;
using UnityEngine.UI;

public class UIPartGiver : MonoBehaviour
{
    public GameObject partToGive;
    private Transform weaponTransform;

    private void Start()
    {
        weaponTransform = GameObject.FindWithTag("Craft").transform;

        Image image = GetComponent<Image>();

        image.sprite = partToGive.GetComponent<SpriteRenderer>().sprite;
        GetComponent<RectTransform>().sizeDelta = image.sprite.bounds.size * 100;
    }

    public void GivePart()
    {
        if(ToolManager.instance.SelectedTool == ToolManager.Tool.Manipulate && !DragDrop.holding)
        {
            ToolManager.instance.NextZ();
            Instantiate(partToGive, new Vector3(transform.position.x, transform.position.y, ToolManager.instance.currentZ), Quaternion.identity, weaponTransform);
        }
    }
}
