using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PipeMove : MonoBehaviour
{
    public float moveSpeed = 6;
    public float deadZone = -45;
    void FixedUpdate()
    {
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;
        if (transform.position.x < deadZone)
        {
            // Debug.Log("Pipe Deleted");
            Destroy(gameObject);
        }
    }
}
