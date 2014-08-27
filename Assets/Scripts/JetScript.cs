using UnityEngine;
using System;
using System.Collections.Generic;

public class JetScript : MonoBehaviour
{
    public Vector3 direction;
    public float force = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(colliders.Count);
        foreach(Collider2D collider in colliders)
        {
            Rigidbody2D body = collider.GetComponent<Rigidbody2D>();
            CarScript car = collider.GetComponent<CarScript>();
            Vector3 value = (transform.rotation * direction) * Time.deltaTime * force * car.MovementRate;
            //Debug.Log(car.MovementRate);
            body.AddForce(new Vector2(value.x, value.y));
            //Debug.Log(value);
        }
    }

    private List<Collider2D> colliders = new List<Collider2D>();

    void OnTriggerEnter2D(Collider2D other)
    {
        if ("Cars" == other.tag && !colliders.Exists(p => p == other))
        {
            colliders.Add(other);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if ("Cars" == other.tag)
        {
            colliders.Remove(other);
        }
    }

    /*
    void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("1");
        if ("Cars" == other.tag)
        {
            //Debug.Log("2");
            Rigidbody2D body = other.GetComponent<Rigidbody2D>();
            CarScript car = other.GetComponent<CarScript>();
            Vector3 value = (transform.rotation * direction) * Time.deltaTime * force * car.MovementRate;
            body.AddForce(new Vector2(value.x, value.y));
            Debug.Log(value);
        }
    }
    */
}
