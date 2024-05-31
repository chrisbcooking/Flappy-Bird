using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;

public class PipeMoving : MonoBehaviour
{
    private float currentY;
    private float yMax = 8f, yMin = -8f;
    private bool reverse;

    private void Awake()
    {
        reverse = (Random.Range(0, 100) > 50 ? false : true);
    }

    void Update()
    {
        currentY = transform.position.y;

        if (!reverse)
        {
            transform.position += Vector3.up * 3f * Time.deltaTime;
            reverse = (currentY >= yMax) ? true : false;
        }
        else
        {
            transform.position += Vector3.down * 3f * Time.deltaTime;
            reverse = (currentY <= yMin) ? false : true;
        }
    }
}
