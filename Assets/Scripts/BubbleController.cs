using UnityEngine;
using System.Collections;

public class BubbleController : MonoBehaviour
{
    public float horizontalSpeed = 10.0f;

    private CarScript _car;

    void Start()
    {
        _car = GetComponent<CarScript>();
    }

    void Update()
    {
        float horizontalValue = Input.GetAxis("Horizontal");
        if (0.0f < Mathf.Abs(horizontalValue))
        {
            horizontalValue *= horizontalSpeed;
            horizontalValue *= Time.deltaTime;
            horizontalValue *= _car.MovementRate;
            rigidbody2D.AddForce(transform.rotation * new Vector2(horizontalValue, 0));
        }


        float verticalValue = Input.GetAxis("Vertical");
        _car.ChangeSizeTo(verticalValue);
    }
}