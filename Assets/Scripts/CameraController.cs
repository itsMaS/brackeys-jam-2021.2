using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cam;

    [Header("Pan")]
    [SerializeField] float panSensitivity;

    [Header("Zoom")]
    [SerializeField] float zoomMin;
    [SerializeField] float zoomMax;
    [SerializeField] float zoomFollowSpeed;
    [SerializeField] float zoomSensitivity;

    Vector2 panInput;

    float zoomTarget;
    float zoomVelocity;


    DefaultActions Actions;
    private void Awake()
    {
        Actions = new DefaultActions();
    }
    private void OnEnable()
    {
        Actions.Player.CameraPan.performed += CameraPan;
        Actions.Player.CameraZoom.performed += CameraZoom;
        Actions.Player.CameraPan.Enable();
        Actions.Player.CameraZoom.Enable();
    }

    private void CameraPan(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        panInput = context.ReadValue<Vector2>();
    }

    private void Start()
    {
        zoomTarget = cam.m_Lens.OrthographicSize;
    }

    private void CameraZoom(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        float zoom = context.ReadValue<float>();
        zoomTarget = Mathf.Clamp(zoomTarget+zoomSensitivity*zoom* cam.m_Lens.OrthographicSize, zoomMin, zoomMax);
    }
    void Update()
    {
        cam.m_Lens.OrthographicSize = Mathf.SmoothDamp(cam.m_Lens.OrthographicSize, zoomTarget, ref zoomVelocity, 1/zoomFollowSpeed);
        transform.position += (Vector3)panInput * panSensitivity * cam.m_Lens.OrthographicSize;
    }
}
