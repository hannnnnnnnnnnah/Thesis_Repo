using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI controlText;
    [SerializeField] Image eye;

    public bool flashlightShown, sneakShown = false;

    Animator animator;
    string moveText = "Use WASD to move";

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
        //ShowText(moveText);
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
