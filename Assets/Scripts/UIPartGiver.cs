using UnityEngine;
using UnityEngine.UI;

public class UIPartGiver : MonoBehaviour
{
    public GameObject partToGive;
    private Transform craft;
    private GameObject helpPanel;

    private void Start()
    {
        craft = GameObject.FindWithTag("Craft").transform;
        helpPanel = GameObject.FindWithTag("Help");

        Image image = GetComponent<Image>();

        image.sprite = partToGive.GetComponent<SpriteRenderer>().sprite;
        GetComponent<RectTransform>().sizeDelta = image.sprite.bounds.size * 100;

        gameObject.AddComponent<BoxCollider2D>().size = GetComponent<RectTransform>().sizeDelta;

    }

    private void OnMouseDown()
    {
        if (!helpPanel.activeSelf)
        {
            GivePart();
        }
    }

    public void GivePart()
    {
        if(ToolManager.instance.SelectedTool == ToolManager.Tool.Manipulate && !DragDrop.holding)
        {
            ToolManager.instance.NextZ();
            Instantiate(partToGive, new Vector3(transform.position.x, transform.position.y, ToolManager.instance.currentZ), Quaternion.identity, craft);
        }
    }
}
