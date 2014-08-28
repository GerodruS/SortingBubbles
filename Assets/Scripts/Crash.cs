using UnityEngine;
using System.Collections;

public class Crash : MonoBehaviour
{
    public float additionalStrength = 1.0f;
    public float cooldown = 1.0f;

    private CarScript car;
    private float cooldownCurrent = 0.0f;


    // Use this for initialization
    void Start()
    {
        car = GetComponent<CarScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (0.0f < cooldownCurrent)
        {
            cooldownCurrent -= Time.deltaTime;
        }
    }

    public GameObject prefab;

    void generateBubbles(float valueAll)
    {
        GameObject[] cars = GameObject.FindGameObjectsWithTag("Cars");
        //Debug.Log("BOOM " + valueAll);
        //float valueOne = Random.value * valueAll;
        int count = (int)(Random.value * 10);
        for (int i = 0; i < count; ++i)
        {
            GameObject go = (GameObject)Instantiate(prefab, transform.position, Quaternion.identity);

            go.rigidbody2D.AddForce(Random.insideUnitCircle * Random.value * 500);

            /*
            if (cars != null)
            {
                for (int j = 0; j < cars.Length; ++j)
                {
                    Physics2D.IgnoreLayerCollision(go.GetComponent<Collider2D>(), cars[j].GetComponent<Collider2D>());
                }
            }
            */
            //valueAll -= valueOne;
            //valueOne = Random.value * valueAll;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (cooldownCurrent <= 0.0f && additionalStrength * car.Strength < coll.relativeVelocity.magnitude)
        {
            float value = coll.relativeVelocity.magnitude - additionalStrength * car.Strength;
            //car.ChangeSizeTo();
            generateBubbles(value);
            cooldownCurrent = cooldown;
        }
        else
        {
            //Debug.Log("no");
        }
    }
}
