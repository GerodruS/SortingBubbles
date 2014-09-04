using UnityEngine;
using System.Collections;

public class BubbleController : MonoBehaviour
{
    public float horizontalSpeed = 10.0f;
    public string suffix = "P1_";

    private Car _car;

    void Start()
    {
        _car = GetComponent<Car>();
    }

    void FixedUpdate()
    {
        float horizontalValue = Input.GetAxis(suffix + "Horizontal");
        float verticalValue = Input.GetAxis(suffix + "Vertical");
        Control(horizontalValue, verticalValue);
    }


    void Control(float horizontalValue, float verticalValue)
    {
        //Debug.Log(verticalValue);
        if (0.0f < Mathf.Abs(horizontalValue))
        {
            horizontalValue *= horizontalSpeed;
            horizontalValue *= Time.deltaTime;
            rigidbody2D.AddForce(transform.rotation * new Vector2(horizontalValue, 0));
        }
        
        _car.ChangeRadiusTo(verticalValue);
    }


    public void Reset()
    {
        Control(0.0f, 0.0f);
    }

}