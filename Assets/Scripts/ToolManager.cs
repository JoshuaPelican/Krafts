using UnityEngine;
using UnityEngine.UI;

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

    public Color drawColor;

    public float currentZ = 0;
    [HideInInspector] public Color selectedColor = Color.white;
    private int selectedColorIndex = -1;
    public Image colorTool;
    public Transform partGiverContainer;

    public AudioClip glueClip;
    public AudioClip pickupBottle;
    public AudioClip placeBottle;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        ChangeColorTool();
    }

    public void NextZ()
    {
        currentZ -= .01f;
    }

    public enum Tool
    {
        None,
        Manipulate,
        Delete,
        Glue,
        Paint,
        Draw,
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

    public void ChangeColorTool()
    {
        selectedColorIndex++;

        if(selectedColorIndex >= 8)
        {
            selectedColorIndex = 0;
        }

        switch (selectedColorIndex)
        {
            case 0:
                selectedColor = Color.white / 1.05f; break;
            case 1:
                selectedColor = Color.red / 1.25f; break;
            case 2:
                selectedColor = new Color(1, 0.5f, 0) / 1.15f; break;
            case 3:
                selectedColor = Color.yellow / 1.05f; break;
            case 4:
                selectedColor = new Color(0, 0.8f, 0.1f) / 1.15f; break;
            case 5:
                selectedColor = new Color(0, 0.3f, 1) / 1.15f; break;
            case 6:
                selectedColor = new Color(0.5f, 0, 1) / 1.15f; break;
            case 7:
                selectedColor = new Color(1, 0, .75f) / 1.15f; break;
        }

        selectedColor.a = 1;

        colorTool.color = selectedColor;

        foreach (UIPartGiver partGiver in partGiverContainer.GetComponentsInChildren<UIPartGiver>())
        {
            if (partGiver.partToGive.GetComponent<Part>().canBeColored)
            {
                partGiver.GetComponent<Image>().color = selectedColor;
            }
        }
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
            GameObject clickedObject = InputUtility.GetClickedObject();

            switch (selectedTool)
            {
                case Tool.Glue:
                    if (InputUtility.ClickedObject)
                    {
                        GameObject newGlue = Instantiate(gluePrefab, new Vector3(InputUtility.MousePosition.x, InputUtility.MousePosition.y, currentZ - .01f), Quaternion.identity, clickedObject.transform);
                        MakeRandomGlue(newGlue);
                    }
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeColorTool();
        }
    }

    public void MakeRandomGlue(GameObject newGlue)
    {
        int randIndex = Random.Range(0, glueBlobs.Length);
        SpriteRenderer glueRend = newGlue.GetComponent<SpriteRenderer>();
        glueRend.sprite = glueBlobs[randIndex];

        newGlue.AddComponent(typeof(BoxCollider2D));

        float randColor = Random.Range(.96f, 1f);
        glueRend.color = new Color(randColor, randColor, randColor, 1);
        newGlue.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));

        float magnitude = newGlue.transform.parent.localScale.magnitude;

        newGlue.transform.localScale = new Vector3(newGlue.transform.localScale.x / magnitude, newGlue.transform.localScale.y / magnitude, 1);

        source.pitch = Random.Range(0.9f, 1.1f);
        source.PlayOneShot(glueClip, Random.Range(.2f, .3f));
    }
}
