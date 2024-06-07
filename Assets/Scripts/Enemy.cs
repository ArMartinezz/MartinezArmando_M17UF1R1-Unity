using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D body;
    public FloorCheck floorCheck;
    public Collider2D playerCollider;
    private bool facingRight = true;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        floorCheck.noGround += ChangeSide;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        body.velocity = new Vector2(facingRight ? speed : -speed, 0);
    }

    public void ChangeSide()
    {
        Vector2 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        facingRight = !facingRight;
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMovement>().Die();
        }
    }
}
