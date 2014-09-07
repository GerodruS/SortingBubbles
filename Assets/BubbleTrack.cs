using UnityEngine;
using System.Collections;

public class BubbleTrack : MonoBehaviour
{
    public GameObject generatedBubble;
    public float startRadius = 3.0f;
    public float cooldownTime = 0.1f;
    public float penalty = 0.1f;
    public float startForce = 100.0f;

    private Bubble _bubble;
    private Cooldown _cooldown = new Cooldown();

    // Use this for initialization
    void Start()
    {
        _bubble = GetComponent<Bubble>();
        _cooldown.StartTimer(cooldownTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (_cooldown.isReady() && startRadius < _bubble.GetRadiusTarget())
        if (startRadius < _bubble.GetRadiusTarget())
        {
            generateBubble();
            _cooldown.StartTimer(cooldownTime);
        }

        _cooldown.Step(Time.deltaTime);
    }

    void generateBubble()
    {
        float radiusOld = _bubble.GetRadiusTarget();
        float r = radiusOld * penalty;

        GameObject gameObjectNew = (GameObject)Instantiate(generatedBubble, transform.position, Quaternion.identity);
        Bubble bubbleNew = gameObjectNew.GetComponent<Bubble>();
        bubbleNew.radiusStart = 0.0f;
        bubbleNew.SetRadius(r);

        float radiusDelta = bubbleNew.GetRadiusTarget();
        _bubble.ChangeRadius(-radiusDelta);

        bubbleNew.rigidbody2D.AddForce(_bubble.CurrentDirection * -startForce);

        Crash crash = GetComponent<Crash>();
        crash.IgnoreCollision(gameObjectNew.collider2D);
    }

}
