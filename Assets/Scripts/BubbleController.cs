using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Eppy;

public class BubbleController : MonoBehaviour
{
    public float horizontalSpeed = 10.0f;
    public List<Tuple<string, float>> suffixes = new List<Tuple<string, float>>();


    private Bubble _bubble;
    private Car _car;


    private void Start()
    {
        _bubble = GetComponent<Bubble>();
        _car = GetComponent<Car>();
    }


    void FixedUpdate()
    {
        float horizontalValue = 0;
        float verticalValue = 0;
        for (int i = 0, count = suffixes.Count; i < count; ++i)
        {
            horizontalValue += Input.GetAxis(suffixes[i].Item1 + "Horizontal");
            verticalValue += Input.GetAxis(suffixes[i].Item1 + "Vertical");
        }
        Control(horizontalValue, verticalValue);
    }


    private void Control(float horizontalValue, float verticalValue)
    {
        if (0.0f < Mathf.Abs(horizontalValue))
        {
            horizontalValue *= horizontalSpeed;
            horizontalValue *= _bubble.Size;
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