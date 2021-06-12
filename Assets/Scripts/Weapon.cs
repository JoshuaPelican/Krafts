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
    public bool active = false;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (active)
        {
            transform.up = (Vector3)InputUtility.MousePosition() - transform.parent.position;
            GetClickInput();
        }
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

        Equip(true);

        ShrinkWeapon();
    }

    public void SetActivate(bool value)
    {
        active = value;
    }

    public void Equip(bool value)
    {
        if(value == true)
        {
            transform.SetParent(player);
            transform.localScale = Vector3.one * shrinkSize;
            transform.localPosition = Vector3.zero;
        }
        else
        {
            transform.localScale = Vector3.one;
            transform.localRotation = Quaternion.identity;
            transform.localPosition = new Vector3(0, -2.5f, -9.5f);
            transform.parent = null;
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
        foreach (Transform child in children)
        {
            if(child.TryGetComponent(out Part part))
            {
                part.Shoot();
            }
        }
    }
}