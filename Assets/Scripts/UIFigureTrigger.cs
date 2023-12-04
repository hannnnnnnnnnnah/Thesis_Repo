using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFigureTrigger : MonoBehaviour
{
    [SerializeField] FigureDisappear figureDisappear;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("NEAR FIGURE");
            UIManager.instance.ResetEye("EyeVisible", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            UIManager.instance.ResetEye("EyeVisible", false);
            Debug.Log("LEFT FIGURE");
            figureDisappear.PlayerLeft();
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Player")
        {
            Debug.Log("NEAR FIGURE collider");
            UIManager.instance.ResetEye("EyeVisible", true);
        }
    }
}
