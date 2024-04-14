using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleWifeAnim : MonoBehaviour
{
    [SerializeField] GameObject rotTarget;

    bool triggered, forceRotate, blendRotation = false;

    Quaternion storedRotation;
    float rotateSpeed = 150f;

    private void FixedUpdate()
    {
        if (forceRotate)
        {
            //it's fine it's fine it's fine look away ahahhaha
            var targetRotation = Quaternion.LookRotation(rotTarget.transform.position - PlayerMovement.instance.mainCamera.transform.position);
            PlayerMovement.instance.mainCamera.transform.rotation = Quaternion.RotateTowards(PlayerMovement.instance.mainCamera.transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }

        if (blendRotation)
        {
            PlayerMovement.instance.mainCamera.transform.rotation = Quaternion.RotateTowards(PlayerMovement.instance.mainCamera.transform.rotation, storedRotation, rotateSpeed * Time.deltaTime);
        }

    }
}
