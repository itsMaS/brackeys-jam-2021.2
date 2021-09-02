using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    public static PlanetController Instance;
    private void Awake()
    {
        Instance = this;
    }

    [Header("Gravity parameters")]
    public float massAndForce;

    [Header("Impact Particle")]
    [SerializeField] AnimationCurve particleAmountPerMass;
    [SerializeField] AnimationCurve particleSpeedPerVelocity;
    [SerializeField] GameObject ImpactParticlePrefab;

    public Vector2 VectorToCenter(Transform obj)
    {
        return transform.position - obj.position;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log($"Impact!");

        Vector2 impactDirection = collider.attachedRigidbody.velocity.normalized;

        Debug.DrawLine(collider.transform.position, (Vector2)collider.transform.position + impactDirection * 2, Color.red, 2);

        SpawnImpact(collider);
        Destroy(collider.gameObject);
    }

    private void SpawnImpact(Collider2D collider)
    {
        ParticleSystem ps = Instantiate(ImpactParticlePrefab, collider.transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
        ParticleSystem.EmissionModule em = ps.emission;
        em.SetBurst(0, new ParticleSystem.Burst(0, particleAmountPerMass.Evaluate(collider.attachedRigidbody.mass)));

        ParticleSystem.MainModule main = ps.main;
        main.startSpeed = new ParticleSystem.MinMaxCurve(0, particleSpeedPerVelocity.Evaluate(collider.attachedRigidbody.velocity.magnitude));
    }
}
