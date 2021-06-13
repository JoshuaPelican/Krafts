using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

public class ScreenshotManager : MonoBehaviour
{
    private int fileCounter;
    public new Camera camera;

    [DllImport("__Internal")]
    private static extern void ImageDownloader(string str, string fn);

    public void Capture()
    {
        RenderTexture activeRenderTexture = RenderTexture.active;
        RenderTexture.active = camera.targetTexture;

        camera.Render();

        Texture2D image = new Texture2D(camera.targetTexture.width, camera.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
        image.Apply();
        RenderTexture.active = activeRenderTexture;

        byte[] bytes = image.EncodeToPNG();
        Destroy(image);

        ImageDownloader(System.Convert.ToBase64String(bytes), "Krafts_Screenshot_" + fileCounter + ".jpg");
        fileCounter++;
    }
}