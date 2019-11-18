using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxEffectCheat : MonoBehaviour
{

    Camera mainCamera;
    Rigidbody2D thisElementRB;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        thisElementRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        thisElementRB.velocity = -mainCamera.velocity;
    }
}
