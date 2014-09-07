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
        wall.transform.localPosition = new Vector3(-start + n + Random.value - 0.5f, Random.value - 0.5f, Random.value - 0.5f);
        wall.transform.rotation = Quaternion.EulerAngles(0.0f, 0.0f, Random.value - 0.5f + (n - start) * -30.0f);
        wall.transform.localScale = new Vector3(Random.value + 0.5f, Random.value + 0.5f, 1.0f);
    }

    private void Update()
    {
        //Reset();
    }
}
