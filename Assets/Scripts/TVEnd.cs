using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TVEnd : MonoBehaviour
{
    [SerializeField] TVTrigger trigger;
    [SerializeField] GameObject NewSpawn;
    List<ObjectExplode> objects;
    bool triggered = false;

    private void Start()
    {
        objects = GetComponentsInChildren<ObjectExplode>().ToList<ObjectExplode>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !triggered)
        {
            trigger.Reset();

            foreach(var obj in objects)
                obj.explode = true;

            RespawnManager.instance.ChangeSpawn(NewSpawn.transform.position);

            triggered = true;
        }
    }
}
