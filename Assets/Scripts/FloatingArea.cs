using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingArea : MonoBehaviour
{
    bool entered = false;
    public List<GameObject> floatables;
    public static FloatingArea instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !entered)
        {
            foreach(GameObject floatable in floatables)
            {
                floatable.GetComponent<ObjectFloat>().floating = true;
            }

            entered = true;
        }
    }
}
