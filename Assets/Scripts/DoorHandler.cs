using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    [SerializeField] List<GameObject> leftDoors, rightDoors = new List<GameObject>();
    [SerializeField] BoxCollider SideLeft, SideRight;

    public bool leftSided, rightSided;

    public void DisableDoors()
    {
        SideLeft.enabled = true;
        SideRight.enabled = true;

        foreach (GameObject door in leftDoors) 
        {
            door.GetComponent<BoxCollider>().enabled = false;
            door.GetComponent<Animator>().SetBool("NearDoor", false);
        }

        foreach (GameObject door in rightDoors)
        {
            door.GetComponent<BoxCollider>().enabled = false;
            door.GetComponent<Animator>().SetBool("NearDoor", false);
        }
    }

    public void EnableDoors()
    {
        if (rightSided)
        {
            SideRight.enabled = false;
            SideLeft.enabled = true;

            foreach (GameObject door in rightDoors)
                door.GetComponent<BoxCollider>().enabled = true;
        }

        if (leftSided)
        {
            SideRight.enabled = true;
            SideLeft.enabled = false;

            foreach (GameObject door in leftDoors)
                door.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
