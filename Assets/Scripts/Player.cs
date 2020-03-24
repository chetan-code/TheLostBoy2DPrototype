using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour {

    public static Player instance;

    public float maxJumpHeight = 6;
    public float minJumpHeight = 2;
    public float timeToApex = 0.4f;//time taken to reach max-height of a jump
    float accelerationTimeAirborne = 0.2f;
    float accelerationTimeGrounded = 0.1f;
    public float moveSpeed = 10;


    public Vector2 wallJumpClimb;
    public Vector2 wallJumpoff;
    public Vector2 wallLeap;

    public float wallSlideSpeedMax = 3;
    public float wallStickTime = 0.25f;

    [HideInInspector]
    public bool isDead = false;

    private float timeToWallUnstick;
    private float gravity;
    private float maxJumpVelocity;
    private float minJumpVelocity;
    [HideInInspector]
    public  Vector3 velocity;
    private float velocityXSmoothing;

    [HideInInspector]
    public Controller2D controller;
    [SerializeField]
    private Animator anim;
    private bool wallSliding;
    private int wallDirX;

    [HideInInspector]
    public Vector2 directionalInput;



    [HideInInspector]
    public bool lookingRight = true;
    [HideInInspector]
    public Vector2 resetStartPos;
    [HideInInspector]
    public Vector2 resetEndPos;
    [HideInInspector]
    public float resetStartTime;
    public PlayerFx playerFx;
    public PlayerSound playerSound;
    private float fracJourney;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("More than one instaance of Player. Deleting Player Object");
            Destroy(gameObject);
        }
    }


    // Use this for initialization
    void Start() {

        Application.targetFrameRate = 60;

        controller = GetComponent<Controller2D>();
        //anim = GetComponent<Animator>();


        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
        print("Gravity: " + gravity + " Jump Velocity: " + maxJumpVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            CalculateVelocity();
            HandleWallSliding();
            //anim.SetFloat("velocityX", Mathf.Abs(velocity.x));
            if ((velocity.x > 0 && !lookingRight) || (velocity.x < 0 && lookingRight))
            {
                Flip();
                playerFx.InstantiateFx(0, transform.position, Quaternion.identity);
            }
            controller.Move(velocity * Time.deltaTime, directionalInput);
           

            if (controller.collisions.above || controller.collisions.below)
            {
                velocity.y = 0;
                anim.SetBool("Jump", false);
            }

        }


    }


    public void SetDirectionalInput(Vector2 input) {
        directionalInput = input;
    }

    public void OnJumpInputDown() {
        if (wallSliding)
        {
            if (wallDirX == directionalInput.x)
            {
                velocity.x = -wallDirX * wallJumpClimb.x;
                velocity.y = wallJumpClimb.y;
            }
            else if (directionalInput.x == 0)
            {
                velocity.x = -wallDirX * wallJumpoff.x;
                velocity.y = wallJumpoff.y;
            }
            else
            {
                velocity.x = -wallDirX * wallLeap.x;
                velocity.y = wallLeap.y;
            }
            JumpImapct();
        }
        if (controller.collisions.below)
        {
            velocity.y = maxJumpVelocity;
            //anim.SetFloat("velocityY", Mathf.Abs(velocity.y));
            anim.SetBool("Jump", true);
            JumpImapct();
        }
    }

    private void JumpImapct()
    {
        playerFx.InstantiateFx(0, transform.position, Quaternion.identity);
        PlayerSound.instance.PlayAShot(PlayerSound.instance.jumpSound);
    }

    private void WalkParticles()
    {

    }

    public void OnJumpInputUp() {
        if (velocity.y > minJumpVelocity)
        {
            velocity.y = minJumpVelocity;
        }
    }

    public void ChangePlayerPosition() {
        transform.position = new Vector2(resetEndPos.x,resetEndPos.y + 10f);
        isDead = false;
    }
	

    void HandleWallSliding() {
        wallDirX = (controller.collisions.left) ? -1 : 1;

        wallSliding = false;
        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        {
            wallSliding = true;

            if (velocity.y < -wallSlideSpeedMax)
            {
                velocity.y = -wallSlideSpeedMax;
            }

            if (timeToWallUnstick > 0)
            {
                velocityXSmoothing = 0;
                velocity.x = 0;

                if (directionalInput.x != wallDirX && directionalInput.x != 0)
                {
                    timeToWallUnstick -= Time.deltaTime;
                }
                else
                {
                    timeToWallUnstick = wallStickTime;
                }
            }
            else
            {
                timeToWallUnstick = wallStickTime;
            }
        }

    }

    public void Flip()
    {
        lookingRight = !lookingRight;
        Vector3 myScale = transform.localScale;
        myScale.x *= -1;
        transform.localScale = myScale;
    }

    void CalculateVelocity()
    {
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
    }

}
