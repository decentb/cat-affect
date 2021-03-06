﻿using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

    //animator variables
    //refrence for movement and animating: http://www.youtube.com/watch?v=Xnyb2f6Qqzg
    bool facingRight = true;
    Animator anim;
    public Transform groundCheck;
    bool isGrounded = false;
    float groundRadius = 0.2f;
    //what is ground? that is the question
    public LayerMask whatIsGround;


    private float lockPos = 0;

    //The prefab will overwrite these
    public float airGravy = 1.0f;
    public float jumpForce = 400.0f;
    public float normalGravity = 1.0f;
    public float maxSpeed = 10f;
    //public float maxJump = 10f;

    public bool isSwimming = false;
    //public float swimForce = 2.0f;
    public float swimGravy = 0.5f;
    public float swimJumpForce = 100.0f;
    public float swimGravity = 0.3f;
    public float swimMaxSpeed = 4.0f;

    //For swimming sound; added by Rebeca.
    private float swimSoundTimer;

    // Use this for initialization
    void Start()
    {
        //Animation Stuff
        anim = GetComponent<Animator>();


        normalGravity = rigidbody2D.gravityScale;
        SwitchToWalk();

        swimSoundTimer = Time.time;

    }

    public void SwitchToSwim()
    {
        isSwimming = true;
        rigidbody2D.gravityScale = swimGravity;
        anim.SetBool("isSwimming", isSwimming);
    }

    public void SwitchToWalk()
    {
        isSwimming = false;

        //used to prevent kat colliding on water when he jumps out
        isGrounded = false;
        

        rigidbody2D.gravityScale = normalGravity;
        anim.SetBool("isSwimming", isSwimming);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        //if your in the air, your not grounded
        if (!(rigidbody2D.velocity.y < 1 && rigidbody2D.velocity.y > -1))
        {
            isGrounded = false;
        }

        //Your not grounded when your swimming
        if (isSwimming)
        {
            isGrounded = false;
        }

        anim.SetBool("isGrounded", isGrounded);

        anim.SetFloat("vSpeed", rigidbody2D.velocity.y);

        //kitty rotation lock
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, lockPos, lockPos);

        float move = Input.GetAxis("Horizontal");

        //Swimming force
        if (isSwimming)
        {
            rigidbody2D.velocity = new Vector2(move * maxSpeed*swimGravy, rigidbody2D.velocity.y);
        }
        //NormalForce
        else
        {
            rigidbody2D.velocity = new Vector2(move * maxSpeed *airGravy, rigidbody2D.velocity.y);
        }

        //connects animator variables from animator controller
        anim.SetFloat("Speed", Mathf.Abs(move));
        
 

        //direction flipper
        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }

    }

    void Update()
    {
        // Escape to quit
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        // R to restart
        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        //Gather all sounds associated with the Kitty, to play later. Added by Rebeca.
        AudioSource[] kittySounds = gameObject.GetComponents<AudioSource>();

        AudioSource jumpSound = null;
        AudioSource[] splashSounds = new AudioSource[4];
        int i = 0;

        foreach (AudioSource sound in kittySounds)
        {

            //Assign the jump sound and splash sounds.
            if (sound != null && sound.clip != null && sound.clip.name == "cat_jump")
            {
                jumpSound = sound;
            }
            else if (i < 4 && sound.clip != null && sound.clip.name.Contains("splash_"))
            {
                splashSounds[i] = sound;
                i++;
            }
        }


        anim.SetBool("isSwimming", isSwimming);

        if (isSwimming)
        {


            //Play a sound every 2 seconds if swimming.
            if (Time.time - swimSoundTimer > 2.0f && (Mathf.Abs(Input.GetAxis("Horizontal")) > 0 || Mathf.Abs(Input.GetAxis("Vertical")) > 0))
            {
                int soundToPlay = Random.Range(0, 4);

                if (splashSounds[soundToPlay] != null)
                {
                    splashSounds[soundToPlay].Play();
                }

                //Reset timer
                swimSoundTimer = Time.time;
            }

            //swimming
            if (Input.GetKeyDown(KeyCode.Space))
            {
 
                anim.SetBool("isGrounded", false);

                if (rigidbody2D.velocity.y < swimMaxSpeed)
                {
                    rigidbody2D.AddForce(new Vector2(0, swimJumpForce));
                }
                

                //Play Jumping sound. Added by Rebeca.
                if (jumpSound != null)
                {
                    jumpSound.Play();
                }
            }
            

        }

        
        //else not in water
        else
        {
         
          if (isGrounded && Input.GetKeyDown(KeyCode.Space))
          {

               anim.SetBool("isGrounded", false);
               rigidbody2D.AddForce(new Vector2(0, jumpForce));

              //Play Jumping sound. Added by Rebeca.
              if (jumpSound != null)
              {
                 jumpSound.Play();
              }
           }
        }
    }

}
