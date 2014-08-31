using UnityEngine;
using System;
using System.Collections.Generic;

public class Jet : MonoBehaviour
{
    public Vector3 direction;
    public float force = 1;
    
    
    private void OnTriggerStay2D(Collider2D other)
    {
        Bubble bubble = other.GetComponent<Bubble>();
        if (bubble != null)
        {
            Vector3 value = transform.rotation * direction * Time.deltaTime * force * bubble.Size;
            Rigidbody2D body = bubble.GetComponent<Rigidbody2D>();
            body.AddForce(new Vector2(value.x, value.y));
        }
    }
    
}
