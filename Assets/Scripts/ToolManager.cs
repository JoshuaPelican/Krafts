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

    public Texture2D manipulateCursor;
    public Texture2D glueCursor;

    public Sprite[] glueBlobs;
    public GameObject gluePrefab;

    private Tool selectedTool = Tool.Manipulate;

    public float currentZ = 0;

    public void NextZ()
    {
        currentZ -= .01f;
    }

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

        ChangeCursor();
    }

    private void ChangeCursor()
    {
        switch (SelectedTool)
        {
            case Tool.Manipulate:
                Cursor.SetCursor(manipulateCursor, Vector2.one * 24, CursorMode.Auto);
                break;
            case Tool.Glue:
                Cursor.SetCursor(glueCursor, Vector2.zero, CursorMode.Auto);
                break;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch (selectedTool)
            {
                case Tool.Glue:
                    GameObject clickedObject = InputUtility.GetClickedObject();
                    NextZ();
                    if (InputUtility.ClickedObject)
                    {
                        GameObject newGlue = Instantiate(gluePrefab, new Vector3(InputUtility.MousePosition.x, InputUtility.MousePosition.y, currentZ), Quaternion.identity, clickedObject.transform);
                        MakeRandomGlue(newGlue);
                    }
                    break;
            }
        }
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
