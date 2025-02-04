using System.Runtime.CompilerServices;
using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class LightningWizard : MonoBehaviour
{
    [Header("Player Speed")]
    public float speed = 14;

    [Header("Player Number")]
    public float playerNum = 1;

    public SpriteRenderer spriteRenderer;
    public LayerMask hitLayers;

    [Header("Animation Arrays")]
    public Sprite[] jumpAnimations;
    public Sprite[] turningAnimations;
    public Sprite[] crouchAnimations;
    public Sprite[] attackAnimations;
    public Sprite[] deathAnimations;

    public float xOffset;

    [Header("Animation FPS")]
    public float animationFPS;
    int currentFrame;
    float animationTimer;

    private Rigidbody2D playerBody;

    private string lastDirection;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentFrame = 0;
        animationTimer = 1f / animationFPS;

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
       

        playerMovement();


    }

    private void animationLoop(Sprite[] animationArray)
    {
        animationTimer -= Time.deltaTime;
        if (animationTimer < 0)
        {
            animationTimer = 1f / animationFPS;
            currentFrame++;
            if (currentFrame >= animationArray.Length)
            {
                currentFrame = 0;

            }
            spriteRenderer.sprite = animationArray[currentFrame];
        }

    }

    private void playerMovement()
    {

        float inputX = 0;
        float inputY = 0;

        KeyCode up = KeyCode.UpArrow;
        KeyCode right = KeyCode.RightArrow;
        KeyCode left = KeyCode.LeftArrow;
        KeyCode down = KeyCode.DownArrow;
        KeyCode attack = KeyCode.L;


        if (playerNum == 2)
        {
            up = KeyCode.W;
            right = KeyCode.D;
            left = KeyCode.A;
            down = KeyCode.S;
            attack = KeyCode.E;
        }

        if (Input.GetKey(up))
        {
            inputY = 1;
            animationLoop(jumpAnimations);
        }

        if (Input.GetKey(left))
        {
            lastDirection = "left";
            inputX = -1;
            spriteRenderer.flipX = true;
            animationLoop(turningAnimations);

        }

        if (Input.GetKey(right))
        {
            lastDirection = "right";
            inputX = 1;
            spriteRenderer.flipX = false;
            animationLoop(turningAnimations);
        }

        if (Input.GetKey(down))
        {
            inputY = -1;
            animationLoop(crouchAnimations);
        }

        if (Input.GetKey(attack))
        {
            xOffset = 2.0f;
            if (lastDirection == "left")
            {
                spriteRenderer.flipX = true;
                xOffset = -2.0f;
            }
            animationLoop(attackAnimations);



            Vector2 origin = new Vector2(transform.position.x, transform.position.y);
            Vector2 target = new Vector2(transform.position.x + xOffset, transform.position.y);
            Vector2 cool = target - origin;
            StartCoroutine(Wait());
            RaycastHit2D attackHit = Physics2D.Raycast(origin, cool, 2f, hitLayers);
            if (attackHit.collider != null)
            {
                Character ch = attackHit.collider.GetComponent<Character>();
                Debug.Log("Hit!!!");
                if ( ch != null)
                {
                    ch.health -= 40;
                }
            }
        }

        Vector2 direction = new Vector2(inputX, inputY);

        //in case you push multiple keys normalize
        if (direction.magnitude > 1)
        {
            direction.Normalize();
        }

        playerBody.linearVelocity = direction * speed;

    }
    public IEnumerator deathAnimation() {
        animationLoop(deathAnimations);
        yield return new WaitForSeconds(3f);
    }

  

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
    }
}