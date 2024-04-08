using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimationButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("active", true);
        Debug.Log("entered");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("active", false);
        Debug.Log("exiteddd");
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            EventSystem.current.SetSelectedGameObject(null);
            Debug.Log("clicked");
        }
    }

}
