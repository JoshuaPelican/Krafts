using UnityEngine;

public class Drawable : MonoBehaviour
{
    public Shader drawShader;

    private SpriteRenderer rend;

    private RenderTexture drawMap;
    private Material drawableMat;
    private Material drawMat;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();

        drawMat = new Material(drawShader);
        Color drawCol = ToolManager.instance.drawColor;
        drawMat.SetVector("_DrawColor", drawCol);

        drawableMat = rend.material;

        Vector2 spriteSize = rend.sprite.rect.size;

        drawMap = new RenderTexture((int)spriteSize.x, (int)spriteSize.y, 0, RenderTextureFormat.ARGBFloat)
        {
            filterMode = FilterMode.Point,
            antiAliasing = 1
        };
        drawableMat.SetTexture("_DrawMap", drawMap);

    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0) && ToolManager.instance.SelectedTool == ToolManager.Tool.Draw)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            mousePos += ((Vector2)rend.sprite.bounds.extents * transform.localScale) - (Vector2)transform.position;
            mousePos *= rend.sprite.pixelsPerUnit;
            mousePos.x /= rend.sprite.texture.width;
            mousePos.y /= rend.sprite.texture.height;
            mousePos /= transform.localScale;

            drawMat.SetVector("_Coordinate", new Vector4(mousePos.x, mousePos.y, 0, 0));

            RenderTexture temp = RenderTexture.GetTemporary(drawMap.width, drawMap.height, 0, RenderTextureFormat.ARGBFloat);
            Graphics.Blit(drawMap, temp);
            Graphics.Blit(temp, drawMap, drawMat);
            RenderTexture.ReleaseTemporary(temp);
        }
    }
}
