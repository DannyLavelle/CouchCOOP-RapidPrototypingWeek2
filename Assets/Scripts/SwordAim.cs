using UnityEngine;
using UnityEngine.InputSystem;

public class InstantRotation : MonoBehaviour
{
    private PlayersInput playerInput;
    private InputAction aim;
    private Transform playerTransform;
    private Vector2 aimInput;
    [HideInInspector] public bool isAttacking = false;
    private void Awake()
    {
        playerInput = new PlayersInput();
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
        aimWeapon();
        
    }
    void aimWeapon()
    {
        if (aimInput != Vector2.zero && isAttacking == false)
        {
            // Calculate the angle based on the input and set the rotation directly
            float angle = Mathf.Atan2(aimInput.y, aimInput.x) * Mathf.Rad2Deg;
            //Debug.Log(angle);
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
        }
    }
}
