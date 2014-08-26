using UnityEngine;
using System.Collections;

public class BubbleController : MonoBehaviour
{
    public float horizontalSpeed = 1.0f;

    void Update()
    {
        float horizontalValue = Input.GetAxis("Horizontal");
        horizontalValue *= horizontalSpeed;
        horizontalValue *= Time.deltaTime;
        rigidbody2D.AddForce(new Vector2(horizontalValue, 0));
    }
}