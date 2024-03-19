using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI controlText;

    Animator animator;

    public static UIManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ShowText(string text)
    {
        if (RespawnManager.instance.deathCount == 0)
        {
            controlText.SetText(text);
            animator.SetBool("ShowText", true);
            StartCoroutine(TextFade());
        }
    }

    public IEnumerator TextFade()
    {
        yield return new WaitForSeconds(2f);
        animator.SetBool("ShowText", false);
    }
}
