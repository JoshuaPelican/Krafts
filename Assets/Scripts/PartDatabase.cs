using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartDatabase : MonoBehaviour
{
    public GameObject UIPartGiver;
    public Transform giverContainer;

    public GameObject[] database = new GameObject[] { };

    private void Awake()
    {
        foreach (GameObject part in database)
        {
            UIPartGiver newGiver = Instantiate(UIPartGiver, giverContainer).GetComponent<UIPartGiver>();
            newGiver.partToGive = part;
        }
    }
}
