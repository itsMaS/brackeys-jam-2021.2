using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject AsteroidPrefab;

    [SerializeField] float launchForce = 10;
    [SerializeField] float aimClamp = 10;

    [SerializeField] Camera cam;
    Vector2 mouseWorldPosition;
    private DefaultActions Actions;

    private void Awake()
    {
        Actions = new DefaultActions();
    }
    private void OnEnable()
    {
        Actions.Player.Point.performed += Mouse;
        Actions.Player.Point.Enable();

        Actions.Player.Fire.started += BeginAiming;
        Actions.Player.Fire.canceled += EndAiming;
        Actions.Player.Fire.performed += Aim;
        Actions.Player.Fire.Enable();
    }

    public void Mouse(InputAction.CallbackContext context)
    {
        Vector2 screenPos = context.ReadValue<Vector2>();
        mouseWorldPosition = cam.ScreenToWorldPoint(screenPos);
    }


    Vector2 aimStartPosition;

    private void BeginAiming(InputAction.CallbackContext context)
    {
        aimStartPosition = mouseWorldPosition;

    }
    private void EndAiming(InputAction.CallbackContext context)
    {
        Rigidbody2D rb = Instantiate(AsteroidPrefab, aimStartPosition, Quaternion.identity).GetComponent<Rigidbody2D>();

        rb.AddForce(Vector2.ClampMagnitude((aimStartPosition - mouseWorldPosition),aimClamp) * launchForce, ForceMode2D.Impulse);
    }
    private void Aim(InputAction.CallbackContext context)
    {
    }
}
