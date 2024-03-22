using System.Collections;
using UnityEngine;

public class FigureBehavior : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Animator trainAnim, wifeAnim;
    [SerializeField] GameObject figures, textPrompt;

    bool figurePushed = false;

    private void Start()
    {
        trainAnim = GetComponent<Animator>();    
    }

    private void Update()
    {
        if (NarrativeManager.instance.figureKilled && !figurePushed)
        {
            figurePushed = true;
            trainAnim.SetBool("Push", true);
            wifeAnim.SetBool("Die", true);
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
