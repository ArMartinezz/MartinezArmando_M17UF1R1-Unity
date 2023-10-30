using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float gravity;
    public bool gravityUp;
    public float speed;
    private float hInput;
    public bool grounded;

    // Components
    public Rigidbody2D body;
    public Collider2D playerCollider;
    public LayerMask groundLayer;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        gravity = -9.8f;
        speed = 5f;
        animator = GetComponent<Animator>();
    }


    void Update(){
        grounded = IsGrounded();

        if (Input.GetKeyDown(KeyCode.V) && grounded)
        {
            ChangeGravity();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {   

        // Movement
        hInput = Input.GetAxis("Horizontal");

        if (hInput != 0) {animator.SetBool("isWalking", true); } else {animator.SetBool("isWalking", false); }

        body.velocity = new Vector2(hInput * speed, grounded ? 0.0f : gravity);
    }

    private bool IsGrounded() 
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, gravityUp ? Vector2.down : Vector2.up, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    public void ChangeGravity()
    {
        gravity = -gravity;
        Vector2 Scaler = transform.localScale;
        Scaler.y *= -1;
        transform.localScale = Scaler;
        gravityUp = !gravityUp;
    }

}
