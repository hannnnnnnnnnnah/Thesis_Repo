using UnityEngine;

public class TrackKillTrigger : MonoBehaviour
{
    public GameObject[] metrocars;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Death effects are reset
            other.GetComponent<DeathTimer>().StopAllCoroutines();
            other.GetComponent<DeathTimer>().DeathEffectsCancel();

            other.GetComponent<PlayerMovement>().inTracks = true;

            StartTrain();
        }
    }

    public void StartTrain()
    {
        foreach(GameObject metrocar in metrocars)
        {
            metrocar.gameObject.SetActive(true);
            metrocar.GetComponent<TrainMove>().move = true;
        }
    }

    public void StopTrain()
    {
        //Show normal trains
        NarrativeManager.instance.ShowTrains();

        foreach (GameObject metrocar in metrocars)
        {
            metrocar.GetComponent<TrainMove>().move = false;
            metrocar.GetComponent<TrainMove>().t_audio.Stop();
            metrocar.gameObject.transform.position = metrocar.GetComponent<TrainMove>().storePos;
            metrocar.gameObject.SetActive(false);
        }
           
    }
}
