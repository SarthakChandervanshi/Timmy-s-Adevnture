using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public LayerMask playerMask;
    public GameObject Sword,pos_sword;
    public bool canMoveInAir = true;
    // public GameObject gameOverScreen;
    // public GameManager theGameManager;


    float fireRate = 0;
    float nextfire = 0;
    // to set the speed when the Player moves
    [SerializeField] private float speed;
    // for Rigidbody2D components
    private Rigidbody2D rigidBody;
    // To store the conditional value
    //Player when moving right or left
    private float moveInput;
    // To condition true when Player faces right
    private bool facingRight;

    // assigns a value to how high the Player can jump
    [SerializeField] private float jumpForce;
    // indicates true if the player touches the ground
    [SerializeField] private bool isGrounded;
    // ensure that the Player's feet are down,
    //like the soles of the feet 
    [SerializeField] private Transform feetPos;
    // This is used to set how big your Player's foot radius is
    [SerializeField] private float circleRadius;
    // It is used to ensure object
    //which acts as ground
    [SerializeField] private LayerMask whatIsGround;

    public int levelAt;

    //we call this variable
    //to run idle, run and jump animations
    private Animator anim;

    private void Start()
    {
        //initialize existing Rigidbody2D component in Player
        rigidBody = GetComponent<Rigidbody2D>();
        //we set at the beginning TRUE because the player is facing right
        facingRight = true;
        //Initialize the existing Animator component on the Player
        anim = GetComponent<Animator>();
        //Initialising isJumping as false cause in the beginning its in idle state
        anim.SetBool("isJumping", false);

        if(PlayerPrefs.GetInt("levelAt") == null)
        {
            PlayerPrefs.SetInt("levelAt", levelAt);
        }
        else
        {
            Debug.Log(PlayerPrefs.GetInt("levelAt"));
        }
    }


    private void Update()
    {
        //By calling the Physics2D class and the OverlapCircle function
        //which has these 3 parameters indicates that
        //isGrounded will be true if all three parameters are met
        isGrounded = Physics2D.OverlapCircle(feetPos.position, circleRadius, whatIsGround);


        //Function for Player when jumping
        CharacterJump();

        if(fireRate == 0)
        {

          if(Input.GetKeyDown(KeyCode.Space))
          {
            Shooting();
          }
          else
          {
            if(Input.GetKeyDown(KeyCode.Space) && Time.time > nextfire)
            {
              nextfire = Time.time + nextfire;
              Shooting();
            }
          }
        }
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
		if (screenPosition.y > Screen.height || screenPosition.y < 0){
            died();
        }

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
      if(coll.gameObject.tag == "Batas_Mati")
      {
        died();
      }
    }

    void Die(){
        Debug.Log("Game Over");
		SceneManager.LoadScene("Menu");
    }

    void died()
    {
        SceneManager.LoadScene("GameOver");
        

    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(5);
    }


    private void FixedUpdate()
    {
        CharacterMovement();
        CharacterAnimation();

    }
    public float Scale_karak;
     void Shooting(){
           if (Scale_karak == 1f){
            GetComponent<Rigidbody2D>().velocity = new Vector2(8f, GetComponent<Rigidbody2D>().velocity.y);
        }
        else{
            GetComponent<Rigidbody2D>().velocity = new Vector2(-8f, GetComponent<Rigidbody2D>().velocity.y);
        }
        Instantiate(Sword, pos_sword.transform.position, pos_sword.transform.rotation);
    }
    // void OnMousDown (){
	// 	Instantiate(Sword, pos_sword.transform.position, pos_sword.transform.rotation);

	// }

    private void CharacterMovement()
    {
        //Input.GetAxis is a function
        //which has been provided by Unity
        //To see the input keyboard, my friend
        // open the edit menu and select Project Settings and select Input
        moveInput = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(moveInput * speed));

        if (moveInput > 0 && facingRight == false)
        {

            Flip();
        }
        else if (moveInput < 0 && facingRight == true)
        {
            //Useful function for Player
            //can face right or left
            Flip();
        }
        // value on X axis will increase according to speed * moveInput
        rigidBody.velocity = new Vector2(speed * moveInput, rigidBody.velocity.y);
    }

    public void Landing()
    {
        anim.SetBool("isJumping", false);
    }

    void CharacterJump()
    {

        if (anim.GetBool("isJumping") == false && isGrounded == true && Input.GetKeyDown(KeyCode.UpArrow))
        {
            //How to call animation with
            //parameters of type Trigger
            anim.SetBool("isJumping", true);
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
            Landing();
        }
    }

    void CharacterAnimation()
    {
        if (moveInput != 0 && isGrounded == true)
        {
            //how to call animation with
            // parameter of type BOOL
            anim.SetBool("isRun", true);

        }
        else if (moveInput == 0 && isGrounded == true)
        {
            anim.SetBool("isRun", false);
        }
    }

    private void Flip()
    {
        //facingRight is not equal to facingRight
        facingRight = !facingRight;
        //create a variable of type Vector3
        // whose contents = transform.localScale
        //(Scling on axis x=1, y=1,z=1)
        Vector3 scaler = transform.localScale;
        //then multiply on the x-axis
        //with minus so that the x-axis
        // later will have a minus value
        scaler.x *= -1;
        //and finally the x-axis on the Player is given
        //minus value so that when Player faces
        //to the left of the x-axis in Player will be -1
        transform.localScale = scaler;
        //NOTE : This x-axis is the existing x-axis
        // on the existing Scale in the Transform component
    }
}
