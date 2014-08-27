using UnityEngine;
using System.Collections;

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

    }


    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("1");
        if ("Cars" == other.tag)
        {
            Debug.Log("2");
            Rigidbody2D body = other.GetComponent<Rigidbody2D>();
            CarScript car = other.GetComponent<CarScript>();
            Vector3 value = (transform.rotation * direction) * Time.deltaTime * force * car.MovementRate;
            body.AddForce(new Vector2(value.x, value.y));
            Debug.Log(value);
        }
    }
}
