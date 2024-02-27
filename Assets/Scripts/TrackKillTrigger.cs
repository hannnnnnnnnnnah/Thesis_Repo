using UnityEngine;

public class TrackKillTrigger : MonoBehaviour
{
    public GameObject[] metrocars;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Death effects are reset
            DeathTimer.instance.StopAllCoroutines();
            DeathTimer.instance.DeathEffectsCancel();

            PlayerMovement.instance.inTracks = true;

            StartTrain();
        }
    }

    public void StartTrain()
    {
        foreach(GameObject metrocar in metrocars)
        {
            metrocar.gameObject.SetActive(true);
            metrocar.GetComponent<TrainKill>().move = true;
        }
    }

    public void StopTrain()
    {
        //Show normal trains
        NarrativeManager.instance.ShowTrains();

        foreach (GameObject metrocar in metrocars)
        {
            metrocar.GetComponent<TrainKill>().move = false;
            metrocar.GetComponent<TrainKill>().t_audio.Stop();
            metrocar.gameObject.transform.position = metrocar.GetComponent<TrainKill>().storePos;
            metrocar.gameObject.SetActive(false);
        }
           
    }
}
