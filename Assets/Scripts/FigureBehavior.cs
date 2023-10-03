using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class FigureBehavior : MonoBehaviour
{
    [SerializeField] Collider dist1, dist2, dist3;
    [SerializeField] GameObject loc1, loc2;
    [SerializeField] float speed;

    Vector3 pos1, pos2;

    bool move = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (dist1.bounds.Intersects(other.bounds))
            { 
                Run(transform.position, loc1.transform.position);
                //gameObject.transform.position = loc1.transform.position;
                dist1.enabled = false;
            }

            if (dist2.bounds.Intersects(other.bounds))
            {
                transform.position = loc2.transform.position;
                dist2.enabled = false;
            }

            if (dist3.bounds.Intersects(other.bounds))
            {
                Debug.Log("wooaoaoaooao push");
                dist3.enabled = false;
            }
        }
    }

    private void Run(Vector3 _pos1, Vector3 _pos2)
    {
        pos1 = _pos1;
        pos2 = _pos2;   
        move = true;
    }

    private void Update()
    {
        var step = speed * Time.deltaTime;

        if (move && Mathf.Clamp((transform.position)f, pos1, pos2) != pos2)
        {
            //transform.position = Vector3.MoveTowards(pos1, pos2, step);
            transform.position = Vector3.Lerp(pos1, pos2, step);
        }     
        else
            move = false;
    }
}
