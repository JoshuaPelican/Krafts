using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craft : MonoBehaviour
{
    private Transform[] children;

    private List<Stat> stats = new List<Stat>
    {
        new Stat(Stat.StatType.Color),
        new Stat(Stat.StatType.Creativity),
        new Stat(Stat.StatType.Uniqueness),
    };

    public float shrinkSize;

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

        Equip(true);

        ShrinkWeapon();
    }

    public void Equip(bool value)
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
    }

    private void ShrinkWeapon()
    {
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

    public Stat GetStatOfType(Stat.StatType statType)
    {
        return stats.Find(x => x.type == statType);
    }
}