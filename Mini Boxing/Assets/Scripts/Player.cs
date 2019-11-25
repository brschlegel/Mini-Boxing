using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUp { CtrlX, CtrlV, None, Trampoline }

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    private float force;
    public KeyCode[] controls; //[up,down,left,right,dash,powerup]
    public Player otherPlayer;
   


    //jumping
    public bool canJump;
    private int jumpForce;

    //Boosting
    public int boostSpeed;
    private float boostLength;
    public Vector3 directionFaced;

    //bouncing 
    public int bounceForce;

    //powerups
    public PowerUp powerUpState;
    public GameMaster gm;
    public GameObject lastPlatformOn; //last platform the player has stood on
    public GameObject trampoline;
   


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canJump = true;
        directionFaced = Vector3.left;
        force = 10f;
        jumpForce = 400;
        canJump = true;
        boostLength = .2f;
        powerUpState = PowerUp.None;
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();

    }

    public PowerUp PowerUpState
    {
        set { powerUpState = value; }
    }

    void Update()
    {
        if(Input.GetKeyDown(controls[0]))
        {

            if (canJump)
                rb.AddForce(Vector3.up * jumpForce);
            canJump = false;
        }
        if (Input.GetKeyDown(controls[4]))
        {
            StartCoroutine("Boost");

        }
        if (Input.GetKeyDown(controls[5]))
        {
           
            UsePowerUp();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        if (Input.GetKeyDown(controls[1]))
        {
            rb.AddForce(Vector3.down * jumpForce);
        }
        if (Input.GetKey(controls[2]))
        {
            rb.AddForce(Vector3.left * force);
            directionFaced = Vector3.left;
        }
        if (Input.GetKey(controls[3]))
        {
            rb.AddForce(Vector3.right * force);
            directionFaced = Vector3.right;
        }

       

       

    }

    IEnumerator Boost()
    {
       
        rb.AddForce(directionFaced * boostSpeed);
        

       
        yield return new WaitForSeconds(boostLength);

      

    }

    public void UsePowerUp()
    {
        
        switch (powerUpState)
        {
            case PowerUp.None:

                break;

            case PowerUp.CtrlX:
                gm.SplitPlatform(lastPlatformOn, transform);
                powerUpState = PowerUp.CtrlV;
                break;
            case PowerUp.CtrlV:
                gm.AddPlatform(transform);
                powerUpState = PowerUp.None;
                break;
            case PowerUp.Trampoline:
                Instantiate(trampoline, transform.position + new Vector3(0, -1, 0), Quaternion.identity) ;
                powerUpState = PowerUp.None;
                break;
                    
            
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        canJump = true;
    
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            
            otherPlayer = collision.gameObject.GetComponent<Player>();
            //vector from otherplayer to this
            Vector2 bounceVector = new Vector2(transform.position.x - otherPlayer.transform.position.x,
                transform.position.y - otherPlayer.transform.position.y);
            
            rb.AddForce(bounceVector * bounceForce );

        }

        if (collision.gameObject.tag == "Platform")
        {
            lastPlatformOn = collision.gameObject;
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            otherPlayer = collision.gameObject.GetComponent<Player>();
            //vector from otherplayer to this
            Vector2 bounceVector = new Vector2(transform.position.x - otherPlayer.transform.position.x,
                transform.position.y - otherPlayer.transform.position.y);
            Debug.Log(bounceVector);
            rb.AddForce(bounceVector * bounceForce);

        }
    }
}
