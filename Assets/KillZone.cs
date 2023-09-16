using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class KillZone : MonoBehaviour
{
    [SerializeField] AudioSource hit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //StartCoroutine(trainHit());
            RespawnManager.instance.Die();
        }
    }

    /*
    public IEnumerator trainHit()
    {
        yield return new WaitForSeconds(0.08f);
        RespawnManager.instance.Die();
    }*/
}
