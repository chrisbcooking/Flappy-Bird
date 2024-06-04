using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHandler : MonoBehaviour
{
    private BirdScript birdScript;
    public int currentArrows;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (!collision.transform.CompareTag("Arrow"))
            {
                birdScript = FindObjectOfType<BirdScript>();
                if (birdScript != null)
                {
                    // cool thing
                    birdScript.currentArrows = currentArrows;
                }
                Destroy(gameObject);
                currentArrows -= 1;
            }
        }
    }
}
