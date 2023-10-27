using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 boxSize;
    public float gravity;
    public float speed;
    private float hInput;
    // Components
    public Rigidbody2D body;
    public Collider2D playerCollider;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        gravity = -9.8f;
        speed = 5f;
        body = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {   
        // Movement
        hInput = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(hInput * speed, IsGrounded() ? 0.0f : gravity);

        if (Input.GetKeyDown(KeyCode.V) && IsGrounded()){ ChangeGravity(); }
    }

    private bool IsGrounded() 
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(playerCollider.bounds.center, new Vector3(playerCollider.bounds.size.x, playerCollider.bounds.size.y, playerCollider.bounds.size.z), 0f, Vector2.up, 0.01f, groundLayer);
        print(raycastHit.collider != null ? raycastHit.collider : "") ;
        return raycastHit.collider != null;
    }

    public void ChangeGravity()
    {
        gravity = -gravity;
        transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
    }

}
