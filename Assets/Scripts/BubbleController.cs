using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BubbleController : MonoBehaviour
{
    public float horizontalSpeed = 10.0f;
    public string suffix = "P1_";


    private Bubble _bubble;
    private Car _car;


    private void Start()
    {
        _bubble = GetComponent<Bubble>();
        _car = GetComponent<Car>();
    }


    void FixedUpdate()
    {
        float horizontalValue = Input.GetAxis(suffix + "Horizontal");
        float verticalValue = Input.GetAxis(suffix + "Vertical");
        Control(horizontalValue, verticalValue);
    }


    private void Control(float horizontalValue, float verticalValue)
    {
        if (0.0f < Mathf.Abs(horizontalValue))
        {
            horizontalValue *= horizontalSpeed;
            Quaternion q = Quaternion.EulerAngles(0.0f, 0.0f, -90.0f);
            rigidbody2D.AddForce(q * _bubble.CurrentDirection * horizontalValue);
        }
        
        _car.ChangeRadiusTo(verticalValue);
    }


    public void Reset()
    {
        Control(0.0f, 0.0f);
    }

}