using System.Collections;
using UnityEngine;

public class LightTimer : MonoBehaviour
{
    [SerializeField] float timerDelayLight, timerDelayDark;

    public bool timerActive = true;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while (timerActive)
        {
            animator.SetBool("LightExplode", true);
            yield return new WaitForSeconds(timerDelayDark);
            animator.SetBool("LightExplode", false);
            yield return new WaitForSeconds(timerDelayLight);
        }
    }
}
