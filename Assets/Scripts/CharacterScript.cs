using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterScript : MonoBehaviour
{
    public float movementSpeed = 5f;
    public int maxHealth = 100;
    public int currentHealth;
    public float arrowSpeed = 10f;
    public Rigidbody2D rb;
    public PlayerInput playerinput;
    private InputAction move;
    public GameObject slashAttackPrefab;
    public GameObject arrowPrefab;
    public Transform attackSpawnPoint;
    float horizontalInput;
    float verticalInput;
    public Vector2 movementInput;

    private void Awake()
    {
        //playerinput = new PlayerInput();
        currentHealth = maxHealth;
    }
    //private void OnEnable()
    //{
    //    move = playerinput.Player.Move; // Assuming "Movement" is the name of your movement action
    //    move.Enable();
    //}
    //private void OnDisable()
    //{
    //    move.Disable();
    //}

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {

        // Move the character
        Vector2 movementVelocity = movementInput * movementSpeed;
        //Debug.Log(movementVelocity);
        rb.velocity = movementVelocity;



    }
    private void Update()
    {
         //horizontalInput = Input.GetAxis("Horizontal");
         //verticalInput = Input.GetAxis("Vertical");
        // Movement input

        // Attack input
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    // Check if the character is a knight
        //    if (gameObject.CompareTag("Knight"))
        //    {
        //        PerformSlashAttack();
        //    }
        //    // Check if the character is a ranger
        //    else if (gameObject.CompareTag("Ranger"))
        //    {
        //        PerformArrowAttack();
        //    }
        //}
    }

    private void PerformSlashAttack()
    {
        // Create a slash attack at the attack spawn point
        Instantiate(slashAttackPrefab, attackSpawnPoint.position, Quaternion.identity);
        // Add additional logic for dealing damage to enemies
    }

    private void PerformArrowAttack()
    {
        // Create an arrow at the attack spawn point and set its direction
        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        GameObject arrow = Instantiate(arrowPrefab, attackSpawnPoint.position, Quaternion.identity);
        arrow.GetComponent<Rigidbody2D>().velocity = direction * arrowSpeed;
        // Add additional logic for dealing damage to enemies
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>().normalized;
        
        // Normalize the input vector to prevent diagonal movement from being faster
        movementInput.Normalize();
        
        // Move the character
    
    }
}
