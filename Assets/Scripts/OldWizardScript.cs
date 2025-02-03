using UnityEngine;
using System;

public class OldWizardScript : MonoBehaviour
{

    // private bool isMoving = false;
    public float speed;
    private Rigidbody2D rb2d;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float inputX = 0;
    private float inputY = 0;

    private SpriteRenderer playerSpriteRenderer;
    public CollideCheck collideCheck;

    // public Sprite spriteRight;

    public Sprite spriteDown;

    public Sprite[] framesRight;
    public Sprite[] framesUp; //jumping animation
    public Sprite[] framesDown;

    public Sprite[] framesIdle; //defaulted right

    float frameTimerLeft, frameTimerRight, frameTimerUp, frameTimerDown, frameTimerIdle;

    int frameIndexLeft, frameIndexRight, frameIndexUp, frameIndexDown, frameIndexIdle;
    
    // float frameTimerLeft, frameTimerRight;
    float framesPerSecond = 10;

    private string lastDirection = "right"; //defaulted right, would later be determined by player


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        collideCheck = GetComponent<CollideCheck>();

        // Initialize all frame timers and indices
        frameTimerUp = frameTimerDown = frameTimerLeft = frameTimerRight = frameTimerIdle = (1f / framesPerSecond);
        frameIndexLeft = frameIndexRight = frameIndexUp = frameIndexDown = frameIndexIdle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("update");
        bool isMoving = false;

        // Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        inputY = 0;
        inputX = 0;
        playerSpriteRenderer.flipX = false;
       
        if (Input.GetKey(KeyCode.UpArrow)) {
            isMoving = true;
            frameTimerUp -= Time.deltaTime;
            if (frameTimerUp <= 0) {
                frameIndexUp++;
                if (frameIndexUp >= framesUp.Length) {
                    frameIndexUp = 0;
                }
                frameTimerUp = (1f / framesPerSecond);
                playerSpriteRenderer.sprite = framesUp[frameIndexUp];
            }
            if (lastDirection == "left") {
                playerSpriteRenderer.flipX = true;
            }
            inputY = 1;
        }
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow)) {
            isMoving = true;
            frameTimerUp -= Time.deltaTime;
            if (frameTimerUp <= 0) {
                frameIndexUp++;
                if (frameIndexUp >= framesUp.Length) {
                    frameIndexUp = 0;
                }
                frameTimerUp = (1f / framesPerSecond);
                playerSpriteRenderer.sprite = framesUp[frameIndexUp];
            }
            inputY = 1;
            inputX = 1;
            lastDirection = "right";
        }
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow)) {
            isMoving = true;
            frameTimerUp -= Time.deltaTime;
            if (frameTimerUp <= 0) {
                frameIndexUp++;
                if (frameIndexUp >= framesUp.Length) {
                    frameIndexUp = 0;
                }
                frameTimerUp = (1f / framesPerSecond);
                playerSpriteRenderer.sprite = framesUp[frameIndexUp];
            }
            inputY = 1;
            inputX = -1;
            lastDirection = "left";
            playerSpriteRenderer.flipX = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            isMoving = true;
            frameTimerLeft -= Time.deltaTime;
            if (frameTimerLeft <= 0) {
                frameIndexLeft++;
                if (frameIndexLeft >= framesRight.Length) {
                    frameIndexLeft = 0;
                }
                frameTimerLeft = (1f / framesPerSecond);
                playerSpriteRenderer.sprite = framesRight[frameIndexLeft];
            }
            playerSpriteRenderer.flipX = true;
            inputX = -1;
            lastDirection = "left";
        }
        if (Input.GetKey(KeyCode.DownArrow) && collideCheck.IsCollided) {
            isMoving = true;
            playerSpriteRenderer.sprite = spriteDown;
            if (lastDirection == "left") {
                playerSpriteRenderer.flipX = true;
            }
            inputY = -1;
        }
        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow) && collideCheck.IsCollided) {
            isMoving = true;
            playerSpriteRenderer.sprite = spriteDown;
            lastDirection = "right";
            inputY = -1;
            inputX = 1;
        }
        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow) && collideCheck.IsCollided) {
            isMoving = true;
            playerSpriteRenderer.sprite = spriteDown;
            playerSpriteRenderer.flipX = true;
            lastDirection = "left";
            inputY = -1;
            inputX = -1;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            isMoving = true;
            frameTimerDown -= Time.deltaTime;
            if (frameTimerDown <= 0) {
                frameIndexDown++;
                if (frameIndexDown >= framesDown.Length) {
                    frameIndexDown = 0;
                }
                frameTimerDown = (1f / framesPerSecond);
                playerSpriteRenderer.sprite = framesDown[frameIndexDown];
            }
            if (lastDirection == "left") {
                playerSpriteRenderer.flipX = true;
            }
            inputY = -1;
        }
        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow)) {
            isMoving = true;
            frameTimerDown -= Time.deltaTime;
            if (frameTimerDown <= 0) {
                frameIndexDown++;
                if (frameIndexDown >= framesDown.Length) {
                    frameIndexDown = 0;
                }
                frameTimerDown = (1f / framesPerSecond);
                playerSpriteRenderer.sprite = framesDown[frameIndexDown];
            }
            lastDirection = "right";
            inputY = -1;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            isMoving = true;
            frameTimerDown -= Time.deltaTime;
            if (frameTimerDown <= 0) {
                frameIndexDown++;
                if (frameIndexDown >= framesDown.Length) {
                    frameIndexDown = 0;
                }
                frameTimerDown = (1f / framesPerSecond);
                playerSpriteRenderer.sprite = framesDown[frameIndexDown];
            }
            playerSpriteRenderer.flipX = true;
            lastDirection = "left";
            inputY = -1;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            isMoving = true;
            frameTimerRight -= Time.deltaTime;
            if (frameTimerRight <= 0) {
                frameIndexRight++;
                if (frameIndexRight >= framesRight.Length) {
                    frameIndexRight = 0;
                }
                frameTimerRight = (1f / framesPerSecond);
                playerSpriteRenderer.sprite = framesRight[frameIndexRight];
            }
            inputX = 1;
            lastDirection = "right";
        }

        //only use static sprites if we're not moving
        if (!isMoving) {
            if (rb2d.linearVelocity.y < 0) {
                frameTimerDown -= Time.deltaTime;
                if (frameTimerDown <= 0) {
                    frameIndexDown++;
                    if (frameIndexDown >= framesDown.Length) {
                        frameIndexDown = 0;
                    }
                    frameTimerDown = (1f / framesPerSecond);
                    playerSpriteRenderer.sprite = framesDown[frameIndexDown];
                    // Debug.Log("Current frame index: " + frameIndexDown);
                    // Debug.Log("Direction: " + "down");
                    // Debug.Log("Sprite: " + playerSpriteRenderer.sprite);
                }
                if (lastDirection == "left") {
                    playerSpriteRenderer.flipX = true;
                }
            }
            else if (rb2d.linearVelocity.y > 0) {
                frameTimerUp -= Time.deltaTime;
                if (frameTimerUp <= 0) {
                    frameIndexUp++;
                    if (frameIndexUp >= framesUp.Length) {
                        frameIndexUp = 0;
                    }
                    frameTimerUp = (1f / framesPerSecond);
                    playerSpriteRenderer.sprite = framesUp[frameIndexUp];
                    // Debug.Log("Current frame index: " + frameIndexDown);
                    // Debug.Log("Direction: " + "down");
                    // Debug.Log("Sprite: " + playerSpriteRenderer.sprite);
                }
                if (lastDirection == "left") {
                    playerSpriteRenderer.flipX = true;
                }
            }
            else if (rb2d.linearVelocity.y == 0) {
                frameTimerIdle -= Time.deltaTime;
                if (frameTimerIdle <= 0) {
                    frameIndexIdle++;
                    if (frameIndexIdle >= framesIdle.Length) {
                        frameIndexIdle = 0;
                    }
                    frameTimerUp = (1f / framesPerSecond);
                    playerSpriteRenderer.sprite = framesIdle[frameIndexIdle];
                    // Debug.Log("Current frame index: " + frameIndexDown);
                    // Debug.Log("Direction: " + "down");
                    // Debug.Log("Sprite: " + playerSpriteRenderer.sprite);
                }
                if (lastDirection == "left") {
                    playerSpriteRenderer.flipX = true;
                }
            }
        }

        // if (input.magnitude > 1.0f) {
        //     input.Normalize();
        // }
        rb2d.linearVelocity = new Vector2(inputX, inputY) * speed;

        // if (Input.GetKeyDown(KeyCode.RightShift) && (lastTimeFired + 1 / rateOfBomb) < Time.time) {
        //     lastTimeFired = Time.time;
        //     SpawnBomb();
        // }

}
}