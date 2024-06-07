using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCheck : MonoBehaviour
{
    public delegate void NoGround();
    public NoGround noGround;
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.name == "Tilemap") 
        {
            noGround?.Invoke();
        }
    }

}
