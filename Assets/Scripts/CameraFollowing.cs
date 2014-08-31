using UnityEngine;
using System.Collections;

public class CameraFollowing : MonoBehaviour
{
    public float DistanceRate = 1.0f;

    private CarScript _car;
    

    // Use this for initialization
    void Start()
    {
        _car = GetComponent<CarScript>();
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 directionCurrent = transform.rotation * new Vector3(0, 1, 0);
        Vector3 directionTarget = new Vector3(_car.PositionDelta.x, _car.PositionDelta.y, 0);
        Quaternion q = Quaternion.FromToRotation(directionCurrent, directionTarget);
        transform.Rotate(q.eulerAngles);
    }
}
