using System.Collections;
using UnityEngine;

public class LightTimer : MonoBehaviour
{
    [SerializeField] float timerDelayLight, timerDelayDark;
    [SerializeField] GameObject lInactive;

    public bool timerActive = true;

    LightTrigger trigger;

    private void Start()
    {
        trigger = GetComponentInChildren<LightTrigger>();
    }

    public IEnumerator Timer()
    {
        while (timerActive)
        {
            lInactive.SetActive(false);
            trigger.lightBroken = false;
            trigger.spotlight.enabled = true;
            yield return new WaitForSeconds(timerDelayDark);
            lInactive.SetActive(true);
            trigger.lightBroken = true;
            trigger.spotlight.enabled = false;
            yield return new WaitForSeconds(timerDelayLight);
        }
    }
}
