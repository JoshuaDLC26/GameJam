using UnityEngine;
using System;
using System.Collections;

public class OldWizardScript : MonoBehaviour
{
    // private bool isMoving = false;
    public float speed;
    private Rigidbody2D rb2d;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float inputX = 0;
    private float inputY = 0;

    public float xOffset;

    private SpriteRenderer playerSpriteRenderer;
    public CollideCheck collideCheck;

    public LayerMask hitLayers;

    public float playerNum = 1;

    // public Sprite spriteRight;

    public Sprite spriteDown;

    public Sprite[] framesRight;
    public Sprite[] framesUp; //jumping animation
    public Sprite[] framesDown;

    public Sprite[] framesIdle; //defaulted right

    public Sprite[] framesAttack2;

    public Sprite[] deathAnimations;

    public Sprite[] framesAttack1;

    float frameTimerLeft, frameTimerRight, frameTimerUp, frameTimerDown, frameTimerIdle, frameTimerAttack2, frameTimerAttack1, frameTimerDeath;

    int frameIndexLeft, frameIndexRight, frameIndexUp, frameIndexDown, frameIndexIdle, frameIndexAttack2, frameIndexAttack1, frameIndexDeath;

    // float frameTimerLeft, frameTimerRight;
    float framesPerSecond = 10;

    private string lastDirection = "right"; //defaulted right, would later be determined by player

    float allowJumpFor;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        collideCheck = GetComponent<CollideCheck>();

        frameTimerUp = frameTimerDown = frameTimerLeft = frameTimerRight = frameTimerIdle = frameTimerAttack2 = frameTimerDeath = frameTimerAttack1 = (1f / framesPerSecond);
        frameIndexLeft = frameIndexRight = frameIndexUp = frameIndexDown = frameIndexIdle = frameIndexAttack2 = frameIndexDeath = frameIndexAttack1 = 0;

        // allowJumpFor = 0.2f;
        if (playerNum == 1)
        {
            lastDirection = "right";
        }
        else
        {
            lastDirection = "left";
        }
    }

    // Update is called once per frame
    void Update()
    {

        KeyCode up = KeyCode.UpArrow;
        KeyCode right = KeyCode.RightArrow;
        KeyCode left = KeyCode.LeftArrow;
        KeyCode down = KeyCode.DownArrow;
        KeyCode attack1 = KeyCode.Space;
        KeyCode attack2 = KeyCode.RightShift;

        if (playerNum == 2)
        {
            up = KeyCode.W;
            right = KeyCode.D;
            left = KeyCode.A;
            down = KeyCode.S;
            attack2 = KeyCode.E;
            attack1 = KeyCode.Q;
        }

        bool isMoving = false;

        inputY = 0;
        inputX = 0;
        playerSpriteRenderer.flipX = false;

        // if (collideCheck.IsCollided) {
        //     Debug.Log("collided");
        //     allowJumpFor = 0.2f;
        //     Debug.Log("allowJumpFor: " + allowJumpFor);
        // }

        //prioritize up/down over left/right
        // && allowJumpFor > 0
        if (Input.GetKey(up))
        {
            // allowJumpFor -= Time.deltaTime;
            isMoving = true;
            frameTimerUp -= Time.deltaTime;
            if (frameTimerUp <= 0)
            {
                frameIndexUp++;
                if (frameIndexUp >= framesUp.Length)
                {
                    frameIndexUp = 0;
                }
                frameTimerUp = (1f / framesPerSecond);
                playerSpriteRenderer.sprite = framesUp[frameIndexUp];
            }
            inputY = 1;

            if (Input.GetKey(KeyCode.RightArrow))
            {
                inputX = 1;
                lastDirection = "right";
            }
            else if (Input.GetKey(left))
            {
                inputX = -1;
                lastDirection = "left";
                playerSpriteRenderer.flipX = true;
            }
        }

        else if (Input.GetKey(down))
        {
            isMoving = true;
            if (collideCheck.IsCollided)
            {
                playerSpriteRenderer.sprite = spriteDown;
            }
            else
            {
                frameTimerDown -= Time.deltaTime;
                if (frameTimerDown <= 0)
                {
                    frameIndexDown++;
                    if (frameIndexDown >= framesDown.Length)
                    {
                        frameIndexDown = 0;
                    }
                    frameTimerDown = (1f / framesPerSecond);
                    playerSpriteRenderer.sprite = framesDown[frameIndexDown];
                }
            }
            inputY = -1;

            if (Input.GetKey(right))
            {
                inputX = 1;
                lastDirection = "right";
            }
            else if (Input.GetKey(left))
            {
                inputX = -1;
                lastDirection = "left";
                playerSpriteRenderer.flipX = true;
            }
        }

        else if (Input.GetKey(left))
        {
            isMoving = true;
            frameTimerLeft -= Time.deltaTime;
            if (frameTimerLeft <= 0)
            {
                frameIndexLeft++;
                if (frameIndexLeft >= framesRight.Length)
                {
                    frameIndexLeft = 0;
                }
                frameTimerLeft = (1f / framesPerSecond);
                playerSpriteRenderer.sprite = framesRight[frameIndexLeft];
            }
            playerSpriteRenderer.flipX = true;
            lastDirection = "left";
            inputX = -1;
        }

        else if (Input.GetKey(right))
        {
            isMoving = true;
            frameTimerRight -= Time.deltaTime;
            if (frameTimerRight <= 0)
            {
                frameIndexRight++;
                if (frameIndexRight >= framesRight.Length)
                {
                    frameIndexRight = 0;
                }
                frameTimerRight = (1f / framesPerSecond);
                playerSpriteRenderer.sprite = framesRight[frameIndexRight];
            }
            inputX = 1;
            lastDirection = "right";
        }

        else if (Input.GetKey(attack1))
        {
            isMoving = true;
            frameTimerAttack1 -= Time.deltaTime;
            if (frameTimerAttack1 <= 0)
            {
                frameIndexAttack1++;
                if (frameIndexAttack1 >= framesAttack1.Length)
                {
                    frameIndexAttack1 = 0;
                }
                frameTimerAttack1 = (1f / framesPerSecond);
                playerSpriteRenderer.sprite = framesAttack1[frameIndexAttack1];
            }

            if (lastDirection == "left")
            {
                playerSpriteRenderer.flipX = true;
            }

            Vector2 origin = new Vector2(transform.position.x, transform.position.y);
            Vector2 target = new Vector2(transform.position.x + 1, transform.position.y);
            Vector2 cool = target - origin;
            Wait();
            RaycastHit2D attackHit = Physics2D.Raycast(origin, cool, 2f, hitLayers);
            if (attackHit.collider != null)
            {
                Character ch = attackHit.collider.GetComponent<Character>();
                Debug.Log("Hit!!!");
                if (ch != null)
                {
                    ch.health -= 40;
                }
            }
        }

        else if (Input.GetKey(attack2))
        {
            isMoving = true;
            frameTimerAttack2 -= Time.deltaTime;
            if (frameTimerAttack2 <= 0)
            {
                frameIndexAttack2++;
                if (frameIndexAttack2 >= framesAttack2.Length)
                {
                    frameIndexAttack2 = 0;
                }
                frameTimerAttack2 = (1f / framesPerSecond);
                playerSpriteRenderer.sprite = framesAttack2[frameIndexAttack2];
            }

            if (lastDirection == "left")
            {
                playerSpriteRenderer.flipX = true;
            }

            Vector2 origin = new Vector2(transform.position.x, transform.position.y);
            Vector2 target = new Vector2(transform.position.x + 2, transform.position.y);
            Vector2 cool = target - origin;
            Wait();
            RaycastHit2D attackHit = Physics2D.Raycast(origin, cool, 2f, hitLayers);
            if (attackHit.collider != null)
            {
                Character ch = attackHit.collider.GetComponent<Character>();
                Debug.Log("Hit!!!");
                if (ch != null)
                {
                    ch.health -= 40;
                }
            }
        }

        //idle animation
        if (!isMoving)
        {
            if (rb2d.linearVelocity.y < 0)
            {
                frameTimerDown -= Time.deltaTime;
                if (frameTimerDown <= 0)
                {
                    frameIndexDown++;
                    if (frameIndexDown >= framesDown.Length)
                    {
                        frameIndexDown = 0;
                    }
                    frameTimerDown = (1f / framesPerSecond);
                    playerSpriteRenderer.sprite = framesDown[frameIndexDown];
                }
                if (lastDirection == "left")
                {
                    playerSpriteRenderer.flipX = true;
                }
            }
            else if (rb2d.linearVelocity.y > 0)
            {
                frameTimerUp -= Time.deltaTime;
                if (frameTimerUp <= 0)
                {
                    frameIndexUp++;
                    if (frameIndexUp >= framesUp.Length)
                    {
                        frameIndexUp = 0;
                    }
                    frameTimerUp = (1f / framesPerSecond);
                    playerSpriteRenderer.sprite = framesUp[frameIndexUp];
                }
                if (lastDirection == "left")
                {
                    playerSpriteRenderer.flipX = true;
                }
            }
            else if (rb2d.linearVelocity.y == 0)
            {
                frameTimerIdle -= Time.deltaTime;
                if (frameTimerIdle <= 0)
                {
                    frameIndexIdle++;
                    if (frameIndexIdle >= framesIdle.Length)
                    {
                        frameIndexIdle = 0;
                    }
                    frameTimerUp = (1f / framesPerSecond);
                    playerSpriteRenderer.sprite = framesIdle[frameIndexIdle];
                }
                if (lastDirection == "left")
                {
                    playerSpriteRenderer.flipX = true;
                }
            }
        }
        rb2d.linearVelocity = new Vector2(inputX, inputY) * speed;
    }


    public IEnumerator deathAnimation()
    {
        frameTimerDeath -= Time.deltaTime;
        if (frameTimerDeath <= 0)
        {
            frameIndexDeath++;
            if (frameIndexDeath >= deathAnimations.Length)
            {
                frameIndexDeath = 0;
            }
            frameTimerDeath = (1f / framesPerSecond);
            playerSpriteRenderer.sprite = deathAnimations[frameIndexDeath];
        }
        yield return new WaitForSeconds(1f);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
    }
    // private void animationLoop(Sprite[] animationArray)
    // {
    //     //animationTimer -= Time.deltaTime;
    //     //if (animationTimer < 0)
    //     //{
    //     //    animationTimer = 1f / animationFPS;
    //     //    currentFrame++;
    //     //    if (currentFrame >= animationArray.Length)
    //     //    {
    //     //        currentFrame = 0;

    //     //    }
    //     //    spriteRenderer.sprite = animationArray[currentFrame];
    //     //}

    // }

    // public IEnumerator deathAnimation()
    // {
    //     animationLoop(deathAnimations);
    //     yield return new WaitForSeconds(1f);
    // }
}