using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : MonoBehaviour
{
    MeshCollider mesh;

    private void Start()
    {
        mesh = GetComponent<MeshCollider>();
    }

    public void StartSink()
    {
        mesh.enabled = false;
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }


}
