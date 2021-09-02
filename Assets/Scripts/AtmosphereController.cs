using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtmosphereController : MonoBehaviour
{
    [SerializeField] Transform planetBodyTr;

    [SerializeField] AnimationCurve DragCurveOverRadius;
    [SerializeField] float baseDrag;
    private void OnTriggerStay2D(Collider2D collision)
    {
        SpaceObject debris = collision.attachedRigidbody.GetComponent<SpaceObject>();

        float distanceFromPlanetSurface = Vector2.Distance(collision.transform.position, planetBodyTr.position);
        float drag = DragCurveOverRadius.Evaluate
            (Mathf.InverseLerp(transform.localScale.x/2, planetBodyTr.localScale.x/2, distanceFromPlanetSurface)) * baseDrag;
        collision.attachedRigidbody.drag = drag;
        //Debug.Log($"{collision.gameObject} is burning up at {drag} drag with distance of {distanceFromPlanetSurface} from the center");
        //Debug.DrawLine(collision.transform.position, planetBodyTr.position, Color.red);

        debris.StayInAtmosphere(drag);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        SpaceObject debris = collision.attachedRigidbody.GetComponent<SpaceObject>();
        collision.attachedRigidbody.drag = 0;
    }
}
