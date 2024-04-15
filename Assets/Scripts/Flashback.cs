using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashback : MonoBehaviour
{
    [SerializeField] Animator animator;

    bool flashbackTriggered = false;
    public int animNum = 6;

    public List<GameObject> lights;

    [SerializeField] AudioSource lightBreak, scared;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !flashbackTriggered)
            FlashbackStart();
    }

    public void FlashbackStart()
    {
        flashbackTriggered = true;
        lightBreak.Play();

        //DeathTimer.instance.StartDeathTimer();

        foreach (GameObject light in lights)
        {
            light.GetComponentInParent<Animator>().SetBool("LightExplode", true);
            light.GetComponentInChildren<LightTrigger>().lightBroken = true;
        }
    }
}
