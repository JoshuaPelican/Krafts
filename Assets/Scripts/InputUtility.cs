using UnityEngine;

public static class InputUtility
{
    public static Vector2 MousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public static float HorizontalAxis(bool isRaw = false)
    {
        if (isRaw)
            return Input.GetAxisRaw("Horizontal");
        else
            return Input.GetAxis("Horizontal");

    }

    public static float VerticalAxis(bool isRaw = false)
    {
        if (isRaw)
            return Input.GetAxisRaw("Vertical");
        else
            return Input.GetAxis("Vertical");
    }
}
