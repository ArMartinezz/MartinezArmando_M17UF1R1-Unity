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
    private bool dead;
    private bool facingRight;
    private float dyingGravity;
    public Vector2 spawnPoint;

    // Components
    public Rigidbody2D body;
    public Collider2D playerCollider;
    public LayerMask groundLayer;
    private Animator animator;
    private SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        gravity = -9.8f;
        dead = false;
        animator = GetComponent<Animator>();
        dyingGravity = 2.0f;
        spawnPoint = transform.position;
        soundManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundManager>();
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

        if (hInput > 0 && facingRight || hInput < 0 && !facingRight) 
        {
            Vector2 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
            facingRight = !facingRight;
        }

        if (!dead){
            body.velocity = new Vector2(hInput * speed, grounded ? 0.0f : gravity);
        } else {
            body.velocity = new Vector2(2.0f, dyingGravity);
            if (dyingGravity > -9.8f) { dyingGravity -= 0.2f; }
        }

        if (transform.position.y < -5.5f) { Respawn(); }
    }

    private bool IsGrounded() 
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, gravityUp ? Vector2.down : Vector2.up, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    public void ChangeGravity()
    {
        soundManager.PlaySFX("Bwomp");
        gravity = -gravity;
        Vector2 Scaler = transform.localScale;
        Scaler.y *= -1;
        transform.localScale = Scaler;
        gravityUp = !gravityUp;
    }

    public void Die()
    {
        soundManager.PlaySFX("Dolor");
        dead = true;
        playerCollider.enabled = false;
        if (gravity > 0.0f)
        {
            Vector2 Scaler = transform.localScale;
            Scaler.y *= -1;
            transform.localScale = Scaler;
            gravityUp = !gravityUp;
            gravity *= -1;
        }
    }

    public void Respawn()
    {
        dead = false;
        playerCollider.enabled = true;
        transform.position = spawnPoint;
        dyingGravity = 2.0f;
    }

    public void ResetSpawnpoint()
    {
        spawnPoint = (Vector2)transform.position;
    }
}
