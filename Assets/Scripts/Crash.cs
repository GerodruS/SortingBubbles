using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Crash : MonoBehaviour
{
    public float strength = 1.0f;
    public float cooldownTime = 0.1f;
    public float penalty = 0.3f;
    public Bubble generatedBubble;
    public float startForce = 10.0f;
    public int maxCount = 5;

    private Bubble _bubble;
    private Cooldown _cooldown;


    private void Start()
    {
        _bubble = GetComponent<Bubble>();
        _cooldown = new Cooldown();
    }


    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (_cooldown.isReady())
        {
            float r = _bubble.GetRadius(false);
            if (strength / (r * (1.0f + _bubble.rate)) < coll.relativeVelocity.magnitude)
            {
                Bubble bubbleOther = coll.gameObject.GetComponent<Bubble>();
                if (null == bubbleOther || Mathf.Abs(_bubble.GetRadius(true) - bubbleOther.GetRadius(true)) < 0.1f)
                {
                    // crash
                    _cooldown.StartTimer(cooldownTime);

                    generateBubbles(penalty, coll.relativeVelocity.magnitude);

                    r *= (1.0f - penalty);
                    _bubble.SetRadius(r);
                }
                else if (bubbleOther != null && bubbleOther.GetRadius(true) < _bubble.GetRadius(true))
                {
                    // absorption
                    _bubble.ChangeRadius(bubbleOther.GetRadius(false));
                    Destroy(bubbleOther.gameObject);
                }
            }
        }
    }

    private void Update()
    {
        _cooldown.Step(Time.deltaTime);
    }

    private List<Bubble> objectsToIgnore = new List<Bubble>();

    private void generateBubbles(float value, float velocity)
    {
        objectsToIgnore.Clear();
        objectsToIgnore.Add(_bubble);

        for (int i = 0; i < maxCount && 0.1f < value; ++i)
        {
            Bubble bubbleNew = (Bubble)Instantiate(generatedBubble, transform.position, Quaternion.identity);
            objectsToIgnore.Add(bubbleNew);

            bubbleNew.radiusStart = 0.0f;

            float r = (i == maxCount - 1 ?
                       1.0f :
                       0.1f + Random.value * 0.8f) * value;

            bubbleNew.SetRadius(r);
            Rigidbody2D body = bubbleNew.GetComponent<Rigidbody2D>();
            body.AddForce(Random.insideUnitCircle * startForce * velocity);

            value -= r;
        }

        int count = objectsToIgnore.Count;
        for (int i = 0; i < count; ++i)
        {
            Bubble a = objectsToIgnore[i];
            for (int j = 0; j < count; ++j)
            {
                Bubble b = objectsToIgnore[j];
                a.IgnoreCollision(b);
            }
        }
    }

}