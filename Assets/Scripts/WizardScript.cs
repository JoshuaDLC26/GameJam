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

    SpriteRenderer spriteRenderer;

    [Header("Animation Arrays")]
    public Sprite[] jumpAnimations;
    public Sprite[] turningAnimations;
    public Sprite[] crouchAnimations;
    public Sprite[] rangeattackAnimations;
    public Sprite[] closeattackAnimations;
    public Sprite[] deathAnimations;

    [Header("Sounds")]

    public AudioClip slash;
    public AudioClip fire;
    public AudioClip turn;

    [Header("Animation FPS")]
    public float animationFPS;
    int currentFrame;
    float animationTimer;

    private Rigidbody2D playerBody;
    public float health = 200;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentFrame = 0;
        animationTimer = 1f / animationFPS;
    }

    // Update is called once per frame
    void Update()
    {

        playerMovement();
        playerSound();
        if (health <= 0)
        {
            deathAnimation();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
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
            rangeAttack = KeyCode.G;
            closeAttack = KeyCode.H;
        }

        if (Input.GetKey(up))
        {
            inputY = 1;
            animationLoop(jumpAnimations);
        }

        if (Input.GetKey(left))
        {
            inputX = -1;
            spriteRenderer.flipX = true;
            animationLoop(turningAnimations);

        }

        if (Input.GetKey(right))
        {
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
            gameObject.tag = "Attacking";
       
        }

        if (Input.GetKey(closeAttack))
        {
            animationLoop(closeattackAnimations);
            gameObject.tag = "Attacking";
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
            AudioSource.PlayClipAtPoint(slash, transform.position);
        }

        if (Input.GetKeyDown(left) || Input.GetKeyDown(right))
        {

            AudioSource.PlayClipAtPoint(turn, transform.position);

        }

        if (Input.GetKeyDown(rangeAttack))
        {
            AudioSource.PlayClipAtPoint(fire, transform.position);
        }

        if (Input.GetKey(closeAttack))
        {
            AudioSource.PlayClipAtPoint(slash, transform.position);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.otherCollider.CompareTag("Attacking"))
            {
                health -= 40;
            }
            if (collision.otherCollider.CompareTag("bottomBound"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

        }
    }
    public IEnumerator deathAnimation()
    {
        animationLoop(deathAnimations);
        yield return new WaitForSeconds(3f);
    }
}
