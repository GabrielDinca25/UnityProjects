using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //////////////////////////////////////
    /*Movement values*/
    public float jumpForce;
    public float moveSpeed;

    float pushForce = 800f;

    //////////////////////////////////////
    public Transform groundCheck;
    public LayerMask whatIsGround;

    //////////////////////////////////////
    Rigidbody2D rb2d;
    bool facingRight = true;
    bool grounded = false;
    float groundRadius = 0.2f;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);


        float move = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(move * moveSpeed, rb2d.velocity.y);
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
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb2d.AddForce(new Vector2(0, jumpForce));
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Boost")
        {
            rb2d.AddForce(new Vector2(pushForce * 10f * other.gameObject.transform.localScale.x, 0));
            Destroy(other.gameObject);
        }
    }
}
