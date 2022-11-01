using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Part : MonoBehaviour
{
    public StatMod[] statMods = new StatMod[] { };

    public bool canBeColored;
    [HideInInspector] public bool glued;
    private Collider2D col;
    private ContactFilter2D filter;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        filter = new ContactFilter2D
        {
            layerMask = LayerMask.GetMask("Glue")
        };
        if (canBeColored)
        {
            GetComponent<SpriteRenderer>().color = ToolManager.instance.selectedColor;
        }
    }

    public bool CheckGlued()
    {
        List<Collider2D> attatched = new List<Collider2D>();
        Physics2D.OverlapCollider(col, filter, attatched);

        if (attatched.Count > 0 && attatched.Any(x => x.CompareTag("Glue")))
        {
            glued = true;

            transform.parent = attatched.First(x => x.CompareTag("Glue")).transform;
        }
        else
        {
            glued = false;
        }

        return glued;
    }
}