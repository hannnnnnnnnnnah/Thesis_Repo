using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitAnimationTrigger : MonoBehaviour
{
    [SerializeField] GameObject newPlayer, newSpawn, Part3;
    void Exit()
    {
        newPlayer.SetActive(false);
        PlayerMovement.instance.mainCamera.enabled = true;
        PlayerMovement.instance.move = true;
        PlayerMovement.instance.rotate = true;
        NarrativeManager.instance.EndOfGame();


        RespawnManager.instance.ChangeSpawn(newSpawn.transform.position);
        RespawnManager.instance.Die();
        Part3.SetActive(false);
    }
}
