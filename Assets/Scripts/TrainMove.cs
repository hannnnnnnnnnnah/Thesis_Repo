using UnityEngine;

public class TrainMove : MonoBehaviour
{
    [SerializeField] bool move_right, move_left;
    [SerializeField] float speed;
    [SerializeField] GameObject stopPos;
    [SerializeField] AudioSource t_audio;

    public bool move;
    public Vector3 moveDirection;

    private void Start()
    {
        if(move_right)
            moveDirection = Vector3.back;

        if(move_left)
            moveDirection = Vector3.forward;
    }

    private void FixedUpdate()
    {
        float step = speed * Time.fixedDeltaTime;

        if (move)
        {
            if (Vector3.Distance(transform.position, stopPos.transform.position) >= 2f) 
            {
                transform.Translate(moveDirection * step);

                if (!t_audio.isPlaying)
                {
                    t_audio.Play();
                }
            }
            else
            {
                t_audio.Stop();
                move = false;
            }      
        }
    }
}
