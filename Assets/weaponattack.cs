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
    public enum WeaponType
    {
        Sword,
        bow,
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
    {
        switch(weapontype)
        {
            case WeaponType.Sword:
            degreesPerSecond = 180;
            maxRotation = 180;
            dmg = 5;

                swordAttack();
                break;
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


        // Update the current rotation
        currentRotation += degreesToRotate ;
        //Debug.Log(currentRotation);
        // Rotate the object around the Z-axis in 2D space
        parentTransform.Rotate(Vector3.forward, degreesToRotate);

        // Check if the object has reached its maximum rotation or original rotation
        if (Mathf.Abs(currentRotation) >= maxRotation)
        {
            // Reverse the rotation direction
            rotationDirection = -1;

            // Check if we are returning to the original rotation
            //if (!returningToOriginalRotation)
            //{
            //    
            //    // Perform some action when the object returns to the original rotation

            //}
            //else
            //{
            //    returningToOriginalRotation = true;
            //}
        }

        //Debug.Log("Current rotation: " + currentRotation + " startpoint: " + startPoint);
        //Debug.Log("Approx: " + Mathf.Approximately(currentRotation, startPoint));
        // Reset the "returningToOriginalRotation" flag when we are back at the starting position
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
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (attackWithWeapon && attackICD && collision.gameObject.tag == "Enemy")
        {

            //enemy attack script
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
    float findDifference(float num1, float num2)
    {
        float absoluteDifference = Mathf.Abs(num1 - num2);
        return absoluteDifference;
    }

}
