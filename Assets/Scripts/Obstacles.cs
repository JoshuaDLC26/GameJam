using UnityEngine;

public class Obstacles : MonoBehaviour
{
    BoxCollider2D obstacle;
    bool playerComingThrough = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        obstacle = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerComingThrough) {
            obstacle.isTrigger = false;
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player")) {
    //        Rigidbody2D playerVel = collision.gameObject.GetComponent<Rigidbody2D>();
    //        Transform playerLoc = collision.gameObject.GetComponent<Transform>();
    //        if (((playerVel.linearVelocityY + playerVel.gravityScale) > 0 && (playerLoc.position.y < transform.position.y)) ||
    //            ((playerVel.linearVelocityY + playerVel.gravityScale) < 0 && playerLoc.position.y > transform.position.y))
    //        {
    //            obstacle.isTrigger = true;
    //            playerComingThrough=true;
    //        }
    //        else { 
    //            obstacle.isTrigger = false;
    //            playerComingThrough=false;
    //        }
    //    }
    //}
}
