using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFlicker : MonoBehaviour
{
    [SerializeField] GameObject obj;
    bool flickerStarted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !flickerStarted)
        {
            StartCoroutine(Flicker());
            flickerStarted = true;
        }
    }

    IEnumerator Flicker()
    {
        obj.SetActive(false);
        yield return new WaitForSeconds(.25f);
        obj.SetActive(true);
        yield return new WaitForSeconds(.5f);
        Destroy(obj);
        Destroy(gameObject);
    }
}
