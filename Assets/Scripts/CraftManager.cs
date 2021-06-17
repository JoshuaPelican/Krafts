using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftManager : MonoBehaviour
{
    public Transform[] children;
    public float shrinkSize;

    public float endDelay = 1;

    private AudioSource source;

    public GameObject finishedPanel;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Finish()
    {
        StartCoroutine("StartFinish");
    }

    private IEnumerator StartFinish()
    {
        source.Play();

        transform.localScale = Vector3.one * shrinkSize;

        yield return new WaitForSeconds(endDelay);

        GetAllChildren();

        finishedPanel.SetActive(true);
    }

    public void ShakeOff()
    {
        foreach (Transform child in children)
        {
            if (child.TryGetComponent(out Part childPart))
            {
                childPart.CheckGlued();

                if (!childPart.glued)
                {
                    if (TryGetComponent(out Collider2D col))
                    {
                        col.enabled = false;
                    }
                    Rigidbody2D rig = child.gameObject.AddComponent<Rigidbody2D>();
                    rig.gravityScale = 2;
                    rig.velocity = new Vector2(Random.Range(-7, 7f), Random.Range(5f, 15f));
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