using UnityEngine;
using System.Collections;

public class Crash : MonoBehaviour
{
    public float additionalStrength = 1.0f;
    public float cooldown = 1.0f;
    public float carSizeMin = 1.0f;
    public float bubbleSizeMin = 0.1f;
    public float bubbleSizeMax = 1.0f;
    public float startSpeedMax = 500.0f;

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

    public GameObject bubbleObject;
    
    float GenerateBubble(float size = -1.0f)
    {
        if (size <= 0.0f)
        {
            size = bubbleSizeMin + Random.value * (bubbleSizeMax - bubbleSizeMin);
        }

        GameObject obj = (GameObject)Instantiate(bubbleObject, transform.position, Quaternion.identity);
        obj.transform.localScale = new Vector3(size, size, size);
        obj.rigidbody2D.AddForce(Random.insideUnitCircle * Random.value * startSpeedMax);
        
        return size;
    }

    void OnCrash(float damage)
    {
        float sizeOld = car.SizeOrigin;
        if (carSizeMin < sizeOld)
        {
            car.SizeOrigin = sizeOld * (1 - damage);
            float sizeBobbles = sizeOld * damage;
            while (bubbleSizeMin < sizeBobbles)
            {
                sizeBobbles -= GenerateBubble();
            }
            if (0.0f < sizeBobbles)
            {
                GenerateBubble(sizeBobbles);
            }
        }
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if ("AirBonus" == other.tag)
        {
            //Debug.Log("!");
            //Destroy(other);
        }
    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        if ("AirBonus" == coll.gameObject.tag)
        {
            //Debug.Log("!");
            AirBonus bonus = coll.gameObject.GetComponent<AirBonus>();
            if (bonus.isReady())
            {
                float scale = coll.transform.localScale.x;
                car.SizeOrigin += scale;
                Destroy(coll.gameObject);
            }
        }
        else if (cooldownCurrent <= 0.0f && additionalStrength * car.Strength < coll.relativeVelocity.magnitude)
        {
            //float value = coll.relativeVelocity.magnitude - additionalStrength * car.Strength;
            //car.ChangeSizeTo();
            float value = 0.2f;
            OnCrash(value);
            cooldownCurrent = cooldown;
        }
        else
        {
            //Debug.Log("no");
        }
    }

}
