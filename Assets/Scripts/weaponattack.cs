using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Dependencies.Sqlite.SQLite3;
using static UnityEngine.GridBrushBase;

public class weaponattack : MonoBehaviour
{
    [SerializeField]
    bool begunAttacking = false;
    public WeaponType weapontype;
    public GameObject arrowPrefab;
    private bool isOnCooldown = false;
    bool canFire = true;
    // The duration of the cooldown in seconds
    public float cooldownDuration = 2f;
    public enum WeaponType
    {
        Sword,
        bow,
        shield,
        // Add more weapon types as needed
    }
    private Transform parentTransform;
    public float degreesPerSecond = 180.0f;
    float maxRotation=180f;
    [HideInInspector]public bool attackWithWeapon = false;
    float dmg;
    int rotationDirection = 1;
    private float currentRotation = 0.0f;
    private bool returningToOriginalRotation = false;
    bool attackICD = true;
    float startPoint; 
    // Start is called before the first frame update

    private void Start()
    {
       
    }
    private void Update()
    {
        if(attackWithWeapon)
        {
            parentTransform = transform.parent;

            InstantRotation swordAim = parentTransform.GetComponent<InstantRotation>();
            swordAim.isAttacking = true;
            weaponAttack();
        }
        
    }
    public void weaponAttack()
    {if(isOnCooldown == false)
        {
            switch (weapontype)
            {
                case WeaponType.Sword:
                degreesPerSecond = 180;
                maxRotation = 180;
                dmg = 5;
                cooldownDuration = .5f;

                swordAttack();
                break;
                case WeaponType.bow:
                cooldownDuration = 1f;
                dmg = 0;
                bowAttack();
                
                break ;
                
                case WeaponType.shield:
                dmg = 3;
                cooldownDuration = 2;
                ShieldAttack();
                break;
            }
            
        }
        
    }
    void swordAttack()
    {
        if (begunAttacking == false)
        {
            
             startPoint = parentTransform.eulerAngles.z;
            Debug.Log("set start point:" + startPoint); 
            begunAttacking = true;
        }

        
        float degreesToRotate = degreesPerSecond * rotationDirection * Time.deltaTime;



        currentRotation += degreesToRotate ;
  

        parentTransform.Rotate(Vector3.forward, degreesToRotate);


        if (Mathf.Abs(currentRotation) >= maxRotation)
        {
           
            rotationDirection = -1;

        
        }

        if(rotationDirection == -1)
        {
            if (findDifference(parentTransform.eulerAngles.z, startPoint) <= 1)
            {
                Debug.Log("Broke rotation");
                InstantRotation swordAim = parentTransform.GetComponent<InstantRotation>();
                rotationDirection = 1;
                swordAim.isAttacking = false;
                attackWithWeapon = false;
                currentRotation = 0.0f;
                begunAttacking = false;
                StartCooldown();
            }
        }
    }
    void bowAttack()
    {
        if(canFire)
        {
            Quaternion arrowRotation = transform.rotation;
            Debug.Log("firing rotation:" + transform.rotation);

            GameObject newArrow = Instantiate(arrowPrefab, transform.position, parentTransform.rotation * Quaternion.Euler(0f, 0f, 90f));

            // Ensure the arrow's initial position matches the spawner's position
            //newArrow.transform.position = transform.position;

            // Set the new arrow as a child of the parentTransform (weaponattack)
            //newArrow.transform.parent = parentTransform;
            attackWithWeapon = false;

            InstantRotation swordAim = parentTransform.GetComponent<InstantRotation>();
            swordAim.isAttacking = false;
            canFire  = false;
            StartCooldown();
            canFire = true;
            begunAttacking = false;
        }
   
        
    }
    void ShieldAttack()
    {
        if (!isOnCooldown)
        {
            // Add logic for the shield attack here

            // Activate the shield collider
            Transform shieldCollider = transform.Find("ShieldCollider");
            if (shieldCollider != null)
            {
                Collider2D collider = shieldCollider.GetComponent<Collider2D>();
                collider.isTrigger = false;

                // Start the cooldown
               

                // Deactivate the shield collider after 2 seconds
                StartCoroutine(DeactivateColliderForDuration(collider, 2f));
                InstantRotation swordAim = parentTransform.GetComponent<InstantRotation>();
                swordAim.isAttacking = false;
                attackWithWeapon = false;
                begunAttacking = false;
                StartCooldown();
            }
        }
    }

    private IEnumerator DeactivateColliderForDuration(Collider2D collider, float duration)
    {

        yield return new WaitForSeconds(duration);

        // Deactivate the collider by making it a trigger again
        collider.isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (attackWithWeapon && attackICD && collision.gameObject.tag == "Enemy")
        {
            if (weapontype == WeaponType.shield)
            {
                collision.isTrigger = false ;
                StartCoroutine(TriggerCoroutine(2, collision));
            }
            //enemy attack script
            HitManager Hit = collision.gameObject.GetComponent<HitManager>();
            Debug.Log(Hit);
            Hit.Hit(dmg);
            attackICD = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (attackWithWeapon && collision.gameObject.tag == "Enemy")
        {
            attackICD = true;
        }
    }
    private void StartCooldown()
    {
        isOnCooldown = true;
        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(cooldownDuration);

        // Cooldown is over
        isOnCooldown = false;
        Debug.Log("Cooldown is over!");
    }
    private IEnumerator TriggerCoroutine(float duration, Collider2D collision)
    {
        
        yield return new WaitForSeconds(duration);
        collision.isTrigger = true;
        
    }
    float findDifference(float num1, float num2)
    {
        float absoluteDifference = Mathf.Abs(num1 - num2);
        return absoluteDifference;
    }
}
