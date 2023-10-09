using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwordAim : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction aim;
    private Transform playerTransform;
    private float rotationSpeed = 5f;

    private Vector2 aimInput;

    private Quaternion initialRotation;

    private void Awake()
    {
        playerInput = new PlayerInput();
        initialRotation = transform.rotation;
    }

    private void OnEnable()
    {
        aim = playerInput.Player.Aim;
        aim.Enable();
    }

    private void OnDisable()
    {
        aim.Disable();
    }

    private void Update()
    {
        aimInput = aim.ReadValue<Vector2>().normalized;

        // Calculate the angle based on the input or use the initial rotation if there's no input
        float angle = aimInput != Vector2.zero
            ? Mathf.Atan2(aimInput.y, aimInput.x) * Mathf.Rad2Deg
            : initialRotation.eulerAngles.z;

        // Smoothly interpolate between the current rotation and the calculated angle
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
