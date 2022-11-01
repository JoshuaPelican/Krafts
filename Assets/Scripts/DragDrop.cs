using UnityEngine;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour
{
    public static bool holding;
    private bool active;
    public Vector2 placeableArea;

    private SpriteRenderer rend;
    private Transform craftBase;
    private Image blockingImage;

    public AudioClip placeClip;
    public AudioClip squishClip;
    private AudioSource source;

    private void Start()
    {
        craftBase = GameObject.FindWithTag("Craft").transform;
        blockingImage = GameObject.FindWithTag("Blocking").GetComponent<Image>();

        SetActive(true);

        rend = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(blockingImage.raycastTarget != holding)
            blockingImage.raycastTarget = holding;

        if (Input.GetMouseButtonDown(0) && InputUtility.GetClickedObject() == gameObject)
        {
            if (ToolManager.instance.SelectedTool == ToolManager.Tool.Manipulate && !active && !holding)
            {
                //Debug.Log("Pickup " + name);
                ToolManager.instance.NextZ();
                SetActive(true);
            }
        }

        if (active)
        {
            Vector3 offsetPos = new Vector3(InputUtility.MousePosition.x, InputUtility.MousePosition.y, ToolManager.instance.currentZ);
            transform.position = offsetPos;

            float rotationSpeed = InputUtility.HorizontalAxis(true) * 100 * Time.deltaTime;
            float scaleSpeed = InputUtility.VerticalAxis(true) * 5 * Time.deltaTime;

            transform.Rotate(Vector3.forward, rotationSpeed);

            transform.localScale = new Vector3(Mathf.Clamp(transform.localScale.x + scaleSpeed, .5f, 4f), Mathf.Clamp(transform.localScale.y + scaleSpeed, .5f, 4f), 1);

            if(TryGetComponent(out Part part))
            {
                if (part.canBeColored && Input.GetKeyDown(KeyCode.C))
                {
                    rend.color = ToolManager.instance.selectedColor;
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && InputUtility.ClickContainsObject(gameObject))
        {
            if (ToolManager.instance.readyTrashTool)
            {
                SetActive(false);
                Destroy(gameObject);
                ToolManager.instance.readyTrashTool = false;
            }

            else if (ToolManager.instance.SelectedTool == ToolManager.Tool.Manipulate && active)
            {
                Vector2 mousePos = InputUtility.MousePosition * 2;

                if(mousePos.x < placeableArea.x && mousePos.x > -placeableArea.x && mousePos.y < placeableArea.y && mousePos.y > -placeableArea.y)
                {
                    source.pitch = Random.Range(0.9f, 1.1f);
                    if (GetComponent<Part>().CheckGlued())
                    {
                        //Play Sound
                        source.PlayOneShot(squishClip, Random.Range(1.1f, 1.4f));
                    }
                    else
                    {
                        transform.parent = craftBase;
                        source.PlayOneShot(placeClip, Random.Range(.6f, .75f));
                    }
                    //Debug.Log("Place Down " + name);
                    SetActive(false);
                }
            }
        }
    }

    public void SetActive(bool value)
    {
        active = value;
        holding = value;
    }
}
