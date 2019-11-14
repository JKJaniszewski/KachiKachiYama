using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomCharacterController : MonoBehaviour
{

    
    public Sprite FlintStoneOutSprite;
    public Sprite StrikingSparkSprite;

    public Sprite CharacterWalking;

    public float CharacterSpeed = 0.5f;
    public float SparkStrikingTime = 0.2f;
    public SpriteRenderer CharacterSprite;

    // Start is called before the first frame update
    public int StrikeCounter = 0;
    public bool FlintStoneOut = false;
    public bool StrikingSpark = false;
    static bool RacoonCheckingReference;
    public bool busted = false;
    public Text SuccessfulStrikesCounter;

    public GameObject BustedUI;
    public GameObject YouWonUI;
    public Transform RacoonPosition;

    public float distanceFromRacoon;
    void Start()
    {
        CharacterSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        RacoonCheckingReference = racoonMovement.RacoonChecking;

        distanceFromRacoon = Mathf.Abs(RacoonPosition.transform.position.x - transform.position.x);

        if(StrikeCounter == 5){
            YouWonUI.SetActive(true);
            StartCoroutine(WaitForRestart());
        }
        
         if (Input.GetKey(KeyCode.D)){
             transform.Translate(Vector3.right*Time.deltaTime * CharacterSpeed);
        }

        if (Input.GetKey(KeyCode.A)){
             transform.Translate(Vector3.left*Time.deltaTime * (CharacterSpeed+.5f));
        }


        if (Input.GetMouseButtonUp(1)){

            Debug.Log("F was pressed");

            if(FlintStoneOut == false){
                FlintStoneOut = true;
                Debug.Log("Flint Stone Out");
                CharacterSprite.sprite = FlintStoneOutSprite;
            }
            else{
            FlintStoneOut = false;
            CharacterSprite.sprite = CharacterWalking;
            }
        }

        if (Input.GetMouseButtonUp(0) && FlintStoneOut){

            StartCoroutine(StrikingSparkSpriteWait());

        }

        if(FlintStoneOut == true && RacoonCheckingReference == true){
            busted = true;
            BustedUI.SetActive(true);
            StartCoroutine(WaitForRestart());
        }
    }

    IEnumerator StrikingSparkSpriteWait(){
        CharacterSprite.sprite = StrikingSparkSprite;
        StrikingSpark = true;
        yield return new WaitForSeconds(SparkStrikingTime);
        StrikingSpark = false;
        FlintStoneOut = false;
        CharacterSprite.sprite = CharacterWalking;
        if(!busted && distanceFromRacoon<1.5){
            StrikeCounter++;
            SuccessfulStrikesCounter.text = StrikeCounter.ToString() +"/5";
        }
    }
    IEnumerator WaitForRestart(){
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
