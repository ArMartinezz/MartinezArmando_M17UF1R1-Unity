using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pain : MonoBehaviour
{
    public Rigidbody2D body;
    public GameObject parent;
    public enum Direction {Vertical, Horizontal};
    public Direction direction;
    public float speed;
    public float limit;

    // Update is called once per frame
    void FixedUpdate()
    {
        body.velocity = direction == Direction.Horizontal ? new Vector2(speed, 0) : new Vector2(0, speed);


            if (direction == Direction.Horizontal && transform.position.x > limit
            || direction == Direction.Vertical && transform.position.y > limit) 
            {
                NewPain();
                Destroy(gameObject);
            }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMovement>().Die();
        }
    }

    // new Pain with same characteristics
    void NewPain()
    {

        var newPain = Instantiate(transform, parent.transform);
        newPain.transform.position = parent.transform.position;
    }
}
