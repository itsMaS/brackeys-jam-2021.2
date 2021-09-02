using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpaceObject : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    protected virtual void FixedUpdate()
    {
        Vector2 toCenter = PlanetController.Instance.VectorToCenter(transform);
        rb.AddForce(toCenter.normalized * PlanetController.Instance.massAndForce * rb.mass / Mathf.Pow(toCenter.magnitude,2));
    }
    public virtual void EnterAtmosphere()
    {

    }
    public virtual void StayInAtmosphere(float drag)
    {
    }
    public virtual void ExitAtmosphere()
    {
    }
    private void OnDestroy()
    {
        var trail = GetComponentInChildren<TrailRenderer>();
        trail.transform.parent = null;
        Destroy(trail.gameObject, trail.time);
    }
}
