using UnityEngine;

public class TrainKill : MonoBehaviour
{
    [SerializeField] bool move_right, move_left;
    [SerializeField] float speed;
    [SerializeField] GameObject killTrigger;

    public bool move;
    public AudioSource t_audio;
    public Vector3 storePos;

    private void Start()
    {
        t_audio = GetComponent<AudioSource>();
        storePos = transform.position;
    }

    private void FixedUpdate()
    {
        float step = speed * Time.fixedDeltaTime;

        if (move)
        {
            if(!t_audio.isPlaying)
                t_audio.Play();

            if (move_right)
                transform.Translate(Vector3.back * step);

            if (move_left)
                transform.Translate(Vector3.forward * step);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            killTrigger.GetComponent<TrackKillTrigger>().StopTrain();
            t_audio.Stop();
            RespawnManager.instance.Die();
        }
    }
}
