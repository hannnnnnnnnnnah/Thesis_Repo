using System.Collections;
using UnityEngine;

public class AnimTrigger : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] string animClip;
    [SerializeField]
    AudioSource lightBreakSound;

    bool animTriggered = false;
    public bool lightBreak = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !animTriggered)
        {
            animator.SetBool(animClip, true);
            animTriggered = true;
            StartCoroutine(StopAnim());
        }
    }

    IEnumerator StopAnim()
    {
        if (lightBreak)
        {
            lightBreakSound.Play();
            DeathTimer.instance.StartDeathTimer();
        }
        yield return new WaitForSeconds(2f);
        animator.SetBool(animClip, false);
    }
}
