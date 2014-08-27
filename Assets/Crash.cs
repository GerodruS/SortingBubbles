using UnityEngine;
using System.Collections;

public class Crash : MonoBehaviour
{
    public float strength = 1.0f;

    private CarScript car;


    // Use this for initialization
    void Start()
    {
        car = GetComponent<CarScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (strength * car.Strength < coll.relativeVelocity.magnitude)
        {
            Debug.Log("BOOM");
        }
        else
        {
            Debug.Log("no");
        }
    }
}
