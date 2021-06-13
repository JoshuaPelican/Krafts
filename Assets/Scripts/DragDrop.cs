using UnityEngine;

public class DragDrop : MonoBehaviour
{
    public static bool holding;
    private bool active;

    private SpriteRenderer rend;


    public AudioClip placeClip;
    public AudioClip squishClip;
    private AudioSource source;

    private void Start()
    {
        SetActive(true);
        rend = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        if (ToolManager.instance.SelectedTool == ToolManager.Tool.Manipulate && !active && !holding)
        {
            //Debug.Log("Pickup " + name);
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
            float scaleSpeed = InputUtility.VerticalAxis(true) * 30 * Time.deltaTime;

            transform.Rotate(Vector3.forward, rotationSpeed);
            transform.localScale += scaleSpeed * Vector3.one;

            rend.color = ToolManager.instance.selectedColor;
        }
    }

    private void OnMouseUp()
    {
        if (ToolManager.instance.SelectedTool == ToolManager.Tool.Manipulate && active && InputUtility.MousePosition.y > -3.25f)
        {
            source.pitch = Random.Range(0.9f, 1.1f);
            if (GetComponent<Part>().CheckGlued())
            {
                //Play Sound
                source.PlayOneShot(squishClip, Random.Range(1.1f, 1.4f));
            }
            else
            {
                source.PlayOneShot(placeClip, Random.Range(.6f, .75f));
            }
            //Debug.Log("Place Down " + name);
            SetActive(false);
        }
    }

    public void SetActive(bool value)
    {
        active = value;
        holding = value;
    }
}
