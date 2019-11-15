using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class racoonMovement : MonoBehaviour
{

    public float RacoonSpeed;
    public float RandomRotationLowerLimit = 2f;
    public float RandomRotationUpperLimit = 10f;

    public static bool RacoonChecking = false;
    public float tempRacoonSpeed;

    float waitTime;

    public SpriteRenderer RacoonSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        RacoonSprite = GetComponent<SpriteRenderer>();
        StartCoroutine(RandomlyRotate());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right*Time.deltaTime * RacoonSpeed);
        CheckForRacoonRotation();
    }

    IEnumerator RandomlyRotate(){
        waitTime = Random.Range(RandomRotationLowerLimit,RandomRotationUpperLimit);
        yield return new WaitForSeconds(waitTime);  
        tempRacoonSpeed = RacoonSpeed;
        RacoonSpeed = 0;
        RacoonChecking = true;
        yield return new WaitForSeconds(Random.Range(1,2));
        RacoonChecking = false;
        RacoonSpeed = tempRacoonSpeed;
        StartCoroutine(RandomlyRotate());
    }

    void CheckForRacoonRotation(){
        if(RacoonChecking){
                RacoonSprite.flipX = true;
            }
            else{
                RacoonSprite.flipX = false;
            }
    }

    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "player" && RacoonChecking == false){
            //Debug.Log("Collided with Racoon");
            StopCoroutine(RandomlyRotate());
            StartCoroutine(LookingBackCausedByCollision());
        }
    }

    IEnumerator LookingBackCausedByCollision(){
        RacoonChecking = true;
        tempRacoonSpeed = RacoonSpeed;
        RacoonSpeed = 0;
        yield return new WaitForSeconds(Random.Range(1,2));
        StartCoroutine(RandomlyRotate());
        RacoonChecking = false;
        RacoonSpeed = tempRacoonSpeed;
        
    }

}
