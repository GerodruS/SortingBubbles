using UnityEngine;
using System.Collections;

public class RandomWall : MonoBehaviour
{
    public GameObject wall;
    public int positionsCount = 3;

    private void Start()
    {
        Reset();
    }

    public void Reset()
    {
        float start = -0.5f + positionsCount / 2.0f;
        int n = (int)(Random.value * positionsCount);
        wall.transform.localPosition = new Vector3(-start + n, 0.0f, 0.0f);
    }
}
