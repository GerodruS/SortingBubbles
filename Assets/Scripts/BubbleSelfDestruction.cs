using UnityEngine;
using System.Collections;

public class BubbleSelfDestruction : MonoBehaviour
{
    public float cooldownTime = 0.0f;
    public float radiusMin = 0.1f;
    public float radiusInfluence = 1.0f;

    private Cooldown _cooldown;
    private Bubble _bubble;

    // Use this for initialization
    void Start()
    {
        _bubble = GetComponent<Bubble>();
        _cooldown = new Cooldown();

        _cooldown.StartTimer(cooldownTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_cooldown.isReady())
        {
            if (radiusMin < _bubble.GetRadius(false))
            {
                _bubble.SetRadius(0.0f);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            _cooldown.Step(Time.deltaTime, radiusInfluence / _bubble.GetRadiusTarget());
        }
    }
}
