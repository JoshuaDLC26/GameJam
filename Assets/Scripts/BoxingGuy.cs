using UnityEngine;
using System;

public class BoxingGuy : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb2d;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float inputX = 0;
    private float inputY = 0;

    private SpriteRenderer playerSpriteRenderer;

    // public Sprite spriteUp;
    // public Sprite spriteDown;
    // public Sprite spriteLeft;
    public Sprite spriteRight;

    public Sprite spriteDown;

    public Sprite midAir;

    // public Sprite[] framesWalkRight;
    // public Sprite[] framesUp;
    // public Sprite[] framesDown;

    // float frameTimerLeft, frameTimerRight;
    // float framesPerSecond = 10;

    int frameIndexLeft, frameIndexRight;

    private string lastDirection = "right"; //defaulted right, would later be determined by player


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();

        // Initialize all frame timers and indices
        // frameTimerUp = frameTimerDown = frameTimerLeft = frameTimerRight = (1f / framesPerSecond);
        // frameIndexLeft = frameIndexRight = frameIndexLeft = frameIndexRight = 0;
    }

    // Update is called once per frame
    void Update()
    {

        bool isMoving = false;

        // Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        inputY = 0;
        inputX = 0;
        playerSpriteRenderer.flipX = false;

        if (Input.GetKey(KeyCode.UpArrow)) {
            isMoving = true;
            // frameTimerUp -= Time.deltaTime;
            // if (frameTimerUp <= 0) {
            //     frameIndexUp++;
            //     if (frameIndexUp >= framesUp.Length) {
            //         frameIndexUp = 0;
            //     }
            //     frameTimerUp = (1f / framesPerSecond);
            //     playerSpriteRenderer.sprite = framesUp[frameIndexUp];
                // Debug.Log("Current frame index: " + frameIndexUp);
                // Debug.Log("Direction: " + "up");
                // Debug.Log("Sprite: " + playerSpriteRenderer.sprite);
            while (rb2d.linearVelocity.y > 0) {
                playerSpriteRenderer.sprite = midAir;
            }
            inputY = 1;
            // lastDirection = "up";
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            isMoving = true;
            // frameTimerLeft -= Time.deltaTime;
            // if (frameTimerLeft <= 0) {
            //     frameIndexLeft++;
            //     if (frameIndexLeft >= framesWalkRight.Length) {
            //         frameIndexLeft = 0;
            //     }
            //     frameTimerLeft = (1f / framesPerSecond);
            //     playerSpriteRenderer.sprite = framesWalkRight[frameIndexLeft];
            //     // Debug.Log("Current frame index: " + frameIndexLeft);
            //     // Debug.Log("Direction: " + "left");
            //     // Debug.Log("Sprite: " + playerSpriteRenderer.sprite);
            // }
            playerSpriteRenderer.sprite = spriteRight;
            playerSpriteRenderer.flipX = true;
            inputX = -1;
            lastDirection = "left";
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            isMoving = true;
            // frameTimerDown -= Time.deltaTime;
            // if (frameTimerDown <= 0) {
            //     frameIndexDown++;
            //     if (frameIndexDown >= framesDown.Length) {
            //         frameIndexDown = 0;
            //     }
            //     frameTimerDown = (1f / framesPerSecond);
            //     playerSpriteRenderer.sprite = framesDown[frameIndexDown];
            //     // Debug.Log("Current frame index: " + frameIndexDown);
            //     // Debug.Log("Direction: " + "down");
            //     // Debug.Log("Sprite: " + playerSpriteRenderer.sprite);
            // }
            // lastDirection = "down";
            playerSpriteRenderer.sprite = spriteDown;
            inputY = -1;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            isMoving = true;
            // frameTimerRight -= Time.deltaTime;
            // if (frameTimerRight <= 0) {
            //     frameIndexRight++;
            //     if (frameIndexRight >= framesWalkRight.Length) {
            //         frameIndexRight = 0;
            //     }
            //     frameTimerRight = (1f / framesPerSecond);
                // playerSpriteRenderer.sprite = framesWalkRight[frameIndexRight];
                // Debug.Log("Direction: " + "right");
                // Debug.Log("Current frame index: " + frameIndexRight);
            // }
            playerSpriteRenderer.sprite = spriteRight;
            inputX = 1;
            lastDirection = "right";
        }

        // Only use static sprites if we're not moving
        if (!isMoving) {
            // if (lastDirection == "down") {
            //     playerSpriteRenderer.sprite = spriteDown;
            // }
            // else if (lastDirection == "up") {
            //     playerSpriteRenderer.sprite = spriteUp;
            // }
            if (lastDirection == "right") {
                playerSpriteRenderer.sprite = spriteRight;
            }
            else if (lastDirection == "left") {
                playerSpriteRenderer.sprite = spriteRight;
                playerSpriteRenderer.flipX = true;
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

