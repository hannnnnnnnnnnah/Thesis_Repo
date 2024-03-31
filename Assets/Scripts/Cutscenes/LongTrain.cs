using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongTrain : MonoBehaviour
{
    TrainMove trainMove;
    [SerializeField] GameObject canSpawner;
    [SerializeField] PhoneTrigger phoneTrigger;
    [SerializeField] List<Collider> colliders;
    [SerializeField] AudioSource call;

    bool triggered, startStop = false;

    private void Start()
    {
        trainMove = GetComponent<TrainMove>();
    }

    private void Update()
    {
        if (!trainMove.move && triggered && !startStop)
            StopAction();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            trainMove.move = true;

            foreach (var collider in colliders)
                collider.enabled = true;

            StartCoroutine(canTime());
            triggered = true;
        }
    }

    IEnumerator canTime()
    {
        yield return new WaitForSeconds(4f);
        canSpawner.SetActive(true);
    }

    void StopAction()
    {
        startStop = true;   
        canSpawner.SetActive(false);

        foreach (var collider in colliders)
            collider.enabled = false;

        phoneTrigger.PhoneStop();
        call.Play();
    }
}
