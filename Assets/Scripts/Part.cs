using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Part : MonoBehaviour
{
    public StatMod[] statMods = new StatMod[] { };

    public bool glued;
    private Collider2D col;
    private ContactFilter2D filter;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        filter = new ContactFilter2D
        {
            layerMask = LayerMask.GetMask("Glue")
        };
    }

    public void CheckGlued()
    {
        List<Collider2D> attatched = new List<Collider2D>();
        Physics2D.OverlapCollider(col, filter, attatched);

        if (attatched.Count > 1)
        {
            glued = true;
        }
    }
}
