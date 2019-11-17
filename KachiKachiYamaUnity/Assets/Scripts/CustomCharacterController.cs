using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomCharacterController : MonoBehaviour
{

    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
    public Sprite FlintStoneOutSprite;
    public Sprite StrikingSparkSprite;
    public Sprite CharacterWalking;
    public float CharacterSpeed = 0.5f;
    public float SparkStrikingTime = 0.2f;
    public SpriteRenderer CharacterSprite;
    public int StrikeCounter = 0;
    public bool FlintStoneOut = false;
    public bool StrikingSpark = false;
    static bool RacoonCheckingReference;
    public bool busted = false;
    public Text SuccessfulStrikesCounter;

    public GameObject BustedUI;
    public GameObject YouWonUI;
    public Transform RacoonPosition;
    float horizontalMove = 0f;

    public Rigidbody2D MainCharacterRB;

    public float distanceFromRacoon;
    void Start()
    {
        CharacterSprite = GetComponent<SpriteRenderer>();
        MainCharacterRB = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * CharacterSpeed;
        RacoonCheckingReference = racoonMovement.RacoonChecking;

        distanceFromRacoon = Mathf.Abs(RacoonPosition.transform.position.x - transform.position.x);

        if(StrikeCounter == 5){
            YouWonUI.SetActive(true);
            StartCoroutine(WaitForRestart());
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

    void FixedUpdate(){
        Move(horizontalMove * Time.fixedDeltaTime);
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

    void Move(float move)
	{
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, MainCharacterRB.velocity.y);
			// And then smoothing it out and applying it to the character
			MainCharacterRB.velocity = Vector3.SmoothDamp(MainCharacterRB.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}