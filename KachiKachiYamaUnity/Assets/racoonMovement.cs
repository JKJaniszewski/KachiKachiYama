using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class racoonMovement : MonoBehaviour
{

    public float RacoonSpeed;
    public float RandomRotationLowerLimit = 2f;
    public float RandomRotationUpperLimit = 10f;

    public static bool RacoonChecking = false;
    float tempRacoonSpeed;

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
        
    }

    IEnumerator RandomlyRotate(){
        waitTime = Random.Range(RandomRotationLowerLimit,RandomRotationUpperLimit);
        Debug.Log(waitTime);
        yield return new WaitForSeconds(waitTime);
        RacoonSprite.flipX = true;
        RacoonChecking = true;
        tempRacoonSpeed = RacoonSpeed;
        RacoonSpeed = 0;
        yield return new WaitForSeconds(Random.Range(1,2));
        RacoonSpeed = tempRacoonSpeed;
        RacoonSprite.flipX = false;
        RacoonChecking = false;
        StartCoroutine(RandomlyRotate());
    }
}
