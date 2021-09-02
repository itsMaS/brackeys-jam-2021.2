using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : SpaceObject
{
    [SerializeField] AnimationCurve smokeParticlesMultiplierOverDrag;

    [SerializeField] ParticleSystem smokeParticles;

    [SerializeField] float burnRate;
    [SerializeField] AnimationCurve burnOverDrag;

    float baseTrailTime;
    override protected void Awake()
    {
        base.Awake();
    }
    public override void EnterAtmosphere()
    {

    }
    public override void StayInAtmosphere(float drag)
    {
        smokeParticles.enableEmission = true;
        ParticleSystem.EmissionModule emission = smokeParticles.emission;
        emission.rateOverDistanceMultiplier = smokeParticlesMultiplierOverDrag.Evaluate(drag) * transform.localScale.x;

        if (transform.localScale.x > 0)
        {
            transform.localScale -= Vector3.one * burnOverDrag.Evaluate(drag) * burnRate;
            rb.mass = transform.localScale.x;
        }
        else
        {
            smokeParticles.transform.parent = null;
            Destroy(gameObject);
        }
    }

    public override void ExitAtmosphere()
    {
        smokeParticles.enableEmission = false;
    }
}
