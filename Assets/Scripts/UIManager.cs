using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI controlText;

    private void Start()
    {
        controlText.GetComponent<Animator>().SetBool("ShowText", true);
        StartCoroutine(textFade(controlText));
    }

    public IEnumerator textFade(TextMeshProUGUI _text)
    {
        yield return new WaitForSeconds(3f);
        _text.GetComponent<Animator>().SetBool("ShowText", false);
    }
}
