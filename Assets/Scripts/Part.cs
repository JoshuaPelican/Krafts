using UnityEngine;

public class Part : MonoBehaviour
{
    public StatMod[] statMods = new StatMod[] { };
    public Weapon.UseType useType = Weapon.UseType.None;

    [Header("Use Type Specific Values")]

    [Header("Shoot")]
    public GameObject projectile;
    public int count;

    public void Shoot()
    {
        if(useType == Weapon.UseType.Shoot)
        {
            //Do Shooty Things
        }
    }
}
