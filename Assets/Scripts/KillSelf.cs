using System.Collections;
using UnityEngine;

public class KillSelf : MonoBehaviour
{
    public float lifespan;
    bool dieTriggered = false;
    ObjectThrow objThrow;

    private void Start()
    {
        objThrow = GetComponent<ObjectThrow>();
    }

    private void Update()
    {
        if (objThrow.toss && !dieTriggered)
        {
            StartCoroutine(Die());
            dieTriggered = true;
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(gameObject);
    }
}
