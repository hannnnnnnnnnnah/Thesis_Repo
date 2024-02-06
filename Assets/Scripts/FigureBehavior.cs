using System.Collections;
using UnityEngine;

public class FigureBehavior : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Animator ghostAnim;
    [SerializeField] GameObject figures, textPrompt;

    float distRange = 10f;

    Vector3 pos2;

    bool move = false;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        { 
            move = false;
            animator.SetBool("Push", true);
            ghostAnim.SetBool("Spawn", true);

            StartCoroutine(FigureDelay());

            NarrativeManager.instance.figureKilled = true;
            NarrativeManager.instance.TriggerLevelSwitch();
        }
    }

    private void FixedUpdate()
    {
        float step = speed * Time.fixedDeltaTime;
        float dist = Vector3.Distance(transform.position, pos2);
        
        if (move)
        {
            animator.SetBool("Turn", false);
            transform.Translate(Vector3.right * step);

            if (dist <= distRange)
                move = false;
        }
        else if(!move)
        {
            animator.SetBool("Turn", true);
        }
    }

    public IEnumerator FigureDelay()
    {
        yield return new WaitForSeconds(1f);
        //spawn figures
        figures.SetActive(true);
        textPrompt.SetActive(true);
    }
}
