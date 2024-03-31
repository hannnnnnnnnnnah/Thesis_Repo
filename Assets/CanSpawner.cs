using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CanSpawner : MonoBehaviour
{
    [SerializeField] GameObject can;
    [SerializeField] float spawnRate;

    public bool spawning = true;

    private void OnEnable()
    {
        spawning = true;
        StartCoroutine(SpawnCan());
    }

    void OnDisable() 
    {
        spawning = false;
    }

    IEnumerator SpawnCan()
    {
        while (spawning)
        {
            yield return new WaitForSeconds(spawnRate);
            Instantiate(can, new Vector3(transform.position.x + Random.value * 2, transform.position.y, transform.position.z + Random.value * 2), Quaternion.identity);
        }
    }
}
