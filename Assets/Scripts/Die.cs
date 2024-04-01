using System.Collections;
using UnityEngine;

public class Die : MonoBehaviour
{
    public float lifespan;

    private void Start()
    {
        StartCoroutine(DieCountdown());   
    }

    IEnumerator DieCountdown()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(gameObject);
    }
}
