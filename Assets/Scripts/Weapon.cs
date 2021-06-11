using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Transform[] children;

    private List<Stat> stats = new List<Stat>
    {
        new Stat(Stat.StatType.Damage),
        new Stat(Stat.StatType.AttackRate, 3),
        new Stat(Stat.StatType.CritChance),
        new Stat(Stat.StatType.Mobility),
        new Stat(Stat.StatType.SwingArc, 120),
    };

    public float shrinkSize;

    private void Update()
    {
        GetClickInput();
    }

    private void GetAllParts()
    {
        children = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
        }
    }

    public void CombineWeapon()
    {
        GetAllParts();

        foreach (Transform child in children)
        {
            if (child.TryGetComponent(out Part childPart))
            {
                //Get All Things From Part
                ApplyStatMods(childPart.statMods);
            }

            if (child.TryGetComponent(out DragDrop childDragDrop))
            {
                childDragDrop.SetActive(false);
            }
        }

        foreach (Stat stat in stats)
        {
            Debug.Log(stat.type.ToString() + " is " + stat.value);
        }

        ShrinkWeapon();
    }

    private void ShrinkWeapon()
    {
        transform.localScale = Vector3.one * shrinkSize;
    }

    public void ApplyStatMods(StatMod[] statMods)
    {
        foreach (StatMod statMod in statMods)
        {
            Debug.Log(statMod.statType.ToString() + " has been modified by " + statMod.value);
            Stat statToMod = GetStatOfType(statMod.statType);
            statToMod.value += statMod.value;
        }
    }

    private Stat GetStatOfType(Stat.StatType statType)
    {
        return stats.Find(x => x.type == statType);
    }

    private void GetClickInput()
    {
        bool clicked = Input.GetAxisRaw("Fire1") == 1;

        if (clicked)
        {
            //Use Weapon
            UseWeapon();
        }
    }

    public enum UseType
    {
        None,
        Swing,
        Shoot,
    }

    private void UseWeapon()
    {
        bool swing = false;

        foreach (Transform child in children)
        {
            if(child.TryGetComponent(out Part part))
            {
                part.Shoot();

                if (part.useType == UseType.Swing)
                {
                    swing = true;
                }
            }
        }

        if (swing)
        {
            StartCoroutine("SwingSword");
        }
    }

    private IEnumerator SwingSword()
    {
        float timePerDegree = GetStatOfType(Stat.StatType.SwingArc).value / GetStatOfType(Stat.StatType.AttackRate).value;

        for (int i = 0; i < GetStatOfType(Stat.StatType.SwingArc).value; i++)
        {
            Debug.Log("Swinging Sword");
            transform.Rotate(Vector3.forward, 1);
            yield return new WaitForSeconds(timePerDegree);
        }
    }
}