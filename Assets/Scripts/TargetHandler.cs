using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetHandler : MonoBehaviour
{
    private bool openPipe = false;
    private Transform parent;
    private float openSpeed = 2f;
    private void Start()
    {
        parent = transform.parent.GetChild(0);
    }
    private void Update()
    {
        if (openPipe && parent.position.y < 36.5)
        {
            parent.position += new Vector3(0, openSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Arrow")
        {
            openPipe = true;
        }
    }
}
