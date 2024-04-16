using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehaviour : MonoBehaviour
{
    public Transform[] waypoints;
    public float cb_CarSpeed = 5f;

    void FixedUpdate()
    {
        if (transform.position != waypoints[0].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[0].position, cb_CarSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = waypoints[1].position;
        }
    }
}
