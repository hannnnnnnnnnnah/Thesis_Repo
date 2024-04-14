using System.Collections;
using UnityEngine;

public class StairsCutscene : MonoBehaviour
{
    [SerializeField] GameObject StairsCutsceneUi, Wife, newSpawn, Part3;
    [SerializeField] AudioSource EndNoise;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StairsCutsceneUi.SetActive(true);
            EndNoise.Play();
            PlayerMovement.instance.speed = 3f;
            animator.SetBool("StartStairs", true);
        }
    }

    public void TheEnd()
    {
        SoundManager.PlaySound("look");
        PlayerMovement.instance.dr.StartDialogue("lookatme");
    }

    public void Exit()
    {
        StartCoroutine(ExitDelay());
    }

    IEnumerator ExitDelay()
    {
        yield return new WaitForSeconds(.25f);

        NarrativeManager.instance.EndOfGame();
        RespawnManager.instance.ChangeSpawn(newSpawn.transform.position);
        RespawnManager.instance.Die();
        DeathTimer.instance.DeathEffectsCancel();
        Part3.SetActive(false);

        yield return new WaitForSeconds(.2f);

        StairsCutsceneUi.SetActive(false);
    }
}
