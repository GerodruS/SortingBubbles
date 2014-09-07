using UnityEngine;
using System.Collections;

public class CameraFollowing : MonoBehaviour
{
    public float maxAngle = 1.0f;
    public float distance = 1.0f;

    private Car _car;
    private BubbleController _bubbleController;
    private Bubble _bubble;

    private void Start()
    {
        _car = GetComponent<Car>();
        _bubbleController = GetComponent<BubbleController>();
        _bubble = GetComponent<Bubble>();
    }


    private void FixedUpdate()
    {
        Camera[] cameras = GetComponentsInChildren<Camera>();
        for (int i = 0, count = cameras.Length; i < count; ++i)
        {
            Camera camera = cameras[i];
            
            Vector3 directionCurrent = camera.transform.rotation * Vector3.up;
            Vector2 velocity = _car.rigidbody2D.velocity;
            velocity.Normalize();

            velocity += _car.CurrentDirection * 10.0f;

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

            float posZ = camera.transform.localPosition.z;
            camera.transform.localPosition = new Vector3(0.0f, 0.0f, posZ);
            camera.transform.Rotate(0.0f, 0.0f, z);
            camera.transform.localPosition = new Vector3(0.0f, distance * distance / gameObject.transform.localScale.x, posZ);
        }
    }

}
