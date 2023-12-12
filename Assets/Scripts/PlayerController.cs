using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    private SpriteRenderer sprite;
    private Animator spriteAnim;

    //Movement Variables
    private float horizontalInput;
    public int speed;
    public int normSpeed = 40;
    public int crouchSpeed = 7;
    public int airSpeed = 3;

    public int normDrag = 15;
    public int airDrag = 0;

    //Jump Variables
    public float jumpForce = 1200;
    private Rigidbody rb;
    public bool isOnGround;
    public bool isOnWall;

    //Shooting variables
    public bool aimMode = false;
    public GameObject aimRotator;
    public GameObject projectile;

    //Stealth Variables
    public bool isHiding = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        sprite = GameObject.Find("Chara_Sprite").GetComponent<SpriteRenderer>();
        spriteAnim = GameObject.Find("Chara_Sprite").GetComponent<Animator>();

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Aim button
        if (Input.GetMouseButton(0))
        {
            aimMode = true;
            rb.velocity = new Vector3(0, 0, 0);
        }
        else
        {
            aimMode = false;
        }

        //Movement
        //Can only move when not aiming
        if (aimMode == false)
        {
            //Makes aim marker invisible
            aimRotator.SetActive(false);

            //Left and right movement
            horizontalInput = Input.GetAxis("Horizontal");
            rb.AddForce(Vector3.right * horizontalInput * speed, ForceMode.Impulse);

            if(horizontalInput == 0)
            {
                //spriteAnim.gameObject.SetActive(false);
            }
            if(horizontalInput < 0)
            {
                //spriteAnim.gameObject.SetActive(true);
                sprite.flipX = true;
            }
            if(horizontalInput > 0)
            {
                //spriteAnim.gameObject.SetActive(true);
                sprite.flipX = false;
            }

            //Jump 
            if ((Input.GetKeyDown(KeyCode.Space) || 
                Input.GetKeyDown(KeyCode.W) || 
                Input.GetKeyDown(KeyCode.UpArrow)) &&
                (isOnGround || isOnWall))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

            //Crouch button
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                speed = crouchSpeed;
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
            {
                speed = normSpeed;
            }
        }
        
        //Aiming, Shooting
        if (aimMode == true)
        {
            //Makes aim marker visible
            aimRotator.SetActive(true);

            //Right mouse button shoots projectile
            if (Input.GetMouseButtonDown(1))
            {
                Instantiate(projectile, aimRotator.transform.position, aimRotator.transform.rotation);
            }
        }

        //Location Tracker
        if (isOnGround)
        {
            rb.drag = normDrag;
            speed = normSpeed;
        }
        else
        {
            rb.drag = airDrag;
            speed = airSpeed;
        }

        //Stealth Tracker
        if (gameManager.isStealthTime)
        {
            if (isHiding)
            {
                //Add to a score probably?
            }
            else if(!isHiding)
            {
                Destroy(gameObject);
                gameManager.GameOver = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Location marker (marks when the player is touching the wall or floor)
        if (other.gameObject.tag == "Floor")
        {
            isOnGround = true;
        }
        if (other.gameObject.tag == "Wall")
        {
            isOnWall = true;
            //rb.velocity = new Vector3(0, 0, 0);
        }

        //Marks whether the player is hiding 
        if (other.gameObject.tag == "StealthZone")
        {
            isHiding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Location marker (marks when the player is NOT touching the wall or floor)
        if (other.gameObject.tag == "Floor")
        {
            isOnGround = false;
        }
        if (other.gameObject.tag == "Wall")
        {
            isOnWall = false;
        }

        //Marks whether the player is NOT hiding 
        if (other.gameObject.tag == "StealthZone")
        {
            isHiding = false;
        }
    }
}
