using UnityEngine;
using System.Collections;

public class CameraFollowing : MonoBehaviour
{
    private Car _car;
    

    // Use this for initialization
    private void Start()
    {
        _car = GetComponent<Car>();
    }


    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector3 directionCurrent = transform.rotation * Vector3.up;
        Vector2 velocity = _car.rigidbody2D.velocity;
        Vector3 directionTarget = new Vector3(velocity.x, velocity.y, 0);
        Quaternion q = Quaternion.FromToRotation(directionCurrent, directionTarget);
        transform.Rotate(0.0f, 0.0f, q.eulerAngles.z);
    }
}
