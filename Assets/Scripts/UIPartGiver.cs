using UnityEngine;
using UnityEngine.UI;

public class UIPartGiver : MonoBehaviour
{
    public GameObject partToGive;
    private Transform weaponTransform;

    private void Start()
    {
        weaponTransform = GameObject.FindWithTag("Weapon").transform;

        GetComponent<Image>().sprite = partToGive.GetComponent<SpriteRenderer>().sprite;
    }

    public void GivePart()
    {
        Instantiate(partToGive, transform.position, Quaternion.identity, weaponTransform);
    }
}
