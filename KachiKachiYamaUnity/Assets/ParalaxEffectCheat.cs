﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxEffectCheat : MonoBehaviour
{

    Camera mainCamera;
    Rigidbody2D thisElementRB;
    [Range(0f, 1f)]
    public float thisParallaxSpeed = 1;
    Vector2 newVelocity;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        thisElementRB = GetComponent<Rigidbody2D>();
        if(thisElementRB == null){
            thisElementRB = gameObject.AddComponent<Rigidbody2D>();
            thisElementRB.isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {   
        newVelocity = new Vector2(-mainCamera.velocity.x * thisParallaxSpeed, 0);
        thisElementRB.velocity = newVelocity;
    }
}
