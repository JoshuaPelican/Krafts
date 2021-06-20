﻿using System.Linq;
using UnityEngine;

public static class InputUtility
{
    public static Vector2 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);

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

    public static bool ClickedObject
    {
        get { return Physics2D.OverlapPoint(MousePosition, LayerMask.GetMask("Part")); }
    }

    public static GameObject GetClickedObject()
    {
        Collider2D hit = Physics2D.OverlapPoint(MousePosition, LayerMask.GetMask("Part"));

        if (ClickedObject)
        {
            //Debug.Log("Clicked " + hit.name);
            return hit.gameObject;
        }
        else
        {
            //Debug.Log("Clicked Nothing");
            return null;
        }
    }

    public static bool ClickContainsObject(GameObject go)
    {
        Collider2D[] hit = Physics2D.OverlapPointAll(MousePosition, LayerMask.GetMask("Part"));

        foreach (Collider2D col in hit)
        {
            if(col.gameObject == go)
            {
                return true;
            }
        }

        return false;
    }
}
