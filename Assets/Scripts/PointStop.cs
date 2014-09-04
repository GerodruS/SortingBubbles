using UnityEngine;
using System.Collections;

public class PointStop : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "CubeWall")
        {
            MovingWall movingWall = other.GetComponentInParent<MovingWall>();
            movingWall.fromAtoB = !movingWall.fromAtoB;
        }
    }
}
