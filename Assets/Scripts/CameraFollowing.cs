using UnityEngine;
using System.Collections;

public class CameraFollowing : MonoBehaviour
{
    private Car _car;
    private BubbleController _bubbleController;
    private Camera _camera;
    private float maxAngle = 1.0f;

    private void Start()
    {
        _car = GetComponent<Car>();
        _bubbleController = GetComponent<BubbleController>();
        _camera = GetComponentInChildren<Camera>();
    }


    private void FixedUpdate()
    {
        Vector3 directionCurrent = _camera.transform.rotation * Vector3.up;
        Vector2 velocity = _car.rigidbody2D.velocity;
        velocity.Normalize();

        velocity += _bubbleController.CurrentDirection * 10.0f;

        Vector3 directionTarget = new Vector3(velocity.x, velocity.y, 0);
        Quaternion q = Quaternion.FromToRotation(directionCurrent, directionTarget);

        float z = q.eulerAngles.z;
        if (maxAngle < z && z < 360.0f - maxAngle)
        {
            if (180.0f < z)
            {
                z = 360.0f - maxAngle;
            }
            else
            {
                z = maxAngle;
            }
        }

        _camera.transform.Rotate(0.0f, 0.0f, z);
    }

}
