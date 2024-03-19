using UnityEngine;

public class TrackKillTrigger : MonoBehaviour
{
    public GameObject[] metrocars;
    public float offset = 100;

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
            metrocar.transform.position = new Vector3(metrocar.transform.position.x, metrocar.transform.position.y, PlayerMovement.instance.transform.position.z + offset);

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
