using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BubbleController : MonoBehaviour
{
    public float verticalSpeed = 2.0f;
    public float horizontalSpeed = 10.0f;
    public string suffix = "P1_";

    public Vector2 CurrentDirection
    {
        get
        {
            return _currentDirection;
        }
    }


    private Bubble _bubble;
    private Vector2 _currentDirection = Vector2.zero;
    private List<Vector2> directions = new List<Vector2>();
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

        {
            Vector2 v = Vector2.zero;

            for (int i = 0, count = directions.Count; i < count; ++i)
            {
                v += directions[i];
            }
            v.Normalize();

            rigidbody2D.AddForce(v * verticalSpeed * _bubble.Size);
            _currentDirection = v;

            directions.Clear();
        }
    }


    private void Control(float horizontalValue, float verticalValue)
    {
        if (0.0f < Mathf.Abs(horizontalValue))
        {
            horizontalValue *= horizontalSpeed;
            Quaternion q = Quaternion.EulerAngles(0.0f, 0.0f, -90.0f);
            rigidbody2D.AddForce(q * CurrentDirection * horizontalValue);
        }
        
        _car.ChangeRadiusTo(verticalValue);
    }


    public void Reset()
    {
        Control(0.0f, 0.0f);
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (9 == other.gameObject.layer)
        {
            Vector2 v = other.gameObject.transform.rotation * Vector2.up;
            directions.Add(v * _bubble.Size);
        }
    }

}