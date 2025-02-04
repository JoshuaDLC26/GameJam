using System.Runtime.CompilerServices;
using UnityEngine;
using System;
using static UnityEngine.UI.Image;
using UnityEngine.SceneManagement;
using System.Collections;

public class WizardScript : MonoBehaviour
{
    [Header("Player Speed")]
    public float speed = 14;

    [Header("Player Number")]
    public float playerNum = 1;
    public LayerMask hitLayers;
    public float xOffset;

    SpriteRenderer spriteRenderer;

    [Header("Animation Arrays")]
    public Sprite[] jumpAnimations;
    public Sprite[] turningAnimations;
    public Sprite[] crouchAnimations;
    public Sprite[] rangeattackAnimations;
    public Sprite[] closeattackAnimations;
    public Sprite[] deathAnimations;

    [Header("Sounds")]

    //public AudioClip slash;
    //public AudioClip fire;
    //public AudioClip turn;

    [Header("Animation FPS")]
    public float animationFPS;
    int currentFrame;
    float animationTimer;

    private string lastDirection;

    private Rigidbody2D playerBody;

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
        else {
            lastDirection = "left";
            spriteRenderer.flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        playerMovement();
        playerSound();
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
        KeyCode rangeAttack = KeyCode.L;
        KeyCode closeAttack = KeyCode.K;
       
        if (playerNum == 2)
        {
            up = KeyCode.W;
            right = KeyCode.D;
            left = KeyCode.A;
            down = KeyCode.S;
            rangeAttack = KeyCode.Q;
            closeAttack = KeyCode.E;
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

        if (Input.GetKey(rangeAttack))
        {
            animationLoop(rangeattackAnimations);
            Vector2 origin = new Vector2(transform.position.x, transform.position.y);
            Vector2 target = new Vector2(transform.position.x - 2, transform.position.y);
            Vector2 cool = target - origin;
            StartCoroutine(Wait());
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

        if (Input.GetKey(closeAttack))
        {
            xOffset = 2.0f;
            if (lastDirection == "left") {
                spriteRenderer.flipX = true;
                xOffset = -2.0f;
            }
            animationLoop(closeattackAnimations);
            


            Vector2 origin = new Vector2(transform.position.x, transform.position.y);
            Vector2 target = new Vector2(transform.position.x + xOffset, transform.position.y);
            Vector2 cool = target - origin;
            StartCoroutine(Wait());
            RaycastHit2D attackHit = Physics2D.Raycast(origin, cool, 1f, hitLayers);
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

        Vector2 direction = new Vector2(inputX, inputY);

        //in case you push multiple keys normalize
        if (direction.magnitude > 1)
        {
            direction.Normalize();
        }

        playerBody.linearVelocity = direction * speed;

    }

    private void playerSound()
    {
       
        KeyCode up = KeyCode.UpArrow;
        KeyCode right = KeyCode.RightArrow;
        KeyCode left = KeyCode.LeftArrow;
        KeyCode rangeAttack = KeyCode.L;
        KeyCode closeAttack = KeyCode.K;

        if (playerNum == 2)
        {
            up = KeyCode.W;
            right = KeyCode.D;
            left = KeyCode.A;
            rangeAttack = KeyCode.G;
            closeAttack = KeyCode.H;
        }

        if (Input.GetKeyDown(up))
        {
            //AudioSource.PlayClipAtPoint(slash, transform.position);
        }

        if (Input.GetKeyDown(left) || Input.GetKeyDown(right))
        {

            //AudioSource.PlayClipAtPoint(turn, transform.position);

        }

        if (Input.GetKeyDown(rangeAttack))
        {
            //AudioSource.PlayClipAtPoint(fire, transform.position);
        }

        if (Input.GetKey(closeAttack))
        {
            //AudioSource.PlayClipAtPoint(slash, transform.position);
        }
    }
    
    public IEnumerator deathAnimation()
    {
        animationLoop(deathAnimations);
        yield return new WaitForSeconds(3f);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
    }
}
