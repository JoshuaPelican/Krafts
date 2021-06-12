using UnityEngine;

public class ToolManager : MonoBehaviour
{
    #region Simple Singleton
    public static ToolManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public Sprite[] glueBlobs;
    public GameObject gluePrefab;

    private Tool selectedTool = Tool.Manipulate;

    public enum Tool
    {
        None,
        Manipulate,
        Glue,
        Tape
    }

    public Tool SelectedTool
    {
        get { return selectedTool; }
        set
        {
            if(selectedTool != value)
            {
                selectedTool = value;
            }
        }
    }

    public void SelectTool(string toolName)
    {
        SelectedTool = (Tool)System.Enum.Parse(typeof(Tool), toolName);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch (selectedTool)
            {
                case Tool.Glue:
                    GameObject newGlue = Instantiate(gluePrefab, InputUtility.MousePosition, Quaternion.identity, GetClickedObject());
                    MakeRandomGlue(newGlue);
                    break;
            }
        }
    }

    public Transform GetClickedObject()
    {
        Vector2 mousePos = InputUtility.MousePosition;
        Collider2D hit = Physics2D.OverlapPoint(mousePos);

        if (hit)
            return hit.gameObject.transform;
        else
            return null;
    }

    public void MakeRandomGlue(GameObject newGlue)
    {
        int randIndex = Random.Range(0, glueBlobs.Length);
        SpriteRenderer glueRend = newGlue.GetComponent<SpriteRenderer>();
        glueRend.sprite = glueBlobs[randIndex];

        newGlue.AddComponent(typeof(BoxCollider2D));

        float randColor = Random.Range(.9f, 1f);
        glueRend.color = new Color(randColor, randColor, randColor, 1);

        newGlue.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
    }
}
