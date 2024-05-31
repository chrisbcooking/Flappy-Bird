using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Rigidbody2D Rigidbody2D;
    private Animator birdAnimator;

    // Start is called before the first frame update
    void Start()
    {
        birdAnimator = GameObject.Find("Wings").GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Rigidbody2D != null) 
        {
            birdAnimator.SetFloat("BirdVelocity", Rigidbody2D.velocity.y + 5);
        }
    }
}
