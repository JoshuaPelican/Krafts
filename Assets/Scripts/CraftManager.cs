using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftManager : MonoBehaviour
{
    public Transform[] children;
    public float shrinkSize;

    public void Finish(bool value)
    {
        if(value == true)
        {
            transform.localScale = Vector3.one * shrinkSize;
            transform.localPosition = Vector3.zero;
        }
        else
        {
            transform.localScale = Vector3.one;
            transform.localRotation = Quaternion.identity;
            transform.localPosition = new Vector3(0, -2.5f, -9.5f);
        }

        GetAllChildren();

        foreach (Transform child in children)
        {
            if(child.TryGetComponent(out Part childPart))
            {
                childPart.CheckGlued();

                if (!childPart.glued)
                {
                    child.gameObject.AddComponent<Rigidbody2D>().gravityScale = 3;
                }
            }
        }
    }

    private void GetAllChildren()
    {
        children = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
        }
    }
}