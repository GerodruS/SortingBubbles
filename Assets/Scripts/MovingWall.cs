using UnityEngine;
using System.Collections;

public class MovingWall : MonoBehaviour
{
    public GameObject wall;
    public Transform positionA;
    public Transform positionB;
    public float force = 1.0f;
    public bool fromAtoB = true;


    private void Start()
    {
        wall.transform.position = fromAtoB ? positionA.position : positionB.position;
        fromAtoB = !fromAtoB;
    }


    private void FixedUpdate()
    {
        Vector2 direction = (fromAtoB ? positionB.position : positionA.position) - wall.transform.position;
        direction.Normalize();
        wall.rigidbody2D.AddForce(direction * force);
    }

}
